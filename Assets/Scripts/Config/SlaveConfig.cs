using System;
using System.Xml.Serialization;
using UnityEngine;

namespace Config
{
	[Serializable]
	[XmlRoot("slave-config")]
	public class SlaveConfig
	{		
		[XmlElement("show-energy")]
		public bool ShowEnergy {
			get;
			set;
		}

		[XmlElement("has-camera-control")]
		public bool CameraControl {
			get;
			set;
		}

		private const string ConfigFileName = "slave-config.xml";
		static SlaveConfig() {
			if (UnityClusterPackage.NodeInformation.IsSlave) {
				try {
					var filePath = System.IO.Path.Combine(Application.streamingAssetsPath, ConfigFileName);
					var reader = new System.IO.StreamReader(filePath);
					var serializer = new XmlSerializer(typeof(SlaveConfig));
					Instance = (SlaveConfig)serializer.Deserialize(reader);
				} catch (Exception e) {
					UnityEngine.Debug.Log(e);
					Instance = new SlaveConfig() {
						CameraControl = false,
						ShowEnergy = false
					};
				}
			} else {
				// Luiz: In case this is the master node, everything's enabled
				Instance = new SlaveConfig() {
					CameraControl = true,
					ShowEnergy = true
				};
			}
		}

		public static readonly SlaveConfig Instance;
		private SlaveConfig() {}
	}
}

