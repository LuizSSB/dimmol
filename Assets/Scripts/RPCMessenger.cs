using System;
using UnityEngine;
using UnityClusterPackage;
using System.Linq;

namespace AssemblyCSharp
{
	public class RPCMessenger : MonoBehaviour
	{
		private NetworkView mNetworkView;

		private string[] mComplexObjectParts = null;
		private const int ComplexObjectPartLength = 3950;
		
		void Start() {
			mNetworkView = GetComponent<NetworkView>();
		}

		public static RPCMessenger GetCurrent() {
			var gameObjects = GameObject.FindGameObjectsWithTag("RPCMessenger");

			if (gameObjects.Length > 1) {
				Debug.Log("More than one RPCMessenger gameobject in scene. Will work only with the first one");
			}

			return gameObjects[0].GetComponent<RPCMessenger>();
		}

		public void SendComplexObject(object obj, Type callbackType, string callbackMethod) {
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

				mNetworkView.RPC("ReceiveString", RPCMode.All, Node.CurrentNode.Id, part);
			}
		}

		[RPC]
		public void ReceiveString(int nodeId, string value) {
			var indexOfDollar = value.IndexOf("$");
			var controlParts = value.Substring(0, indexOfDollar);
			var control = ControlData.Deserialize(controlParts);

			if (mComplexObjectParts == null) {
				mComplexObjectParts = new string[control.TotalParts];
			}

			mComplexObjectParts[control.CurrentPart] = value.Substring(indexOfDollar + 1);

			if (!mComplexObjectParts.Any(p => p == null)) {
				var serialized = string.Join(string.Empty, mComplexObjectParts);
				var obj = JsonUtility.FromJson(serialized, control.ObjectType);
				var method = control.CallbackType.GetMethod(control.CallbackMethodName);
				method.Invoke(null, new object[] {nodeId, obj});
				mComplexObjectParts = null;
			}
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
				return string.Format(
					"{0}/{1}/{2}/{3}/{4}",
					_CallbackTypeName,
					CallbackMethodName,
					_ObjectTypeName,
					CurrentPart,
					TotalParts
				);
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

