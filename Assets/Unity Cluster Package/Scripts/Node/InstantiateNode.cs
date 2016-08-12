using UnityEngine;
using System.Collections;
using System;

namespace UnityClusterPackage {
	
	public class InstantiateNode : MonoBehaviour {
		
		public GameObject MultiProjectionCamera;
		
		void Awake() {
			Network.sendRate = 100;

			if ( NodeInformation.type.Equals("master") )
			{
				Network.proxyIP = NodeInformation.serverIp;
				bool useNat = Network.HavePublicAddress();
				Network.InitializeServer( NodeInformation.nodes, NodeInformation.serverPort, useNat );				
				var networkedCamera = Network.Instantiate(
					MultiProjectionCamera,
					transform.position,
					transform.rotation,
					0
				) as GameObject;
				networkedCamera.transform.parent = this.transform;
			}
			else if ( NodeInformation.IsSlave )
			{
				Network.Connect( NodeInformation.serverIp, NodeInformation.serverPort );
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
		}
		
		void OnFailedToConnect(NetworkConnectionError error)
		{
			Debug.Log( "Could not connect to server: " + error );
		}
		
		void OnDestroy() {
			Debug.Log ("Destroying node: " + NodeInformation.name);
			if ( NodeInformation.type.Equals("master") )
			{
				Network.Disconnect();
			}
			else if ( NodeInformation.IsSlave )
			{
				Network.CloseConnection( Network.player, true );                
			}
		}
		
	}
}