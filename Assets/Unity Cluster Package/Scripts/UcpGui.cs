using System;
using UnityEngine;

namespace UnityClusterPackage {
	public abstract class UcpGuiAction : MonoBehaviour
	{
		public abstract void NodeSetUp();
	}

	public class UcpGui : MonoBehaviour {
		public UcpGuiAction DoAfterSetUp;
		public event Action<Node> NodeSetUp;

		public Node Node { get; private set; }
		bool m_IsMobileDevice;
		string m_ServerPort;
		StringNodePoint m_Pa;
		StringNodePoint m_Pb;
		StringNodePoint m_Pc;
		StringNodePoint m_Pe;

		string m_ErrorMessages = string.Empty;

		protected static readonly Rect MainAreaFrame = new Rect (
			                                               (Screen.width / 3f / 2f),
			                                               5,
			                                               (Screen.width / 1.5f),
			                                               Screen.height - 5
		                                               );

		// Use this for initialization
		protected virtual void Start () {
			Node = Node.CurrentNode;

			m_ServerPort = Node.NodeServer.Port.ToString();
			m_Pa = ConvertNodePoint(Node.NodeScreen.Pa);
			m_Pb = ConvertNodePoint(Node.NodeScreen.Pb);
			m_Pc = ConvertNodePoint(Node.NodeScreen.Pc);
			m_Pe = ConvertNodePoint(Node.NodeScreen.Pe);
		}

		protected void OnGUI() {
			GUISkin mySkin = GUI.skin;
			mySkin.box.fontSize =
				mySkin.button.fontSize =
					mySkin.horizontalSlider.fontSize =
						mySkin.textArea.fontSize =
							mySkin.textField.fontSize =
								mySkin.toggle.fontSize =
									mySkin.window.fontSize =
										mySkin.label.fontSize = Node.CurrentNode.GuiSize;
			mySkin.horizontalSlider.fixedHeight =
				mySkin.horizontalSliderThumb.fixedHeight = Mathf.Max(20f, Node.CurrentNode.GuiSize);

			GUILayout.BeginArea(MainAreaFrame);
			{
				GUILayout.BeginHorizontal();
				{
					GUILayout.BeginVertical(MakeWidthOption(.33f));
					{
						GUILayout.Label("NODE SETTINGS", MakeWidthOption(.33f));
						m_IsMobileDevice = IntToBool(GUILayout.SelectionGrid(
							BoolToInt(m_IsMobileDevice),
							new []{ "Desktop", "Mobile Device" },
							2
						));
					}
					GUILayout.EndVertical();

					GUILayout.BeginVertical();
					{
						GUILayout.Label("GUI scale");
						Node.CurrentNode.GuiSize = (int)GUILayout.HorizontalSlider(
							Node.CurrentNode.GuiSize,
							10f,
							40f
						);
					}
					GUILayout.EndVertical();
				}
				GUILayout.EndHorizontal();

				// General stuff
				GUILayout.BeginHorizontal();
				{
					// Node type
					GUILayout.BeginVertical(MakeWidthOption(.33f));
					{
						GUILayout.Label("Node type");
						Node.NodeType = (Node.Type)GUILayout.SelectionGrid(
							(int)Node.NodeType,
							Enum.GetNames(typeof(Node.Type)),
							2
						);
					}
					GUILayout.EndVertical();

					// Id
					GUILayout.BeginVertical(MakeWidthOption(.33f));
					{
						GUILayout.Label("Node Id");
						var idString = GUILayout.TextField(Node.Id.ToString());
						int idInt;
						if (int.TryParse(idString, out idInt)) {
							Node.Id = idInt;
						}
					}
					GUILayout.EndVertical();

					// Name
					GUILayout.BeginVertical(MakeWidthOption(.33f));
					{
						GUILayout.Label("Node name");
						Node.Name = GUILayout.TextField(Node.Name);
					}
					GUILayout.EndVertical();
				}
				GUILayout.EndHorizontal();

				// Server Data

				GUILayout.BeginHorizontal();
				{
					if (Node.NodeType.IsHost()) {
						// Nodes
						GUILayout.BeginVertical(MakeWidthOption(.32f));
						{
							GUILayout.Label("Number of nodes");
							var nodes = GUILayout.TextField(Node.Nodes.ToString());
							int nodesInt;
							if (int.TryParse(nodes, out nodesInt)) {
								Node.Nodes = nodesInt;
							}
						}
						GUILayout.EndVertical();
					} else {
						// Server Address
						GUILayout.BeginVertical(MakeWidthOption(.33f));
						{
							GUILayout.Label("Host IP");
							Node.NodeServer.Ip = GUILayout.TextField(Node.NodeServer.Ip);
						}
						GUILayout.EndVertical();

						// Server Port
						GUILayout.BeginVertical(MakeWidthOption(.32f));
						{
							GUILayout.Label("Host Port");
							m_ServerPort = GUILayout.TextField(m_ServerPort);
						}
						GUILayout.EndVertical();
					}
				}
				GUILayout.EndHorizontal();

				if (m_IsMobileDevice) {
					// Screen data
					GUILayout.BeginHorizontal();
					{
						// Whether it is using Google VR
						Node.NodeScreen.UsesGoogleVr = GUILayout.Toggle(
							Node.NodeScreen.UsesGoogleVr,
							"Google VR",
							MakeWidthOption(.33f)
						);

						// Whether it uses GoogleVR's head tracking
						if (Node.NodeScreen.UsesGoogleVr) {
							Node.NodeScreen.TracksHead = true;
						} else {
							Node.NodeScreen.TracksHead = GUILayout.Toggle(
								Node.NodeScreen.TracksHead,
								"Gyroscope / Tracks head",
								MakeWidthOption(.33f)
							);
						}
					}
					GUILayout.EndHorizontal();

				} else {
					GUILayout.BeginHorizontal();
					{
						// Is Stereo?
						Node.NodeScreen.Stereo = GUILayout.Toggle(
							Node.NodeScreen.Stereo,
							"Stereo",
							MakeWidthOption(.33f)
						);

						if (Node.NodeScreen.Stereo) {
							// Which eye stereo uses
							Node.NodeScreen.ScreenEye = (Node.Eye)GUILayout.SelectionGrid(
								(int)Node.NodeScreen.ScreenEye,
								new [] { "Left eye", "Right eye" },
								2,
								MakeWidthOption(.33f)
							);
						}							
					}
					GUILayout.EndHorizontal();
					// Points
					DrawPoints("Pa", m_Pa);
					DrawPoints("Pb", m_Pb);
					DrawPoints("Pc", m_Pc);
					DrawPoints("Pe", m_Pe);
				}

				// Buttons
				if (GUILayout.Button("Confirm")) {
					CheckAndSave();
				}
				if (GUILayout.Button("Confirm and save node-config.xml")) {
					CheckAndSave(() => Node.SetNodeDataUp(Node));
				}

				GuiCustomizationHook();

				// Error messages
				GUILayout.Label(m_ErrorMessages);

			}
			GUILayout.EndArea();
		}

