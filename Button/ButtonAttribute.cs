﻿using UnityEngine;

namespace DRL
{
	/// <summary>
	/// This attribute adds a button before the affected field.
	/// You also need to define a static method which will actually perform
	/// whatever you need to be done on button click.
	/// This method takes exactly one argument of type <see cref="UnityEngine.Object"/>.
	/// In the method, you'll get your class instance itself via this argument,
	/// so you may need to cast it back tou your specific class type.
	/// This static function is passed as the 1st required parameter for this attribute.
	/// </summary>
	public class ButtonAttribute : PropertyAttribute {
		public readonly string Method;
		public readonly string Label;
		public readonly string Tooltip;

		public readonly float Width;

		/// <summary>
		/// This attribute adds a button before the affected field.
		/// </summary>
		/// <param name="method">
		/// The name of the method called on button click.
		/// The method needs to exist as a member of the <see cref="Object"/>-inferited class you add the button to.
		/// </param>
		/// <param name="label">Text displayed on the button.</param>
		/// <param name="tooltip">[optional] Text displayed in the popup on mouse hover.</param>
		/// <param name="width">[optional] Width of the button:<para />
		/// less then 0.0 - use the default width (fit to the text length);<para />
		/// from 0.0 to 2.0 - use as relative width, as the fraction of default control size;<para />
		/// more than 2.0 - use as absolute width, in pixels.<para />
		/// The width is clamped to available space in the inspector. So values from 1.0 to 2.0
		/// essentially stretch the button to the entire inspector width.
		/// </param>
		public ButtonAttribute(
			string method, string label, string tooltip="", float width=-1.0f
		) {
			Method = method;
			Label = label;
			Tooltip = tooltip;
			Width = width;
		}

	}
}