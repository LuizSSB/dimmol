using System;
using UnityEngine;
using System.Collections.Generic;

namespace UI
{
	// Luiz: could be far better, using abstract factories, templates and whatnot
	// but I'm not in the mood for writing such code for something as this.
	public class ModalUtility : MonoBehaviour
	{
		private enum ModalType
		{
			Message,
			Loading
		}

		private class Modal
		{
			public int Index { get; set; }
			public string Title { get; set; }
			public string Message { get; set; }
		}

		private static readonly Vector2 AlertSize =  new Vector2(400f, 400f);
		private static readonly Dictionary<ModalType, List<Modal>> ModalData = new Dictionary<ModalType, List<Modal>>();
		private static string[] LoadingStates = { "--", "\\", "|", "/"};
//		private static 

		static ModalUtility() {
			foreach (var value in Enum.GetValues(typeof(ModalType))) {
				ModalData[(ModalType)value] = new List<Modal>();
			}
		}

		public static void ShowAlert(string title, string text) {
			ModalData[ModalType.Message].Add(new Modal {
				Title = title,
				Message = text
			});
		}

		public static void SetLoadingHud(bool visible, string message = "") {
			ModalData[ModalType.Loading].Clear();

			if (visible) {
				ModalData[ModalType.Loading].Add(new Modal {
					Index = 0,
					Message = message,
					Title = "Loading"
				});
			}
		}

		private static void DrawAlert(int code) {
			var modal = ModalData[ModalType.Message][0];
			GUILayout.Label(modal.Message);
			GUILayout.FlexibleSpace();

			if (GUILayout.Button("OK")) {
				ModalData[ModalType.Message].RemoveAt(0);
			}
		}

		private static void DrawLoading(int code) {
			var modal = ModalData[ModalType.Loading][0];
			GUILayout.Label(modal.Message);
			GUILayout.FlexibleSpace();
			GUILayout.BeginHorizontal(); {
				GUILayout.FlexibleSpace();

				GUILayout.Label(LoadingStates[modal.Index]);
				modal.Index = (int)Time.time % LoadingStates.Length;

				GUILayout.FlexibleSpace();
			} GUILayout.EndHorizontal();
			GUILayout.FlexibleSpace();
		}

		void OnGUI() {
			var frame = new Rect(
				new Vector2((Screen.width - AlertSize.x) / 2f, (Screen.height - AlertSize.y) / 2f),
				AlertSize
			);

			foreach (var alertTypeData in ModalData) {
				if (alertTypeData.Value.Count > 0) {
					GUI.WindowFunction windowFunction;

					switch (alertTypeData.Key) {
						case ModalType.Message:
							windowFunction = DrawAlert;
							break;
						case ModalType.Loading:
							windowFunction = DrawLoading;
							break;
						default:
							UIData.Instance.SetError(true, "Unrecognized modal type: " + alertTypeData.Key);
							continue;
					}

					GUI.ModalWindow(91734, frame, windowFunction, alertTypeData.Value[0].Title);
					
					return;
				}
			}
		}
	}
}