		protected virtual void GuiCustomizationHook() { }

		static void DrawPoints(string label, StringNodePoint point)
		{
			GUILayout.BeginHorizontal(); {
				point.X = DrawPoint(label + " X", point.X);
				point.Y = DrawPoint(label + " Y", point.Y);
				point.Z = DrawPoint(label + " Z", point.Z, .32f);
			} GUILayout.EndHorizontal();
		}

		static string DrawPoint(string label, string pointValue, float fieldWidth = .33f) {
			GUILayout.BeginVertical(MakeWidthOption(fieldWidth)); {
				GUILayout.Label(label);
				pointValue = GUILayout.TextField(pointValue);
			} GUILayout.EndVertical();

			return pointValue;
		}

		void CheckAndSave(Action action = null)
		{
			var msgs = new System.Text.StringBuilder();
			bool valid = true;

			int port;
			if (!int.TryParse(m_ServerPort, out port)) {
				msgs.AppendLine("INVALID SERVER PORT");
				valid = false;
			} else
				Node.NodeServer.Port = port;

			if(m_IsMobileDevice) {
				var defaultNode = Node.Default();
				Node.NodeScreen.Pa = defaultNode.NodeScreen.Pa;
				Node.NodeScreen.Pb = defaultNode.NodeScreen.Pb;
				Node.NodeScreen.Pc = defaultNode.NodeScreen.Pc;
				Node.NodeScreen.Pe = defaultNode.NodeScreen.Pe;

			} else {
				if (!ValidatePoint(m_Pa, Node.NodeScreen.Pa)) {
					msgs.AppendLine("INVALID PA COORDINATES");
					valid = false;
				}

				if (!ValidatePoint(m_Pb, Node.NodeScreen.Pb)) {
					msgs.AppendLine("INVALID PB COORDINATES");
					valid = false;
				}

				if (!ValidatePoint(m_Pc, Node.NodeScreen.Pc)) {
					msgs.AppendLine("INVALID PC COORDINATES");
					valid = false;
				}

				if (!ValidatePoint(m_Pe, Node.NodeScreen.Pe)) {
					msgs.AppendLine("INVALID PE COORDINATES");
					valid = false;
				}
			}

			if (valid) {
				try {
					if (action != null)
						action();

					if (DoAfterSetUp != null) {
						DoAfterSetUp.NodeSetUp();
					}

					if (NodeSetUp != null) {
						NodeSetUp(Node);
					}
				} catch (Exception e) {
					m_ErrorMessages = e.ToString();
				}
			} else {
				m_ErrorMessages = msgs.ToString();
			}
		}

		static StringNodePoint ConvertNodePoint(Node.Point point) {
			return new StringNodePoint {
				X = point.X.ToString(),
				Y = point.Y.ToString(),
				Z = point.Z.ToString()
			};
		}

		static bool ValidatePoint(StringNodePoint stringPoint, Node.Point point)
		{
			float value;

			if (float.TryParse(stringPoint.X, out value))
				point.X = value;
			else
				return false;

			if (float.TryParse(stringPoint.Y, out value))
				point.Y = value;
			else
				return false;

			if (float.TryParse(stringPoint.Z, out value))
				point.Z = value;
			else
				return false;

			return true;
		}

		protected static GUILayoutOption MakeWidthOption(float portionOfArea) {
			return GUILayout.Width (MainAreaFrame.width * portionOfArea);
		}

		[System.Serializable]
		public class StringNodePoint
		{
			public string X {
				get;
				set;
			}
			public string Y {
				get;
				set;
			}
			public string Z {
				get;
				set;
			}
		}

		static bool IntToBool(int value) {
			return value != 0;
		}

		static int BoolToInt(bool value) {
			return value ? 1 : 0;
		}
	}
}
