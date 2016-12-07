using UnityEngine;
using System.Collections;
using Config;

public class UnityMolUcpGui : UnityClusterPackage.UcpGui {
	private SlaveConfig m_SlaveConfig;

	protected override void Start()
	{
		base.Start();

		m_SlaveConfig = SlaveConfig.CurrentConfig;
	}

	protected override void GuiCustomizationHook()
	{
		GUILayout.Label(string.Empty);
		GUILayout.Label("SLAVE SETTINGS");
		GUILayout.BeginHorizontal(); {
			m_SlaveConfig.CameraControl = GUILayout.Toggle(
				m_SlaveConfig.CameraControl,
				"Has camera control",
				MakeWidthOption(.33f)
			);

			m_SlaveConfig.ShowEnergy = GUILayout.Toggle(
				m_SlaveConfig.ShowEnergy,
				"Displays energy meter",
				MakeWidthOption(.5f)
			);
		} GUILayout.EndHorizontal();

		if (GUILayout.Button("Save slave settings")) {
			SlaveConfig.SetSlaveConfigData(m_SlaveConfig);
		}
	}
}
