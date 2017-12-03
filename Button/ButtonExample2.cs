using UnityEngine;

namespace DRL {
	public class ButtonExample2 : ButtonExample1
	{
		public float www = 5.2f;
		[Button("DoPrintSecond", "Print second")]
		public bool bbb;

		[Button("DoPrint", "Print from parent", "The inherited method is called.")]
		public bool ccc;

		[Button("DDD", "Erratic button", 100)]
		public bool ddd;

		[Button("EEE", "Erratic button with tooltip", "WHAAAAT?!")]
		public bool eee;

		public void DoPrintSecond() {
			Debug.Log(string.Format("{0}: {1}", name, www));
		}
	}
}