﻿using System.Reflection;
using UnityEngine;
using UnityEditor;

namespace DRL {
	[CustomPropertyDrawer(typeof(ButtonAttribute))]
	[CanEditMultipleObjects]
	public class ButtonDrawer : PropertyDrawer
	{
		private float _height = 0.0f;

		#region Private constants

		/// <summary>
		/// The state of the button. It affects it's look.
		/// </summary>
		private enum State
		{
			/// <summary>
			/// The regular state. No special styling.
			/// </summary>
			Normal = 0,
			/// <summary>
			/// The button is in the inspector with multi-selection.
			/// </summary>
			Multi = 1,
			Error = 2
		}

		private const string DefalutText = "DO!";
		private const string MultiMsg = "[Multiple objects selected]";
		private static readonly Color MultiColor = new Color(1.0f, 0.7f, 0.4f);

		private const BindingFlags GetMethodFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static;

		#endregion

		#region Internal methods preparing the required data

		/// <summary>
		/// Generates the button's <see cref="GUIContent"/> object,
		/// containing the provided label and [optionally] tooltip.
		/// Tooltip could be modified to reflect the unusual state of the button.
		/// </summary>
		private static GUIContent ButtonContent(ButtonAttribute attr, State state, string errorMessage) {
			string text = attr.Label.Trim();
			if (string.IsNullOrEmpty(text))
				text = DefalutText;
			var tooltip = attr.Tooltip;

			// modify text for non-regular modes:
			if (state == State.Error) {
				string msg = string.Format("[ERROR: {0}]", errorMessage);
				tooltip =
					string.IsNullOrEmpty(tooltip) ?
					msg :
					msg + "\n" + tooltip
				;
				text = "[ERROR] " + text;
			} else if (state == State.Multi) {
				tooltip =
					string.IsNullOrEmpty(tooltip) ?
					MultiMsg :
					string.Format("{0}\n{1}", tooltip, MultiMsg)
				;
			}

			GUIContent buttonText = new GUIContent(text);
			if (!string.IsNullOrEmpty(tooltip))
				buttonText.tooltip = tooltip;
			return buttonText;
		}

		/// <summary>
		/// Calculate the actual width of the button, in pixels. It depends on the mode.
		/// </summary>
		/// <param name="attrWidth">The given width value, from the <see cref="ButtonAttribute"/>.</param>
		/// <param name="srcWidth">The maximum width available for the control, depends on inspector size.</param>
		/// <param name="textWidth">The width defined by the button text.</param>
		/// <returns></returns>
		private static float ButtonWidth(float attrWidth, float srcWidth, float textWidth) {
			float width;
			if (attrWidth > 2.0f) // absolute mode
				width = attrWidth;
			else if (attrWidth > 0.0f) // relative
				width = attrWidth * srcWidth;
			else // default mode - by text size
				width = Mathf.Min(textWidth, srcWidth);
			return Mathf.Min(width, srcWidth);
		}

		/// <summary>
		/// Calculates the button's <see cref="Rect"/> and <see cref="GUIStyle"/> matching to the provided attribute params.
		/// </summary>
		/// <param name="attr">The main <see cref="ButtonAttribute"/> object this drawer is applied to.</param>
		/// <param name="position">The original <see cref="Rect"/> generated by Unity and passed to <see cref="OnGUI"/>.</param>
		/// <param name="buttonText">The <see cref="GUIContent"/> object, containing text and an [optional] tooltip.</param>
		/// <param name="style">
		/// [out] Generated <see cref="GUIStyle"/> for the button.
		/// It's based on the default stule for inspector button, but has it's <see cref="GUIStyle.wordWrap"/> set
		/// acctordingly to the given button content.
		/// </param>
		private Rect ButtonRectAndStyle(
			ButtonAttribute attr, Rect position, GUIContent buttonText,
			out GUIStyle style
		) {
			// first, init the style and assume the width from it:
			style = EditorGUIUtility.GetBuiltinSkin(EditorSkin.Inspector).button;
			style.wordWrap = false;
			var expectedSize = style.CalcSize(buttonText);

			// get the desired width in pixels, depending on the mode:
			float srcWidth = position.width;
			float width = ButtonWidth(attr.Width, srcWidth, expectedSize.x);

			// calculate the height:
			if (expectedSize.x > width)
				style.wordWrap = true;
			_height = style.CalcHeight(buttonText, width);

			// generate the actual rect:
			return new Rect(
				position.x + Mathf.Max(srcWidth - width, 0.0f) * 0.5f,
				position.y,
				width,
				_height
			);
		}

