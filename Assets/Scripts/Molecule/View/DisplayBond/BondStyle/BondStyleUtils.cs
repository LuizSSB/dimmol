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
			Action<IBondUpdate, MonoBehaviour> setBondColor = (a, b) => {};

			switch (UIData.Instance.bondtype) {
				case UIData.BondType.hyperstick:
					bondsScripts = HStickManager.sticks;
					createBondUpdate = (int idx) => {
						var bondObject = BondCubeStyle.CreateCylinderByShader(idx, MoleculeModel.bondEPList);
						return bondObject.GetComponent<StickUpdate>();
					};
					break;
					setBondColor = (bondScript, bondObject) => {
						bondObject.GetComponent<Renderer>().material.SetColor(
							"_Color", bondScript.atompointer1.GetComponent<Renderer>().material.GetColor("_Color")
						);
						bondObject. GetComponent<Renderer>().material.SetColor(
							"_Color2", bondScript.atompointer2.GetComponent<Renderer>().material.GetColor("_Color")
						);
					};
				case UIData.BondType.line:
					bondsScripts = LineManager.lines;
					createBondUpdate = (int idx) => {
						var bondObject = BondLineStyle.CreateLine(idx, MoleculeModel.bondEPList);
						return bondObject.GetComponent<LineUpdate>();
					};
					setBondColor = (bondScript, bondObject) => {
						bondObject.GetComponent<LineRenderer>()
							.SetColors(
								bondScript.atompointer1.GetComponent<Renderer>().material.GetColor("_Color"),
								bondScript.atompointer2.GetComponent<Renderer>().material.GetColor("_Color")
							);
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
			Func<int, GameObject> getAtoms;
			switch (UIData.Instance.atomtype) {
				case UIData.AtomType.particleball:
					getAtoms = (idx) => (GameObject)MoleculeModel.atoms[idx];
					break;
				default:
					getAtoms = (idx) => atomManager.GetBall(MoleculeModel.atoms.Count - 1 - idx);
					break;
			}

			int idxBond = 0;
			foreach (var bond in MoleculeModel.bondEPList) {
				IBondUpdate bondScript;

				if (bondsScripts.Count == idxBond) {
					bondScript = createBondUpdate(idxBond);
					bondsScripts.Add(bondScript);
				} else {
					bondScript = (IBondUpdate)bondsScripts[idxBond];
				}

				bondScript.atompointer1 = getAtoms(bond[0]);
				bondScript.atomnumber1 = bond[0];
				bondScript.atompointer2 = getAtoms(bond[1]);
				bondScript.atomnumber2 = bond[1];

				setBondColor(bondScript, (MonoBehaviour)bondScript);

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

