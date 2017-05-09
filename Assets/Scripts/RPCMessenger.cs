using System;
using UnityEngine;
using UnityClusterPackage;
using System.Linq;
using System.Collections.Generic;
using Config;
using UI;

namespace AssemblyCSharp
{
	public class RPCMessenger : MonoBehaviour
	{
		private const int ComplexObjectPartLength = 3950;

		private string[] mComplexObjectParts = null;
		private NetworkView Networking {
			get {
				return GetComponent<NetworkView>();
			}
		}
		
		void Start() {
			// Luiz: Configuring RPC calls for when stuff changes
			if (Node.CurrentNode.HasPermission(NodePermission.MenuControl) | Node.CurrentNode.IsHostNode) {
				GUIDisplay.Instance.Cleared += HandleUICleared;
			}
			if (Node.CurrentNode.HasPermission(NodePermission.MenuControl)) {
				ChangeManager.MethodInvoked += HandleChangeManagerMethodInvoked;
				ChangeManager.PropertyChanged += HandleChangeManagerPropertyChanged;
			}
		}

		private string mClientAddress;
		void OnConnectedToServer() {
			if (Node.CurrentNode.NodeType == Node.Type.client) {
				Networking.RPC(
					"ReceiveClientNode",
					RPCMode.Server,
					Node.CurrentNode.Id,
					Network.player.ipAddress,
					Network.player.port
				);
			}
		}
		void OnPlayerDisconnected(NetworkPlayer player) {
			if(CombineIpAndPort(player.ipAddress, player.port) == mClientAddress) {
				mClientAddress = null;
				GUIDisplay.Instance.Clear(false);
			}
		}
		void OnDisconnectedFromServer(NetworkDisconnection info) {
			GUIDisplay.Instance.Clear(true);
		}
		[RPC]
		void ReceiveClientNode(int clientId, string clientIp, int clientPort) {
			mClientAddress = CombineIpAndPort(clientIp, clientPort);
		}
		private static string CombineIpAndPort(string ip, int port) {
			return ip + ":" + port;
		}

		public static RPCMessenger GetCurrent() {
			var gameObjects = GameObject.FindGameObjectsWithTag("RPCMessenger");

			if (gameObjects.Length > 1) {
				Debug.Log("More than one RPCMessenger gameobject in scene. Will work only with the first one");
			}

			return gameObjects.Length > 0 ? gameObjects[0].GetComponent<RPCMessenger>() : null;
		}

		void HandleChangeManagerPropertyChanged (object sender, PropertyEventArgs e)
		{
			DoOnMainThread.AddAction(delegate {
				var rpcData = GetRPCData(e.NewValue, "Property");
				Networking.RPC(
					rpcData.HandlerName,
					RPCMode.All,
					Node.CurrentNode.Id,
					e.TypeName,
					e.PropertyName,
					e.NewValue.GetType().FullName,
					rpcData.Data
				);
			});
		}

		void HandleChangeManagerMethodInvoked (object sender, MethodParamEventArgs e)
		{
			var rpcData = GetRPCData(e.Param, "Method");
			Networking.RPC(
				rpcData.HandlerName,
				RPCMode.All,
				Node.CurrentNode.Id,
				e.TypeName,
				e.MethodName,
				(e.Param ?? new object()).GetType().FullName,
				rpcData.Data
			);
		}

		void HandleUICleared (object sender, ClearingEventArgs e)
		{
			if(e.ExitingScene) {
				ChangeManager.MethodInvoked -= HandleChangeManagerMethodInvoked;
				ChangeManager.PropertyChanged -= HandleChangeManagerPropertyChanged;
				GUIDisplay.Instance.Cleared -= HandleUICleared;
			} else {
				Networking.RPC("Clear", RPCMode.All, Node.CurrentNode.Id);
			}
		}

