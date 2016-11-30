using UnityEngine;
using System.Collections;
using System;

namespace UnityClusterPackage {
	
	public class InstantiateNode : MonoBehaviour {
		
		public GameObject MultiProjectionCamera;
		
		void Awake() {
			Network.sendRate = 100;

			if ( Node.CurrentNode.IsHostNode)
			{
				Network.proxyIP = Node.CurrentNode.NodeServer.Ip;
				bool useNat = Network.HavePublicAddress();
				Network.InitializeServer( Node.CurrentNode.Nodes, Node.CurrentNode.NodeServer.Port, useNat );				
				var networkedCamera = Network.Instantiate(
					MultiProjectionCamera,
					transform.position,
					transform.rotation,
					0
				) as GameObject;
				networkedCamera.transform.parent = transform;
			}
			else if ( Node.CurrentNode.IsChildNode )
			{
				Network.Connect( Node.CurrentNode.NodeServer.Ip, Node.CurrentNode.NodeServer.Port );
				StartCoroutine("AdoptSlaveCamera");
			}			
		}

		IEnumerator AdoptSlaveCamera() {
			GameObject camera;
			while((camera = GameObject.FindGameObjectWithTag("MainCamera")) == null) {
				yield return new WaitForEndOfFrame();
			}
			camera.transform.parent = transform;
		}
		
		void OnServerInitialized()
		{
			Debug.Log( "Server initialized and ready." );
		}
		
		void OnPlayerConnected(NetworkPlayer player) {
			Debug.Log( "Node connected " + player.ipAddress + ":" + player.port );
		}
		
		void OnPlayerDisconnected( NetworkPlayer node ) {
			Debug.Log( "Clean up after player " + node );
			Network.RemoveRPCs(node);
			Network.DestroyPlayerObjects(node);
		}
		
		void OnConnectedToServer()
		{
			Debug.Log( "Connected to server. " +  GameObject.FindGameObjectWithTag("MainCamera"));

			StartCoroutine (check());
		}

		IEnumerator check()
		{
			while (Camera.main == null)
				yield return new WaitForEndOfFrame ();

			GameObject.FindGameObjectWithTag("MainCamera").transform.parent = transform;
		}
		
		void OnFailedToConnect(NetworkConnectionError error)
		{
			Debug.Log( "Could not connect to server: " + error );
		}
		
		void OnDestroy() {
			Debug.Log ("Destroying node: " + Node.CurrentNode.Name);
			if ( Node.CurrentNode.IsHostNode )
			{
				Network.Disconnect();
			}
			else if ( Node.CurrentNode.IsChildNode )
			{
				// Luiz: Gotta be inside a try/catch because if the server disconnects before this object is destroyed
				// there will be no connection to be closed.
				try {
					Network.CloseConnection( Network.player, true );
				} catch { }
			}
		}
		
	}
}