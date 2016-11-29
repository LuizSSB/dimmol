using System;
using ExternalOutput;
using ExternalOutput.Parse;
using Molecule.Model;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Config;

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

//		private string[] StateFiles { get; private set; }
		private OutputState[] States { get; set; }

		public OutputState CurrentState {
			get {
				return IsLoaded ? States[CurrentStateIdx] : null;
			}
		}

		public OutputState PreviousState { get; private set; }

		public RangeAttribute StateEnergyMinMax { get; private set; }

		public bool IsLoaded {
			get {
				return CurrentStateIdx >= 0;
			}
		}

		public int NumberOfStates {
			get {
				return States.Length;
			}
		}

		private int _CurrentStateIdx = -1;
		public int CurrentStateIdx {
			get {
				return _CurrentStateIdx;
			}
			set {
				if(value != _CurrentStateIdx) {
					if(value < 0 || value > NumberOfStates - 1) {
						throw new System.ArgumentOutOfRangeException("CurrentStateIdx " + value + " out of bounds");
					}

//					var state = ParseUtils.ExtractState(StateFiles[value]);
//					if (MoleculeModel.atomsLocationlist.Count > 0 && state.Atoms.Count != MoleculeModel.atomsLocationlist.Count) {
//						throw new System.InvalidOperationException("number of atoms in PDB file does not match number of atoms in display.");
//					}

					PreviousState = CurrentState;
					var oldValue = _CurrentStateIdx;
					_CurrentStateIdx = value;
					UIData.Instance.stateChanged = true;
					Debug.Log("dig");

					ChangeManager.DispatchPropertyEvent(GetType(), "CurrentStateIdx", oldValue, value);
				}
			}
		}

		public void GoToNextState() {
			if(CurrentStateIdx >= NumberOfStates - 1) {
				CurrentStateIdx = 0;
			} else {
				++CurrentStateIdx;
			}
		}

		public void LoadTrajectoryFile(string filePath, ParseableOutputTypes type) {
			var states = ParseUtils.ExtractStates(filePath, type);

			AssemblyCSharp.RPCMessenger.GetCurrent().SendComplexObject(
				new TrajectoryLoadedArgs { States = states },
				GetType(),
				"ReceiveTrajectory"
			);
		}

		public static void ReceiveTrajectory(int nodeId, TrajectoryLoadedArgs trajectory) {
			var states = trajectory.States;
			Instance.States = states.ToArray();
			Instance._CurrentStateIdx = 0;

			var energies = states.Select(s => s.Energy);
			Instance.StateEnergyMinMax = new RangeAttribute(energies.Min(), energies.Max());

			var stateFile = ParseUtils.SaveStateAsPDB(states.First(), Application.temporaryCachePath);
			GUIDisplay.Instance.OpenFileCallback(stateFile);

			new System.Threading.Thread(new System.Threading.ThreadStart(() => {
				System.Threading.Thread.Sleep(250);
				UIData.Instance.stateChanged = true;
			})).Start();
		}

		public static void Clear() {
			sInstance = null;
		}
	}

	[Serializable]
	public class TrajectoryLoadedArgs {
		public List<OutputState> States;
	}
}

