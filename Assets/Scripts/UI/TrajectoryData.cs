using System;
using ExternalOutput;
using ExternalOutput.Parse;
using Molecule.Model;
using UnityEngine;

namespace UI
{
	[ClearableSingleton]
	public class TrajectoryData
	{
		private static TrajectoryData sInstance = new TrajectoryData();
		public static TrajectoryData Instance {
			get {
				return sInstance = sInstance ?? new TrajectoryData();
			}
		}
		private TrajectoryData() {}

		public string[] StateFiles { get; private set; }

		public OutputState CurrentState { get; private set; }

		public OutputState PreviousState { get; private set; }

		public RangeAttribute StateEnergyMinMax { get; private set; }

		private int _CurrentStateIdx = -1;
		public int CurrentStateIdx {
			get {
				return _CurrentStateIdx;
			}
			set {
				if(value != _CurrentStateIdx) {
					if(value < 0 || value > StateFiles.Length - 1) {
						throw new System.ArgumentOutOfRangeException("CurrentStateIdx " + value + " out of bounds");
					}

					var state = ParseUtils.ExtractState(StateFiles[value]);
					if (MoleculeModel.atomsLocationlist.Count > 0 && state.Atoms.Count != MoleculeModel.atomsLocationlist.Count) {
						throw new System.InvalidOperationException("number of atoms in PDB file does not match number of atoms in display.");
					}

					PreviousState = CurrentState;
					CurrentState = state;
					_CurrentStateIdx = value;
					UIData.Instance.stateChanged = true;
				}
			}
		}

		public void GoToNextState() {
			if(CurrentStateIdx >= StateFiles.Length - 1) {
				CurrentStateIdx = 0;
			} else {
				++CurrentStateIdx;
			}
		}

		public void LoadSequenceOutputFile() {}
	}
}

