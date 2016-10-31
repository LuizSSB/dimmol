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
		GUILayout.Label("Slave settings");
		GUILayout.BeginHorizontal(); {
			m_SlaveConfig.CameraControl = GUILayout.Toggle(
				m_SlaveConfig.CameraControl,
				"Has camera control",
				MakeWidthOption(.33f)
			);
			m_SlaveConfig.ShowEnergy = GUILayout.Toggle(
				m_SlaveConfig.ShowEnergy,
				"Displays energy meter",
				MakeWidthOption(.33f)
			);
			if (GUILayout.Button("Save slave settings", MakeWidthOption(.32f))) {
				SlaveConfig.SetSlaveConfigData(m_SlaveConfig);
			}
		} GUILayout.EndHorizontal();
	}
}
