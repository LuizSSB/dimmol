using UnityEngine;
using System.Collections;

namespace UnityClusterPackage {
	public class AdditionalUcpSetup : MonoBehaviour {
		// Use this for initialization
		void Start () {
			GvrViewer.Instance.VRModeEnabled = Node.CurrentNode.NodeScreen.UsesGoogleVr;
			GvrViewer.Instance.enabled = Node.CurrentNode.NodeScreen.TracksHead;

			if (GvrViewer.Instance.VRModeEnabled) {
				GameObject.Find("NodeManager").transform.position = new Vector3(0f, 0f, 0f);
			}

			if (!GvrViewer.Instance.enabled) {
				var head = GameObject.FindObjectOfType<GvrHead>();
				head.trackPosition = false;
				head.trackRotation = false;
				head.target = transform;
				GvrViewer.Instance.Recenter();
			}
		}
	}
}
