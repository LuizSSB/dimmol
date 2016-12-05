/// @file BondCubeStyle.cs
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
/// $Id: BondCubeStyle.cs 540 2014-06-05 13:23:54Z sebastien $
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
using System.Linq;


namespace Molecule.View.DisplayBond {
	using UnityEngine;
	using System.Collections;
	using System.Collections.Generic;
	using Molecule.Model;
	using Molecule.Control;
	using Config;
	using UI;
	using Molecule.View.DisplayAtom;

	[ClearableSingleton]
	public class BondCubeData {
		private static BondCubeData sInstance;
		public static BondCubeData Instance {
			get {
				return sInstance = sInstance ?? new BondCubeData();
			}
		}
		private BondCubeData() { }

		public readonly GameObject BondCubeParent = new GameObject("BondCubeParent");
	}

	public class BondCubeStyle:IBondStyle {
		public BondCubeStyle() {
		}
		
		public void DisplayBonds() {
			List<int[]> bondEPList;

			if(UIData.Instance.bondtype==UIData.BondType.cube) {
				MoleculeModel.bondsnumber = MoleculeModel.bondEPList.Count;
				if(UIData.Instance.secondarystruct)
					bondEPList=MoleculeModel.bondCAList;
				else
					bondEPList=MoleculeModel.bondEPList;
				int Number=bondEPList.Count;
				
				Debug.Log("DisplayBonds??bondList.Count "  + MoleculeModel.bondEPList.Count);

				for(int i=0;i<Number;i++)
					CreateCylinder(i, bondEPList);
				GameObject cbManagerObj = GameObject.FindGameObjectWithTag("CubeBondManager");
				CubeBondManager cbManager = cbManagerObj.GetComponent<CubeBondManager>();
				cbManager.Init();
				
				// HERE COMES THE BONDMANAGER
			}
			else if(UIData.Instance.bondtype==UIData.BondType.hyperstick) {
				if(UIData.Instance.secondarystruct)
					bondEPList=MoleculeModel.bondCAList;
				else
					bondEPList=MoleculeModel.bondEPList;
				Debug.Log("Bonds?? bondEPList.Count :: "  + bondEPList.Count);
				int Number=bondEPList.Count;

				for(int i=0;i<Number;i++)
					CreateCylinderByShader(i, bondEPList);
				GameObject hsManagerObj = GameObject.FindGameObjectWithTag("HStickManager");
				HStickManager hsManager = hsManagerObj.GetComponent<HStickManager>();
				hsManager.Init();
				
//				GameObject hbManagerObj = GameObject.FindGameObjectWithTag("HBallManager");
//				HBallManager hbManager = hbManagerObj.GetComponent<HBallManager>();
//				hbManager.findBonds();
//				if (UIData.Instance.atomtype == UIData.AtomType.hyperball)
//				{
//					if (hbManager.ellipsoidViewEnabled() == true)
//					{
//						Debug.Log ("+++++++++BONDCUBESTULE - Ellipsoid View enabled");
//						hbManager.RenderEllipsoids();
//					}
//					else {
//						Debug.Log ("+++++++++BONDCUBESTULE - Ellipsoid View disabled");
//						hbManager.RenderAtoms();
//					}
//				}
			}
			else if(UIData.Instance.bondtype==UIData.BondType.bbhyperstick) {
				if(UIData.Instance.secondarystruct)
					bondEPList=MoleculeModel.bondCAList;
				else
					bondEPList=MoleculeModel.bondEPList;
				Debug.Log("DisplayBonds??bondEPList.Count "  + bondEPList.Count);
				int Number=bondEPList.Count;

				for(int i=0;i<Number;i++)
					CreateBBCylinderByShader(i, bondEPList);
			}
		}

		//Hypersticks
		private static GameObject CreateCylinderByShader(int start, List<int[]> bondEPList) {
			GameObject Stick;
			int i = start;

			int[] atomsIds = bondEPList[i] as int[];
			Stick = GameObject.CreatePrimitive(PrimitiveType.Cube);
			RuntimePlatform platform = Application.platform;
			switch (platform) {
				case RuntimePlatform.WindowsPlayer:
				case RuntimePlatform.WindowsWebPlayer:
				case RuntimePlatform.WindowsEditor:
					Stick.GetComponent<Renderer>().material.shader = Shader.Find("FvNano/Stick HyperBalls 2 OpenGL");
					break;
				default :
					Stick.GetComponent<Renderer>().material.shader = Shader.Find("FvNano/Stick HyperBalls 2 OpenGL");
					break;				
			}
			StickUpdate comp = Stick.AddComponent<StickUpdate>();
			//Debug.Log("BOND : " + atomsIds[0] + " - " + atomsIds[1]);
			//comp.atompointer1=(GameObject)MoleculeModel.atoms[atomsIds[0]];
			//comp.atompointer2=(GameObject)MoleculeModel.atoms[atomsIds[1]];
				
			if (UI.UIData.Instance.atomtype == UI.UIData.AtomType.particleball) {
				comp.atompointer1 = (GameObject)MoleculeModel.atoms[atomsIds[0]];
				comp.atompointer2 = (GameObject)MoleculeModel.atoms[atomsIds[1]];
			} else {
				GenericManager manager = Molecule.View.DisplayMolecule.GetManagers()[0];
				comp.atompointer1 = manager.GetBall(Molecule.Model.MoleculeModel.atoms.Count - 1 - atomsIds[0]);
				comp.atompointer2 = manager.GetBall(Molecule.Model.MoleculeModel.atoms.Count - 1 - atomsIds[1]);
			}
			
			comp.enabled = true;										
			Stick.GetComponent<Renderer>().material.SetFloat("_Shrink", StickUpdate.shrink);
			Stick.tag = "Club";
			Stick.GetComponent<Collider>().enabled = false;
			Stick.transform.position = comp.atompointer1.transform.position;
			Stick.transform.parent = BondCubeData.Instance.BondCubeParent.transform;

			return Stick;
		}


