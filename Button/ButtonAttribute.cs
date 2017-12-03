using System;
using UnityEngine;

namespace DRL
{
	public class ButtonAttribute : PropertyAttribute {
		public readonly Action<UnityEngine.Object> ActionMethod;
		public readonly string Label;
		public readonly string Tooltip = "";

		public readonly bool WidthIsRelative = true;
		public readonly int WidthAbsolute = 0;
		public readonly float WidthRelative = 1.0f;

		#region Constructors

		/// <summary>
		/// This attribute adds a button before the affected field.
		/// </summary>
		/// <param name="action">
		/// The method to call on button click.
		/// This method receives the instance of the current class (the class this button is attached to)
		/// as an argument of type <see cref="UnityEngine.Object"/>.
		/// </param>
		/// <param name="label">Test displayed on the button.</param>
		/// <param name="tooltip">[optional] Text displayed in the popup on mouse hover.</param>
		public ButtonAttribute(
			Action<UnityEngine.Object> action, string label, string tooltip
		) {
			ActionMethod = action;
			Label = label;
			Tooltip = tooltip;
		}

		/// <summary>
		/// This attribute adds a button before the affected field.
		/// </summary>
		/// <param name="action">
		/// The method to call on button click.
		/// This method receives the instance of the current class (the class this button is attached to)
		/// as an argument of type <see cref="UnityEngine.Object"/>.
		/// </param>
		/// <param name="label">Test displayed on the button.</param>
		public ButtonAttribute(
			Action<UnityEngine.Object> action, string label
		) : this(action, label, "") { }

		/// <summary>
		/// This attribute adds a button before the affected field.
		/// </summary>
		/// <param name="action">
		/// The method to call on button click.
		/// This method receives the instance of the current class (the class this button is attached to)
		/// as an argument of type <see cref="UnityEngine.Object"/>.
		/// </param>
		/// <param name="label">Test displayed on the button.</param>
		/// <param name="tooltip">[optional] Text displayed in the popup on mouse hover.</param>
		/// <param name="widthRel">[optional] Relative width of the button (from 0.0 to 1.0)</param>
		public ButtonAttribute(
			Action<UnityEngine.Object> action, string label, string tooltip, float widthRel
		) : this(action, label, tooltip) {
			WidthRelative = widthRel;
		}

		/// <summary>
		/// This attribute adds a button before the affected field.
		/// </summary>
		/// <param name="action">
		/// The method to call on button click.
		/// This method receives the instance of the current class (the class this button is attached to)
		/// as an argument of type <see cref="UnityEngine.Object"/>.
		/// </param>
		/// <param name="label">Test displayed on the button.</param>
		/// <param name="tooltip">[optional] Text displayed in the popup on mouse hover.</param>
		/// <param name="widthAbs">[optional] Absolute width of the button (in pixels).</param>
		public ButtonAttribute(
			Action<UnityEngine.Object> action, string label, string tooltip, int widthAbs
		) : this(action, label, tooltip) {
			WidthIsRelative = false;
			WidthAbsolute = widthAbs;
		}

		/// <summary>
		/// This attribute adds a button before the affected field.
		/// </summary>
		/// <param name="action">
		/// The method to call on button click.
		/// This method receives the instance of the current class (the class this button is attached to)
		/// as an argument of type <see cref="UnityEngine.Object"/>.
		/// </param>
		/// <param name="label">Test displayed on the button.</param>
		/// <param name="widthRel">[optional] Relative width of the button (from 0.0 to 1.0)</param>
		public ButtonAttribute(
			Action<UnityEngine.Object> action, string label, float widthRel
		) : this(action, label, "", widthRel) { }

		/// <summary>
		/// This attribute adds a button before the affected field.
		/// </summary>
		/// <param name="action">
		/// The method to call on button click.
		/// This method receives the instance of the current class (the class this button is attached to)
		/// as an argument of type <see cref="UnityEngine.Object"/>.
		/// </param>
		/// <param name="label">Test displayed on the button.</param>
		/// <param name="widthAbs">[optional] Absolute width of the button (in pixels).</param>
		public ButtonAttribute(
			Action<UnityEngine.Object> action, string label, int widthAbs
		) : this(action, label, "", widthAbs) { }

		#endregion
	}
}