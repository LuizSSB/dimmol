using System;
using Molecule.Model;
using System.Collections.Generic;

namespace UI
{
	public class SplineData
	{
		public List<string> BackupCAtomsSplinesChains { get; set; }
		public List<float[]> BackupCAtomsLocations { get; set; }
		public List<float[]> CAtomsLocations { get; set; }
		public List<AtomModel> CAtomsSplinesTypes { get; set; }
		public List<string> CAtomsSplinesChains { get; set; }
		public string Sequence { get; set; }
		public List<int[]> BondsEP { get; set; }
		public List<int[]> BondsEPSugar { get; set; }
		public List<int[]> BondsCAtoms { get; set; }
		public long NumberOfAtoms { get; set; }
		public long NumberOfBonds { get; set; }

		public static SplineData FromMoleculeModel() {
			return new SplineData {
				BackupCAtomsLocations = MoleculeModel.backupCatomsLocationlist,
				BackupCAtomsSplinesChains = MoleculeModel.backupCaSplineChainList,
				CAtomsLocations = MoleculeModel.CatomsLocationlist,
				CAtomsSplinesTypes = MoleculeModel.CaSplineTypeList,
				CAtomsSplinesChains = MoleculeModel.CaSplineChainList,
				Sequence = MoleculeModel.sequence,
				BondsEP = MoleculeModel.bondEPList,
				BondsEPSugar = MoleculeModel.bondEPSugarList,
				BondsCAtoms = MoleculeModel.bondCAList,
				NumberOfAtoms = MoleculeModel.atomsnumber,
				NumberOfBonds = MoleculeModel.bondsnumber
			};
		}

		public void ApplyOnMoleculeModel() {
			MoleculeModel.backupCatomsLocationlist = BackupCAtomsLocations;
			MoleculeModel.backupCaSplineChainList = BackupCAtomsSplinesChains;
			MoleculeModel.CatomsLocationlist = CAtomsLocations;
			MoleculeModel.CaSplineTypeList = CAtomsSplinesTypes;
			MoleculeModel.CaSplineChainList = CAtomsSplinesChains;
			MoleculeModel.sequence = Sequence;
			MoleculeModel.bondEPList = BondsEP;
			MoleculeModel.bondEPSugarList = BondsEPSugar;
			MoleculeModel.bondCAList = BondsCAtoms;
			MoleculeModel.atomsnumber = NumberOfAtoms;
			MoleculeModel.bondsnumber = NumberOfBonds;
		}
	}
}

