using UnityEngine;

namespace DRL {
	public class ButtonExample1 : MonoBehaviour {
		public float qqq = 7.1f;
		[Button("DoPrint", "")]
		public bool aaa;

		public void DoPrint() {
			Debug.Log(string.Format("{0}: {1}", name, qqq));
		}
	}
}