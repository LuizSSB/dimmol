using UnityEngine;
using System.Collections;

namespace UnityClusterPackage {
	public class AdditionalUcpSetup : MonoBehaviour {
		// Use this for initialization
		void Start () {
			GvrViewer.Instance.VRModeEnabled = Node.CurrentNode.NodeScreen.UsesGoogleVr;

			if (GvrViewer.Instance.VRModeEnabled) {
				GameObject.Find("NodeManager").transform.position = new Vector3(0f, 0f, 0f);
			}
		}
	}
}