		public void SendComplexObject(object obj, Type callbackType, string callbackMethod) {
			new System.Threading.Thread(() => {
				ModalUtility.SetLoadingHud(true, "Sending to cluster nodes");

				var objSerialized = JsonUtility.ToJson(obj);
				var control = new ControlData {
					CallbackType = callbackType,
					CallbackMethodName = callbackMethod,
					ObjectType = obj.GetType(),
					TotalParts = (int) System.Math.Ceiling (objSerialized.Length / (double) ComplexObjectPartLength)
				};

				for (int idx = 0, idxPart = 0;
					idx < objSerialized.Length;
					idx += ComplexObjectPartLength, ++idxPart) {

					control.CurrentPart = idxPart;

					var part = control +
						"$" +
						objSerialized.Substring(
							idx,
							(int) System.Math.Min(ComplexObjectPartLength, objSerialized.Length - idx)
						);

					DoOnMainThread.AddAction(() => {
						Networking.RPC("ReceiveString", RPCMode.All, Node.CurrentNode.Id, part);
					});
				}

				ModalUtility.SetLoadingHud(false);
			}).Start();
		}

		[RPC]
		public void ReceiveString(int nodeId, string value) {
			if (!Node.CurrentNode.HasPermission(NodePermission.MenuControl)) {
				ModalUtility.SetLoadingHud(true, "Receiving data");
			}

			var indexOfDollar = value.IndexOf("$");
			var controlParts = value.Substring(0, indexOfDollar);
			var control = ControlData.Deserialize(controlParts);

			if (mComplexObjectParts == null) {
				mComplexObjectParts = new string[control.TotalParts];
			}

			mComplexObjectParts[control.CurrentPart] = value.Substring(indexOfDollar + 1);

			if (!mComplexObjectParts.Any(p => p == null)) {
				new System.Threading.Thread(() => {
					ModalUtility.SetLoadingHud(true, "Parsing received data");
					var serialized = string.Join(string.Empty, mComplexObjectParts);
					var obj = JsonUtility.FromJson(serialized, control.ObjectType);
					var method = control.CallbackType.GetMethod(control.CallbackMethodName);
					mComplexObjectParts = null;

					ModalUtility.SetLoadingHud(false);
					DoOnMainThread.AddAction(() => {
						method.Invoke(null, new object[] { nodeId, obj });
					});					
				}).Start();
			}
		}

		[RPC]
		public void DieHard(int senderNodeId)
		{
			if (Node.CurrentNode.Id != senderNodeId) {
				GUIDisplay.Instance.DieHard();
			}
		}

		[RPC]
		public void Clear(int senderNodeId)
		{
			if (Node.CurrentNode.Id != senderNodeId) {
				GUIDisplay.Instance.Clear (false);
			}
		}

		[RPC]
		public void HandleInt32Method(int senderNodeId, string typeName, string methodName, string paramTypeName, int param)
		{
			HandleMethodInvoked (senderNodeId, typeName, methodName, param);
		}
		[RPC]
		public void HandleSingleMethod(int senderNodeId, string typeName, string methodName, string paramTypeName, float param)
		{
			HandleMethodInvoked (senderNodeId, typeName, methodName, param);
		}
		[RPC]
		public void HandleBooleanMethod(int senderNodeId, string typeName, string methodName, string paramTypeName, bool param)
		{
			HandleMethodInvoked (senderNodeId, typeName, methodName, param);
		}
		[RPC]
		public void HandleStringMethod(int senderNodeId, string typeName, string methodName, string paramTypeName, string param)
		{
			HandleMethodInvoked (senderNodeId, typeName, methodName, param);
		}
		[RPC]
		public void HandleObjectMethod(int senderNodeId, string typeName, string methodName, string paramTypeName, string paramSerialized)
		{
			var param = paramSerialized == null ? null : JsonUtility.FromJson (paramSerialized, Type.GetType (paramTypeName));
			HandleMethodInvoked (senderNodeId, typeName, methodName, param);
		}
		private void HandleMethodInvoked(int senderNodeId, string typeName, string methodName, object param)
		{
			Debug.Log ("### Method " + typeName + "." + methodName + "(" + param + ")");

			if (senderNodeId != Node.CurrentNode.Id) {
				var type = Type.GetType (typeName);
				var method = type.GetMethod (methodName);
				var singleton = GetTypeSingletonIfAny(type, method);
				if (param != null) {
					method.Invoke (singleton, new [] { param });
				} else {
					method.Invoke (singleton, null);
				}
			}
		}

