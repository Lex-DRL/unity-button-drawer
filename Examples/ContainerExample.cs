using UnityEngine;

namespace DRL {
	[System.Serializable]
	public class ChildC {
		[SerializeField]
		public float zzz = 0.15f;

		[Button("DoPrint", "")]
		public bool aaa;

		public void DoPrint() {
			Debug.Log(string.Format("{0}: {1}", "ChildC", zzz));
		}
	}


	public class ContainerExample : MonoBehaviour {
		public ChildC cc;

		public ContainerExample() {
			cc = new ChildC();
		}
	}
}