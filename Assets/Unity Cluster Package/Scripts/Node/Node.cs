using System.Xml.Serialization;
using System.Collections;
using System.IO;

namespace UnityClusterPackage
{
	[System.Serializable]
	[XmlRoot("node")]
	public class Node {
		[XmlAttribute("type")]
		public Type NodeType { get; set; }

		[XmlAttribute("name")]
		public string Name { get; set; }

		[XmlAttribute("id")]
		public int Id { get; set; }

		[XmlAttribute("nodes")]
		public int Nodes { get; set; }

		[XmlElement("server")]
		public Server NodeServer { get; set; }

		[XmlElement("screen")]
		public Screen NodeScreen { get; set; }

		public bool IsSlave { get { return NodeType == Type.slave; } }

		private static string NodeInformationFilePath {
			get {
				return Path.Combine(UnityEngine.Application.streamingAssetsPath, "node-config.xml");
			}
		}
		public static Node CurrentNode {
			get;
			private set;
		}
		static Node()
		{
			try {
				using(var reader = new StreamReader(NodeInformationFilePath)) {
					var serializer = new XmlSerializer(typeof(Node));
					CurrentNode = (Node) serializer.Deserialize(reader);
				}
			} catch(System.Exception e) {
				UnityEngine.Debug.Log("Failed to load node-config.xml: " + e);

				CurrentNode = new Node {
					Id = 0,
					Name = "Node",
					Nodes = 0,
					NodeType = Node.Type.master,
					NodeServer = new Node.Server() {
						Ip = "192.168.1.100",
						Port = 2500
					},
					NodeScreen = new Node.Screen() {
						ScreenEye = Eye.left,
						Pa = new Node.Point() { X = -8f, Y = -4f, Z = 0f },
						Pb =  new Node.Point() { X = 8f, Y = -4f, Z = 0f },
						Pc = new Node.Point() { X = -8f, Y = 5f, Z = 0f },
						Pe = Node.Point.Empty()
					}
				};
			}
		}
		// Luiz: Could actually be a setter, however this way emphasizes the file processing
		// that happens when setting de the node data.
		public static void SetNodeDataUp(Node node, string path = null)
		{
			using (var writer = new StreamWriter(path ?? NodeInformationFilePath)) {
				var serializer = new XmlSerializer(typeof(Node));
				serializer.Serialize(writer, node);
			}

			CurrentNode = node;
		}

		public enum Type {
			// Lower case for us to not have a problem when deserializing
			master, slave
		}

		[System.Serializable]
		public class Server {
			[XmlAttribute("ip")]
			public string Ip { get; set; }

			[XmlAttribute("port")]
			public int Port { get; set; }
		}

		[System.Serializable]
		public class Screen {
			[XmlAttribute("stereo")]
			public bool Stereo { get; set; }

			[XmlAttribute("eye")]
			public Eye ScreenEye { get; set; }

			[XmlElement("pa")]
			public Point Pa { get; set; }

			[XmlElement("pb")]
			public Point Pb { get; set; }

			[XmlElement("pc")]
			public Point Pc { get; set; }

			[XmlElement("pe")]
			public Point Pe { get; set; }
		}
			
		public enum Eye {
			// Luiz: Again, lower case because of deserialization
			left, right
		}

		[System.Serializable]
		public class Point {
			public static Point Empty() {
				return new Point { X = 0, Y = 0f, Z = 0f };
			}

			[XmlAttribute("x")]
			public float X { get; set; }

			[XmlAttribute("y")]
			public float Y { get; set; }

			[XmlAttribute("z")]
			public float Z { get; set; }

			public UnityEngine.Vector3 ToVector3() {
				return new UnityEngine.Vector3(X, Y, Z);
			}
		}
	}
}