		[RPC]
		public void HandleInt32Property(int senderNodeId, string typeName, string propertyName, string newValueTypeName, int param)
		{
			HandlePropertyChanged (senderNodeId, typeName, propertyName, param);
		}
		[RPC]
		public void HandleSingleProperty(int senderNodeId, string typeName, string propertyName, string newValueTypeName, float param)
		{
			HandlePropertyChanged (senderNodeId, typeName, propertyName, param);
		}
		[RPC]
		public void HandleBooleanProperty(int senderNodeId, string typeName, string propertyName, string newValueTypeName, bool param)
		{
			HandlePropertyChanged (senderNodeId, typeName, propertyName, param);
		}
		[RPC]
		public void HandleStringProperty(int senderNodeId, string typeName, string propertyName, string newValueTypeName, string param)
		{
			HandlePropertyChanged (senderNodeId, typeName, propertyName, param);
		}
		[RPC]
		public void HandleObjectProperty(int senderNodeId, string typeName, string propertyName, string newValueTypeName, string newValueSerialized)
		{
			var param = JsonUtility.FromJson (newValueSerialized, Type.GetType (newValueTypeName));
			HandlePropertyChanged (senderNodeId, typeName, propertyName, param);
		}
		private void HandlePropertyChanged(int senderNodeId, string typeName, string propertyName, object newValue) {
			Debug.Log (string.Format ("### Property changed - {0}:{1} = {2}", typeName, propertyName, newValue));

			if (Node.CurrentNode.Id != senderNodeId) {
				var type = Type.GetType (typeName);
				var property = type.GetProperty (propertyName);
				var singleton = GetTypeSingletonIfAny(type, property.GetGetMethod());
				property.SetValue (singleton, newValue, null);
			}
		}

		private object GetTypeSingletonIfAny(Type ofType, System.Reflection.MethodInfo forMethod)
		{
			if (forMethod.IsStatic) {
				return null;
			} else {
				var instanceMember = ofType.GetProperty("Instance");
				if (instanceMember == null) {
					throw new ArgumentException("Type " + ofType.Name + " has no singleton property named 'Instance' for method: " + forMethod.Name);
				}
				return instanceMember.GetValue(null, null);
			}
		}

		private static HashSet<Type> sRPCHandledTypes = new HashSet<Type> {
			typeof(int), typeof(float), typeof(bool), typeof(string)
		};

		private static RPCData GetRPCData(object forValue, string handlerSuffix)
		{
			var valueType = (forValue ?? new object()).GetType ();
			if (sRPCHandledTypes.Contains (valueType)) {
				return new RPCData {
					Data = forValue,
					HandlerName = "Handle" + valueType.Name + handlerSuffix
				};
			} else {
				return new RPCData {
					Data = JsonUtility.ToJson(forValue),
					HandlerName = "HandleObject" + handlerSuffix
				};
			}
		}

		private class RPCData {
			public object Data;
			public string HandlerName;
		}

		private class ControlData
		{
			private	string _CallbackTypeName;
			private Type _CallbackType;
			public Type CallbackType {
				get { return _CallbackType; }
				set {
					_CallbackType = value;
					_CallbackTypeName = value.FullName;
				}
			}

			public string CallbackMethodName {
				get;
				set;
			}

			private string _ObjectTypeName;
			private Type _ObjectType;
			public Type ObjectType {
				get { return _ObjectType; }
				set {
					_ObjectType = value;
					_ObjectTypeName = value.FullName;
				}
			}

			public int TotalParts {
				get;
				set;
			}

			public int CurrentPart {
				get;
				set;
			}

			public override string ToString()
			{
				var value = string.Join(
					            "/",
					            new [] {
									_CallbackTypeName,
									CallbackMethodName,
									_ObjectTypeName,
									CurrentPart.ToString(),
									TotalParts.ToString()
								}
				            );
				return value;
			}

			public static ControlData Deserialize(string deserialized) {
				var valueParts = deserialized.Split('/');
				ControlData obj = new ControlData {
					CallbackType = Type.GetType(valueParts[0]),
					CallbackMethodName = valueParts[1],
					ObjectType = Type.GetType(valueParts[2]),
					CurrentPart = int.Parse(valueParts[3]),
					TotalParts = int.Parse(valueParts[4])
				};
				return obj;
			}
		}
	}
}
	