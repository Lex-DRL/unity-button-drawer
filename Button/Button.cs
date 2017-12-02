using System;
using UnityEngine;

namespace DRL
{
	public class ButtonAttribute : PropertyAttribute {
		public readonly Action ActionMethod;
		public readonly string Label;
		public readonly string Tooltip = "";

		public readonly bool WidthIsRelative = true;
		public readonly int WidthAbsolute = 0;
		public readonly float WidthRelative = 1.0f;

		#region Constructors

		/// <summary>
		/// This attribute adds a button before the affected field.
		/// </summary>
		/// <param name="action">The mathod to call on button click.</param>
		/// <param name="label">Test displayed on the button.</param>
		/// <param name="tooltip">[optional] Text displayed in the popup on mouse hover.</param>
		public ButtonAttribute(
			Action action, string label, string tooltip
		) {
			ActionMethod = action;
			Label = label;
			Tooltip = tooltip;
		}

		/// <summary>
		/// This attribute adds a button before the affected field.
		/// </summary>
		/// <param name="action">The mathod to call on button click.</param>
		/// <param name="label">Test displayed on the button.</param>
		public ButtonAttribute(
			Action action, string label
		) : this(action, label, "") { }

		/// <summary>
		/// This attribute adds a button before the affected field.
		/// </summary>
		/// <param name="action">The mathod to call on button click.</param>
		/// <param name="label">Test displayed on the button.</param>
		/// <param name="tooltip">[optional] Text displayed in the popup on mouse hover.</param>
		/// <param name="widthRel">[optional] Relative width of the button (from 0.0 to 1.0)</param>
		public ButtonAttribute(
			Action action, string label, string tooltip, float widthRel
		) : this(action, label, tooltip) {
			WidthRelative = widthRel;
		}

		/// <summary>
		/// This attribute adds a button before the affected field.
		/// </summary>
		/// <param name="action">The mathod to call on button click.</param>
		/// <param name="label">Test displayed on the button.</param>
		/// <param name="tooltip">[optional] Text displayed in the popup on mouse hover.</param>
		/// <param name="widthAbs">[optional] Absolute width of the button (in pixels).</param>
		public ButtonAttribute(
			Action action, string label, string tooltip, int widthAbs
		) : this(action, label, tooltip) {
			WidthIsRelative = false;
			WidthAbsolute = widthAbs;
		}

		/// <summary>
		/// This attribute adds a button before the affected field.
		/// </summary>
		/// <param name="action">The mathod to call on button click.</param>
		/// <param name="label">Test displayed on the button.</param>
		/// <param name="widthRel">[optional] Relative width of the button (from 0.0 to 1.0)</param>
		public ButtonAttribute(
			Action action, string label, float widthRel
		) : this(action, label, "", widthRel) { }

		/// <summary>
		/// This attribute adds a button before the affected field.
		/// </summary>
		/// <param name="action">The mathod to call on button click.</param>
		/// <param name="label">Test displayed on the button.</param>
		/// <param name="widthAbs">[optional] Absolute width of the button (in pixels).</param>
		public ButtonAttribute(
			Action action, string label, int widthAbs
		) : this(action, label, "", widthAbs) { }

		#endregion
	}
}