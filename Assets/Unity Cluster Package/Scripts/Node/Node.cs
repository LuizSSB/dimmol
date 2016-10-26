using System.Xml.Serialization;
using System.Collections;

// Luiz: TODO use proper deserialization instead of that NodeInformation class.
//[XmlRoot("node")]
//public class Node {
//	[XmlAttribute("type")]
//	public string Type {
//		get;
//		set;
//	}
//
//	[XmlAttribute("name")]
//	public string Name {
//		get;
//		set;
//	}
//
//	[XmlAttribute("id")]
//	public int Id {
//		get;
//		set;
//	}
//
//	[XmlAttribute("nodes")]
//	public int Nodes {
//		get;
//		set;
//	}
//
//	[XmlElement("server")]
//	public Server NodeServer {
//		get;
//		set;
//	}
//
//	[XmlElement("screen")]
//	public Screen NodeScreen {
//		get;
//		set;
//	}
//
//	public class Server {
//		[XmlAttribute("ip")]
//		public string Ip {
//			get;
//			set;
//		}
//
//		[XmlAttribute("port")]
//		public int Port {
//			get;
//			set;
//		}
//	}
//
//	public class Screen {
//		[XmlAttribute("stereo")]
//		public bool Stereo {
//			get;
//			set;
//		}
//
//		[XmlAttribute("eye")]
//		public string Eye {
//			get;
//			set;
//		}
//
//		[XmlElement("pa")]
//		public Point Pa {
//			get;
//			set;
//		}
//
//		[XmlElement("pb")]
//		public Point Pb {
//			get;
//			set;
//		}
//
//		[XmlElement("pc")]
//		public Point Pc {
//			get;
//			set;
//		}
//
//		[XmlElement("pe")]
//		public Point Pe {
//			get;
//			set;
//		}
//	}
//
//	public class Point {
//		[XmlAttribute("x")]
//		public int X {
//			get;
//			set;
//		}
//
//		[XmlAttribute("y")]
//		public int Y {
//			get;
//			set;
//		}
//
//		[XmlAttribute("z")]
//		public int Z {
//			get;
//			set;
//		}
//	}
//}