		private Rect ButtonRect(ButtonAttribute attr, Rect position, GUIContent buttonText) {
			GUIStyle style;
			return ButtonRectAndStyle(attr, position, buttonText, out style);
		}

		/// <summary>
		/// Returns Corresponding <see cref="MethodInfo"/> object for the method on the  given target. Could be null.
		/// </summary>
		private static MethodInfo GetMethod(Object target, string methodName) {
			var t = target.GetType();
			try { return t.GetMethod(methodName, GetMethodFlags); }
			catch { return null; }
		}

		/// <summary>
		/// Calls the specified method on the given <see cref="Object"/>.
		/// Logs an error to the console if the method is not found.
		/// </summary>
		private static void CallMethod(Object target, string methodName) {
			var method = GetMethod(target, methodName);
			if (method == null)
				Debug.LogError(string.Format(
					"Button on <{0}> object:\n" +
					"Can't perform since the object doesn't have a <{1}> method",
					target.name, methodName
				));
			else
				method.Invoke(target, null);
		}

		/// <summary>
		/// Checks if the button in some error state and generates the appropriate message.
		/// </summary>
		/// <returns>True if button is erratic.</returns>
		private static bool IsErrorState(
			string methodName, Object[] targets,
			out string message
		) {
			if (string.IsNullOrEmpty(methodName)) {
				message = "No method provided for button click";
				return true;
			}

			if (targets == null || targets.Length == 0) {
				message = "No objects selected";
				return true;
			}

			int okTargets = 0;
			var numTargets = targets.Length;
			for (int i = 0; i < numTargets; ++i) {
				if (GetMethod(targets[i], methodName) != null)
					++okTargets;
			}
			if (okTargets == 0) {
				message = "Provided method doesn't exist on the selected objects";
				return true;
			}
			if (okTargets < numTargets) {
				message = "Can't perform action on selected objects because not all of them support it.";
				return true;
			}

			message = "";
			return false;
		}

		private static State GetState(
			SerializedProperty prop, string methodName,
			out Object[] targets, out int numTargets,
			out bool isMulti, out bool isError, out string errorMessage
		) {
			var serObj = prop.serializedObject;
			targets = serObj.targetObjects;
			numTargets = targets.Length;
			isMulti = serObj.isEditingMultipleObjects || numTargets > 1;
			isError = IsErrorState(methodName, targets, out errorMessage);
			return
				isError ?
				State.Error :
				(isMulti ? State.Multi : State.Normal)
			;
		}

		private static State GetState(SerializedProperty prop, string methodName) {
			Object[] targets;
			int numTargets;
			bool isMulti, isError;
			string errorMessage;
			return GetState(
				prop, methodName,
				out targets, out numTargets, out isMulti, out isError, out errorMessage
			);
		}

		#endregion

		public override void OnGUI(Rect position, SerializedProperty prop, GUIContent label) {
			var attr = (ButtonAttribute)attribute;
			var methodName = attr.Method;

			// Detect the state of the buton, which affects it look:
			Object[] targets;
			int numTargets;
			bool isMulti, isError;
			string errorMessage;
			var state = GetState(
				prop, methodName,
				out targets, out numTargets, out isMulti, out isError, out errorMessage
			);

			// Prepare rect, style and content:
			var buttonText = ButtonContent(attr, state, errorMessage);
			GUIStyle style;
			var buttonRect = ButtonRectAndStyle(attr, position, buttonText, out style);

			// the button may be disabled:
			using (new EditorGUI.DisabledScope(isError)) {
				// Now, actually draw the button...
				var oldColor = GUI.backgroundColor;
				if (isMulti) {
					GUI.backgroundColor = MultiColor;
				}

				var isPressed = GUI.Button(buttonRect, buttonText, style);
				// ... and call the function when the button is pressed:
				if (isPressed && !isError) {
					for (var i = 0; i < numTargets; ++i) {
						CallMethod(targets[i], methodName);
					}
				}

				if (isMulti) {
					GUI.backgroundColor = oldColor;
				}
			}
		}

		public override float GetPropertyHeight(SerializedProperty prop, GUIContent label) {
			if (_height > 0.5f)
				return _height;

			var attr = (ButtonAttribute)attribute;
			var state = GetState(prop, attr.Method);
			var buttonText = ButtonContent(attr, state, "");

			// Create a fake rect, positioned at (0, 0), width the width of the inspector area
			var fakeRect = new Rect(
				0.0f,
				0.0f,
				EditorGUIUtility.currentViewWidth, // width of free space in inspector
				base.GetPropertyHeight(prop, label)
			);
			return ButtonRect(attr, fakeRect, buttonText).height;
		}

	}
}