		//Cubes
		private static void CreateCylinder(int i, List<int[]> bondEPList) {
/*			GameObject cylinder;
			MeshFilter filter;
			Mesh   cylinderMesh;
			cylinder=GameObject.CreatePrimitive(PrimitiveType.Cube);
			filter=cylinder.GetComponent<MeshFilter>();
			cylinderMesh=filter.mesh;
			filter.mesh=new Mesh();
			CombineInstance []instances=new CombineInstance[end-start];
*/
			int[] atomsIds = bondEPList[i] as int[];
			GameObject o=GameObject.CreatePrimitive(PrimitiveType.Cube);
			o.GetComponent<Renderer>().material=(Material)Resources.Load("Materials/CubeBoneMaterial");
			BondCubeUpdate comp = o.AddComponent<BondCubeUpdate>();
			//comp.atompointer1=(GameObject)MoleculeModel.atoms[atomsIds[0]];
			//comp.atompointer2=(GameObject)MoleculeModel.atoms[atomsIds[1]];
			
			if(UI.UIData.Instance.atomtype == UI.UIData.AtomType.particleball){
				comp.atompointer1=(GameObject)MoleculeModel.atoms[atomsIds[0]];
				comp.atompointer2=(GameObject)MoleculeModel.atoms[atomsIds[1]];
			}
			else{
				GenericManager manager = Molecule.View.DisplayMolecule.GetManagers()[0];
				comp.atompointer1 = manager.GetBall(Molecule.Model.MoleculeModel.atoms.Count - 1 - atomsIds[0]);
				comp.atompointer2 = manager.GetBall(Molecule.Model.MoleculeModel.atoms.Count - 1 - atomsIds[1]);
			}
			
//			o.transform.position = location[0];
//			o.transform.LookAt(location[1]);
			o.transform.localScale=new Vector3(0.1f,0.1f,1f);
			o.tag="Club";
			o.transform.parent = BondCubeData.Instance.BondCubeParent.transform;
		}
		

		//Billboard hypersticks
		private static void CreateBBCylinderByShader(int i, List<int[]> bondEPList) {
			GameObject Stick;
			if(UIData.Instance.toggleClip)
				Stick=Clip4HyperStick.CreateClip();
			else
				Stick=GameObject.CreatePrimitive(PrimitiveType.Plane);
						
			int[] atomsIds = bondEPList[i] as int[];	
					
			Stick.transform.Rotate(new Vector3(0,-180,0));
			Stick.AddComponent<CameraFacingBillboard>();
			Stick.GetComponent<CameraFacingBillboard>().cameraToLookAt = GameObject.Find("Camera").GetComponent<Camera>();
			RuntimePlatform platform = Application.platform;
			switch(platform) {
				case RuntimePlatform.WindowsPlayer:
				case RuntimePlatform.WindowsWebPlayer:
				case RuntimePlatform.WindowsEditor:
				Stick.GetComponent<Renderer>().material.shader=Shader.Find("FvNano/Stick HyperBalls 2 OpenGL");
					break;
				default :
					Stick.GetComponent<Renderer>().material.shader=Shader.Find("FvNano/Stick HyperBalls 2 OpenGL");
					break;				
			}
			Stick.AddComponent<StickUpdate>();
			
			StickUpdate comp = Stick.GetComponent<StickUpdate>();
			comp.atompointer1=(GameObject)MoleculeModel.atoms[atomsIds[0]];
			comp.atompointer2=(GameObject)MoleculeModel.atoms[atomsIds[1]];
			
			comp.enabled = true;										
			Stick.GetComponent<Renderer>().material.SetFloat("_Shrink", 0.01f);
			Stick.tag="Club";
			Stick.transform.parent = BondCubeData.Instance.BondCubeParent.transform;
		}

		// Luiz:
		public static void ReassignBonds() {
			GameObject hsManagerObj = GameObject.FindGameObjectWithTag("HStickManager");
			var bondsScripts = HStickManager.sticks;
			GenericManager atomManager = Molecule.View.DisplayMolecule.GetManagers()[0];

			int idxBond = 0;
			foreach (var bond in MoleculeModel.bondEPList) {
				StickUpdate bondScript;

				if (bondsScripts.Count == idxBond) {
					var bondObject = CreateCylinderByShader(idxBond, MoleculeModel.bondEPList);
					bondScript = bondObject.GetComponent<StickUpdate>();
					bondsScripts.Add(bondScript);
				} else {
					bondScript = bondsScripts[idxBond];
				}

				bondScript.atompointer1 = atomManager.GetBall(MoleculeModel.atoms.Count - 1 - bond[0]);
				bondScript.atompointer2 = atomManager.GetBall(MoleculeModel.atoms.Count - 1 - bond[1]);
				
				++idxBond;
			}

			while (bondsScripts.Count > idxBond) {
				var bondScript = bondsScripts[idxBond];
				bondsScripts.RemoveAt(idxBond);
				GameObject.Destroy(bondScript.gameObject);
			}
		}
	}
}
