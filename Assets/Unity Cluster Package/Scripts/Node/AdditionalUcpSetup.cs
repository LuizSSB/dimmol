using UnityEngine;
using System.Collections;

namespace UnityClusterPackage {
	public class AdditionalUcpSetup : MonoBehaviour {
		// Use this for initialization
		void Start () {
			if (GvrViewer.Instance.VRModeEnabled = Node.CurrentNode.NodeScreen.UsesGoogleVr) {
				GameObject.Find("NodeManager").transform.position = new Vector3(0f, -6f);
			}
		}
	}
}
