using System;
using UnityEngine;
using System.Linq;

namespace UI
{
	public static class EnergyWindow
	{
		private const string NumberToStringFormat = "#.0000";
		private const string ValueFormat = "<b>{0}</b>";
		
		private static GUIStyle sSliderStyle;
		private static GUIStyle sThumbStyle;
		private static GUIStyle sPoleStyle;
		private static GUIStyle sValueStyle;

		static EnergyWindow() {
			var margins = new RectOffset();

			var sliderColors = new Color[256];
			foreach(var idx in Enumerable.Range(0, 255)) {
				sliderColors[idx] = new Color(idx / 256f, (256f - idx) / 256f, 0);
			}
			sSliderStyle = new GUIStyle(GUI.skin.verticalSlider);
			sSliderStyle.normal.background = new Texture2D(1, 256);
			sSliderStyle.normal.background.SetPixels(sliderColors);
			sSliderStyle.normal.background.Apply();
			sSliderStyle.margin = margins;

			sThumbStyle = new GUIStyle(GUI.skin.verticalSliderThumb);
			sThumbStyle.normal.background = new Texture2D(1, 1);
			sThumbStyle.normal.background.SetPixel(0, 0, Color.white);
			sThumbStyle.normal.background.Apply();
			sThumbStyle.fixedHeight = 1f;
			sThumbStyle.margin = margins;

			sPoleStyle = new GUIStyle(GUI.skin.label);
			sPoleStyle.normal.textColor = Color.gray;
			sPoleStyle.margin = margins;

			sValueStyle = new GUIStyle(sPoleStyle);
			sValueStyle.normal.textColor = Color.white;
			sValueStyle.richText = true;
			sValueStyle.alignment = TextAnchor.MiddleCenter;
		}

		public static void Draw(float value, float maxValue, float minValue) {
			LoadTypeGUI.SetTitle("Geom. Energy");

			GUILayout.BeginVertical(); {
				GUILayout.Label(maxValue.ToString(NumberToStringFormat), sPoleStyle);

				GUILayout.BeginHorizontal(); {
					GUILayout.VerticalSlider(value, maxValue, minValue, sSliderStyle, sThumbStyle);

					if(value - minValue < 1e-4) {
						sValueStyle.normal.textColor = Color.green;
					} else if (maxValue - value < 1e-4) {
						sValueStyle.normal.textColor = Color.red;
					} else {
						sValueStyle.normal.textColor = Color.white;
					}

					GUILayout.BeginVertical(); {
						GUILayout.FlexibleSpace();
						GUILayout.Label(
							string.Format(ValueFormat, value.ToString(NumberToStringFormat)),
							sValueStyle
						);
						GUILayout.FlexibleSpace();
						
					} GUILayout.EndVertical();
				} GUILayout.EndHorizontal();

				GUILayout.Label(minValue.ToString(NumberToStringFormat), sPoleStyle);
			} GUILayout.EndVertical();
		}
	}
}

