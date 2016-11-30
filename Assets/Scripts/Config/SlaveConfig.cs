using System;
using System.Xml.Serialization;
using UnityEngine;
using System.IO;

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

		[XmlElement("camera-control")]
		public bool CameraControl {
			get;
			set;
		}

		private const string ConfigFileName = "slave-config.xml";
		private static SlaveConfig sCurrentConfig;
		public static SlaveConfig CurrentConfig {
			get {
				if (sCurrentConfig == null) {
					try {
						var filePath = Path.Combine(
							Application.isMobilePlatform ?
								Application.persistentDataPath :
								Application.streamingAssetsPath,
							ConfigFileName
						);
						using(var reader = new StreamReader(filePath)) {
							var serializer = new XmlSerializer(typeof(SlaveConfig));
							sCurrentConfig = (SlaveConfig)serializer.Deserialize(reader);
						}
					} catch (Exception e) {
						UnityEngine.Debug.Log("Failed to load slave-config.xml: " + e);

						sCurrentConfig = new SlaveConfig() {
							CameraControl = false,
							ShowEnergy = false
						};
					}
				}

				return sCurrentConfig;
			}
		}

		public static void SetSlaveConfigData(SlaveConfig config, string path = null)
		{
			using (var writer = new StreamWriter(path ?? Path.Combine(Application.streamingAssetsPath, ConfigFileName))) {
				var serializer = new XmlSerializer(typeof(SlaveConfig));
				serializer.Serialize(writer, config);
			}

			sCurrentConfig = config;
		}
	}
}

