/// @file ScenePreload_Fieldlines.cs
/// @brief Details to be specified
/// @author FvNano/LBT team
/// @author Marc Baaden <baaden@smplinux.de>
/// @date   2013-4
///
/// Copyright Centre National de la Recherche Scientifique (CNRS)
///
/// contributors :
/// FvNano/LBT team, 2010-13
/// Marc Baaden, 2010-13
///
/// baaden@smplinux.de
/// http://www.baaden.ibpc.fr
///
/// This software is a computer program based on the Unity3D game engine.
/// It is part of UnityMol, a general framework whose purpose is to provide
/// a prototype for developing molecular graphics and scientific
/// visualisation applications. More details about UnityMol are provided at
/// the following URL: "http://unitymol.sourceforge.net". Parts of this
/// source code are heavily inspired from the advice provided on the Unity3D
/// forums and the Internet.
///
/// This software is governed by the CeCILL-C license under French law and
/// abiding by the rules of distribution of free software. You can use,
/// modify and/or redistribute the software under the terms of the CeCILL-C
/// license as circulated by CEA, CNRS and INRIA at the following URL:
/// "http://www.cecill.info".
/// 
/// As a counterpart to the access to the source code and rights to copy, 
/// modify and redistribute granted by the license, users are provided only 
/// with a limited warranty and the software's author, the holder of the 
/// economic rights, and the successive licensors have only limited 
/// liability.
///
/// In this respect, the user's attention is drawn to the risks associated 
/// with loading, using, modifying and/or developing or reproducing the 
/// software by the user in light of its specific status of free software, 
/// that may mean that it is complicated to manipulate, and that also 
/// therefore means that it is reserved for developers and experienced 
/// professionals having in-depth computer knowledge. Users are therefore 
/// encouraged to load and test the software's suitability as regards their 
/// requirements in conditions enabling the security of their systems and/or 
/// data to be ensured and, more generally, to use and operate it in the 
/// same conditions as regards security.
///
/// The fact that you are presently reading this means that you have had 
/// knowledge of the CeCILL-C license and that you accept its terms.
///
/// $Id: ScenePreload_Fieldlines.cs 268 2013-05-08 11:15:28Z kouyoumdjian $
///
/// References : 
/// If you use this code, please cite the following reference : 	
/// Z. Lv, A. Tek, F. Da Silva, C. Empereur-mot, M. Chavent and M. Baaden:
/// "Game on, Science - how video game technology may help biologists tackle
/// visualization challenges" (2013), PLoS ONE 8(3):e57990.
/// doi:10.1371/journal.pone.0057990
///
/// If you use the HyperBalls visualization metaphor, please also cite the
/// following reference : M. Chavent, A. Vanel, A. Tek, B. Levy, S. Robert,
/// B. Raffin and M. Baaden: "GPU-accelerated atom and dynamic bond visualization
/// using HyperBalls, a unified algorithm for balls, sticks and hyperboloids",
/// J. Comput. Chem., 2011, 32, 2924
///

using UnityEngine;
using System.Collections;
using UI;
using ParseData.ParsePDB;
using Molecule.Model;
using Molecule.View;

public class ScenePreload_Fieldlines : MonoBehaviour {
	
	private float pdb_progress = 0;
	private float json_progress = 0;
	private float obj_progress = 0;
	private string progresses;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator InitScene(RequestPDB requestPDB)
	{
		StartCoroutine(requestPDB.LoadPDBWWW(UIData.Instance.server_url+"Scenes/fieldlines/fieldline.pdb"));
		while(!RequestPDB.isDone)
		{
			pdb_progress = requestPDB.progress;
			Debug.Log(pdb_progress);
			yield return new WaitForEndOfFrame();
		}
		pdb_progress = 1.0f;
		
		UIData.Instance.atomtype = UIData.AtomType.particleball;
		UIData.Instance.bondtype = UIData.BondType.nobond;
		GUIMoleculeController.Instance.showOpenMenu=false;
		GUIMoleculeController.Instance.showAtomMenu=true;
		SendMessage("Display",SendMessageOptions.DontRequireReceiver);
		
		StartCoroutine(requestPDB.LoadJsonWWW(UIData.Instance.server_url+"Scenes/fieldlines/fieldline.json",MoleculeModel.Offset));
		while(!RequestPDB.isDone)
		{
			json_progress = requestPDB.progress;
			yield return new WaitForEndOfFrame();
		}
		json_progress = 1.0f;
		MoleculeModel.fieldLineFileExists = true;
		GUIMoleculeController.Instance.showFieldLines = true;
		FieldLineStyle.DisplayFieldLine();

		StartCoroutine(requestPDB.LoadOBJWWW(UIData.Instance.server_url+"Scenes/fieldlines/fieldline.obj"));
		while(!RequestPDB.isDone)
		{
			obj_progress = requestPDB.progress;
			yield return new WaitForEndOfFrame();
		}
		obj_progress = 1.0f;
		MoleculeModel.surfaceFileExists = true;
		GUIMoleculeController.Instance.modif=true;
	
	}
	
	void OnGUI()
	{
		if(pdb_progress + json_progress + obj_progress >= 3.0f)
			return;
		progresses = "PDB loading: " + Mathf.CeilToInt(pdb_progress*100) + "%\n";
		progresses +="FieldLines loading: " + Mathf.CeilToInt(json_progress*100) + "%\n";
		progresses += "Surface loading: " + Mathf.CeilToInt(obj_progress*100) + "%";
		//GUI.enabled = false;
		progresses = GUI.TextArea(new Rect(Screen.width/2 - 100, Screen.height/2 - 50, 200,100), progresses);
		//GUI.enabled = true;
	}
}
