using System;
using System.Collections;
using UI;
using Molecule.Model;
using UnityEngine;

namespace Molecule.View.DisplayBond
{
	public static class BondStyleUtils
	{
		// Luiz:
		public static void ReassignBonds(bool destroyExceedImmediately) {
			IList bondsScripts; // Luiz: gotta be without generics
			Func<int, IBondUpdate> createBondUpdate;

			switch (UIData.Instance.bondtype) {
				case UIData.BondType.cube:
					bondsScripts = CubeBondManager.bonds;
					createBondUpdate = (int idx) => {
						var bondObject = BondCubeStyle.CreateCylinder(idx, MoleculeModel.bondEPList);
						return bondObject.GetComponent<BondCubeUpdate>();
					};
					break;
				case UIData.BondType.hyperstick:
					bondsScripts = HStickManager.sticks;
					createBondUpdate = (int idx) => {
						var bondObject = BondCubeStyle.CreateCylinderByShader(idx, MoleculeModel.bondEPList);
						return bondObject.GetComponent<StickUpdate>();
					};
					break;
				case UIData.BondType.line:
					bondsScripts = LineManager.lines;
					createBondUpdate = (int idx) => {
						var bondObject = BondLineStyle.CreateLine(idx, MoleculeModel.bondEPList);
						return bondObject.GetComponent<LineUpdate>();
					};
					break;
				case UIData.BondType.nobond:
					return;
					break;
				default:
					UIData.Instance.SetError(true, "Unrecognized bond type\"" + UIData.Instance.bondtype + "\"");
					return;
			}

			GenericManager atomManager = Molecule.View.DisplayMolecule.GetManagers()[0];
			int idxBond = 0;
			foreach (var bond in MoleculeModel.bondEPList) {
				IBondUpdate bondScript;

				if (bondsScripts.Count == idxBond) {
					bondScript = createBondUpdate(idxBond);
					bondsScripts.Add(bondScript);
				} else {
					bondScript = (IBondUpdate)bondsScripts[idxBond];
				}

				bondScript.atompointer1 = atomManager.GetBall(MoleculeModel.atoms.Count - 1 - bond[0]);
				bondScript.atomnumber1 = bond[0];
				bondScript.atompointer2 = atomManager.GetBall(MoleculeModel.atoms.Count - 1 - bond[1]);
				bondScript.atomnumber2 = bond[1];

				++idxBond;
			}

			while (bondsScripts.Count > idxBond) {
				var bondScript = bondsScripts[idxBond];
				var bondObject = ((MonoBehaviour)bondScript).gameObject;

				bondsScripts.RemoveAt(idxBond);

				if (destroyExceedImmediately) {
					GameObject.DestroyImmediate(bondObject);
				} else {
					GameObject.Destroy(bondObject);
				}
			}
		}
	}
}

