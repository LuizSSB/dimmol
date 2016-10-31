using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadMoleculeUcpGuiAction : UnityClusterPackage.UcpGuiAction {
	#region implemented abstract members of UcpGuiAction
	public override void NodeSetUp()
	{
		SceneManager.LoadScene("Molecule", UnityEngine.SceneManagement.LoadSceneMode.Single);
	}
	#endregion
}
