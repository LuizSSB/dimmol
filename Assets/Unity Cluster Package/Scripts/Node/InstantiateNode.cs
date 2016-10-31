using UnityEngine;
using System.Collections;
using System;

namespace UnityClusterPackage {
	
	public class InstantiateNode : MonoBehaviour {
		
		public GameObject MultiProjectionCamera;
		
		void Awake() {
			Network.sendRate = 100;

			if ( Node.CurrentNode.NodeType == Node.Type.master )
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
				networkedCamera.transform.parent = this.transform;
			}
			else if ( Node.CurrentNode.IsSlave )
			{
				Network.Connect( Node.CurrentNode.NodeServer.Ip, Node.CurrentNode.NodeServer.Port );
			}			
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

			Camera.main.transform.parent = transform;
			Camera.main.backgroundColor = Color.black;
		}
		
		void OnFailedToConnect(NetworkConnectionError error)
		{
			Debug.Log( "Could not connect to server: " + error );
		}
		
		void OnDestroy() {
			Debug.Log ("Destroying node: " + Node.CurrentNode.Name);
			if ( Node.CurrentNode.NodeType == Node.Type.master )
			{
				Network.Disconnect();
			}
			else if ( Node.CurrentNode.IsSlave )
			{
				Network.CloseConnection( Network.player, true );                
			}
		}
		
	}
}