using System;
using UnityEngine;
using System.Collections.Generic;

namespace UI
{
	// Luiz: unfinished, but works for what it was initially supposed to
	public class ModalUtility : MonoBehaviour
	{
		private class Alert
		{
			public string Title { get; set; }
			public string Message { get; set; }
		}

		private static readonly System.Random sRandom = new System.Random();
		private static readonly Vector2 AlertSize =  new Vector2(400, 150);
		private static readonly Dictionary<int, Alert> AlertData = new Dictionary<int, Alert>();

		public static void ShowAlert(string title, string text) {
			AlertData[sRandom.Next()] = new Alert { Title = title, Message = text };
		}

		private static void DrawAlert(int code) {
			GUILayout.Label(AlertData[code].Message);
			GUILayout.FlexibleSpace();
			if (GUILayout.Button("OK")) {
				AlertData.Remove(code);
			}
		}

		void OnGUI() {
			foreach (var keyValue in AlertData) {
				var frame = new Rect(
					new Vector2((Screen.width - AlertSize.x) / 2f, (Screen.height - AlertSize.y) / 2f),
					AlertSize
				);
				GUI.ModalWindow(keyValue.Key, frame, DrawAlert, keyValue.Value.Title);
			}
		}
	}
}

