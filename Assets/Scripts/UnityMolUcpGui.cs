using UnityEngine;
using Config;

public class UnityMolUcpGui : UnityClusterPackage.UcpGui {
	SlaveConfig m_SlaveConfig;

	protected override void Start()
	{
		base.Start();

		m_SlaveConfig = SlaveConfig.CurrentConfig;
	}

	protected override void GuiCustomizationHook()
	{
		if(Node.NodeType != UnityClusterPackage.Node.Type.slave) {
			return;
		}

		GUILayout.Label(string.Empty);
		GUILayout.Label("CONFIGURAÇÕES DE ESCRAVO");
		GUILayout.BeginHorizontal(); {
			m_SlaveConfig.CameraControl = GUILayout.Toggle(
				m_SlaveConfig.CameraControl,
				"Tem controle da câmera?",
				MakeWidthOption(.33f)
			);

			m_SlaveConfig.ShowEnergy = GUILayout.Toggle(
				m_SlaveConfig.ShowEnergy,
				"Mostra medidor de energia?",
				MakeWidthOption(.5f)
			);
		} GUILayout.EndHorizontal();

		if (GUILayout.Button("Salvar configurações de escravo")) {
			SlaveConfig.SetSlaveConfigData(m_SlaveConfig);
		}
	}
}
