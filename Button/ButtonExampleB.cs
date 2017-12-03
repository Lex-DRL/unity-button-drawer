using UnityEngine;

namespace DRL {
	public class ButtonExampleB : MonoBehaviour {
		public float qqq = 7.1f;
		public float rrr = 171.8f;
		[Button("DoPrint", "")]
		public bool aaa;

		public void DoPrint() {
			Debug.Log(string.Format("{0}: {1}", name, qqq));
		}
	}
}