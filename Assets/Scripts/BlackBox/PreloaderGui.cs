using UI;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ExternalOutput.Parse;

namespace BlackBox
{
	public class PreloaderGui : MonoBehaviour
	{
		class PreloadedStructure {
			public string Label {
				get;
				set;
			}

			public string File {
				get;
				set;
			}

			public string Format {
				get {
					return File.Split('.').Last().ToLower();
				}
			}

			public static PreloadedStructure From(string name, string file) {
				return new PreloadedStructure {
					Label = name,
					File = file
				};
			}
		}

		static readonly Dictionary<string, PreloadedStructure[]> sPreloadedMolecules =
			new Dictionary<string, PreloadedStructure[]> {
			{"Moléculas", new [] {
					PreloadedStructure.From("Trifluoreto de Bório (BF3)", "BF3.xyz"),
					PreloadedStructure.From("Etanol (C2H6O)", "C2H6O.pdb"),
					PreloadedStructure.From("Metano (CH4)", "CH4.pdb"),
					PreloadedStructure.From("Dióxido de Carbono (CO2)", "CO2.xyz"),
					PreloadedStructure.From("Água (H2O)", "H2O.xyz"),
					PreloadedStructure.From("Sulfeto de Hidrogênio (H2S)", "H2S.xyz"),
					PreloadedStructure.From("Ácido Clorídrico (HCl)", "HCl.xyz"),
					PreloadedStructure.From("Cianeto de Hidrogênio (HCN)", "HCN.pdb"),
					PreloadedStructure.From("Gás Nitrogênio (N2)", "N2.xyz"),
					PreloadedStructure.From("Amônia (NH3)", "NH3.pdb"),
					PreloadedStructure.From("Gás Oxigênio (O2)", "O2.xyz"),
					PreloadedStructure.From("Quitosana", "chitosan.xyz"),
				}},
			{"Trajetórias", new [] {
					PreloadedStructure.From("Otimização Glicina", "trajectory_glycine.gms"),
					PreloadedStructure.From("Dinâmica Nanotubo de Carbono + Quitosana", "trajectory_cnt_chitosan.xmol"),
				}},
		};

		public bool ShowingMenu { get; set; }

		public void DrawMenuOption() {
			var guiEnabled = GUI.enabled;
			GUI.enabled = true;
			if(GUILayout.Button(new GUIContent("Moléculas", "Menu de moléculas pré-carregadas")))
			{
				ShowingMenu = !ShowingMenu;
			}
			GUI.enabled = guiEnabled;
		}

		void OnGUI() {
			if (!ShowingMenu)
				return;
			
			var frame = Rectangles.openRect;
			frame.x += frame.width;

			GUI.Window(
				1332,
				frame,
				id => {
					if(DrawWindowTitleBar()) {
						ShowingMenu = false;
						return;
					} 

					var buttonAligment = GUI.skin.button.alignment;
					GUI.skin.button.alignment = TextAnchor.MiddleLeft;

					foreach(var structureType in sPreloadedMolecules) {
						GUILayout.Label(string.Empty);
						GUILayout.Label(structureType.Key.ToUpper());

						foreach(var structure in structureType.Value) {
							if(GUILayout.Button(structure.Label)) {
								ShowingMenu = false;
								LoadStructure(structure);
							}
						}
					}

					GUI.skin.button.alignment = buttonAligment;
				},
				string.Empty
			);
		}

		static void LoadStructure(PreloadedStructure structure) {
			string path = Path.Combine(
				              Application.streamingAssetsPath,
				              Path.Combine(
					              "Structures",
					              structure.File
				              ));

			GUIDisplay.Instance.Clear(false);
			GUIMoleculeController.Instance.showOpenMenu = false;

			var format = structure.Format;
			if(format == "xyz" || format == "pdb") {
				GUIDisplay.Instance.OpenFileCallback(path);
			} else {
				GUIDisplay.Instance.TrajectoryType = format == "xmol" ?
					ParseableOutputTypes.Xyz_Xmol : ParseableOutputTypes.Gamess;
				GUIDisplay.Instance.OpenTrajectoryCallback(path);
			}
		}

		static bool DrawWindowTitleBar() {
			GUILayout.BeginHorizontal();

			GUILayout.FlexibleSpace();
			GUILayout.Label("Moléculas pré-carregadas");
			GUILayout.FlexibleSpace();

			bool close = GUILayout.Button("X");

			GUILayout.EndHorizontal();

			return close;
		}

		public static PreloaderGui FindFirstInstance() {
			return GameObject.Find("Preloader").GetComponent<PreloaderGui>();
		}
	}
}

