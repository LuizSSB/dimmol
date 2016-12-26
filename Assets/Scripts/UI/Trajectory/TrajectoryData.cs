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
		private SplineData[] StatesSplines { get; set; }

		public OutputState CurrentState {
			get {
				return IsLoaded ? States[CurrentStateIdx] : null;
			}
		}

		public SplineData CurrentStateSpline {
			get {
				return IsLoaded ? StatesSplines[CurrentStateIdx] : null;
			}
		}

		public OutputState PreviousState { get; private set; }

		public RangeAttribute StateEnergyMinMax { get; private set; }

		public bool IsLoaded {
			get {
				return CurrentStateIdx >= 0;
			}
		}
		public bool HasEnergyMeter {
			get {
				return IsLoaded && CurrentState.HasEnergyData;
			}
		}

		public int NumberOfStates {
			get {
				return IsLoaded ? States.Length : 0;
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

					var state = States[value];
					if (MoleculeModel.atomsLocationlist.Count > 0 && state.Atoms.Count != MoleculeModel.atomsLocationlist.Count) {
						throw new System.InvalidOperationException("number of atoms in PDB file does not match number of atoms in display.");
					}

					PreviousState = CurrentState;
					var oldValue = _CurrentStateIdx;
					_CurrentStateIdx = value;
					UIData.Instance.stateChanged = true;

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
			new System.Threading.Thread(() => {
				List<OutputState> states = null;

				ModalUtility.SetLoadingHud(true, "Reading trajectory");
				try {
					ModalUtility.SetLoadingHud(true, "Reading trajectory");
					states = ParseUtils.ExtractStates(filePath, type);
				} catch(Exception e) {
					UIData.Instance.SetError(true, "Invalid trajectory file. Make sure the chosen file matches the chosen format (.gms for GAMESS output / .xmol for XMOL).");
				}
				ModalUtility.SetLoadingHud(false);

				if (states.Count == 0) {
					UIData.Instance.SetError(true, "Trajectory file has no states");
				} else {
					DoOnMainThread.AddAction(() => {
						AssemblyCSharp.RPCMessenger.GetCurrent().SendComplexObject(
							new TrajectoryLoadedArgs { States = states },
							GetType(),
							"ReceiveTrajectory"
						);
					});
				}
			}).Start();
		}


		private static SplineData[] CalculateSplines(OutputState[] states) {
			var firstStatePdb = PDBMaker.MakePDB(states[0]);
			Molecule.Control.ControlMolecule.CreateMolecule(new System.IO.StringReader(firstStatePdb));

			var splines = new SplineData[states.Length];

			ExternalOutput.Atom currentAtom;
			float[] currentLocation;
			int idxState = 0;
			foreach (var state in states) {
				for(int idxAtom = 0; idxAtom < MoleculeModel.atomsLocationlist.Count; ++idxAtom) {
					currentAtom = state.Atoms[idxAtom];
					currentLocation = MoleculeModel.atomsLocationlist[idxAtom];
					currentLocation[0] = currentAtom.FloatX;
					currentLocation[1] = currentAtom.FloatY;
					currentLocation[2] = currentAtom.FloatZ;
				}

				Molecule.Control.ControlMolecule.CreateSplines();

				splines[idxState++] = SplineData.FromMoleculeModel();
			}

			return splines;
		}

		public static void ReceiveTrajectory(int nodeId, TrajectoryLoadedArgs trajectory) {
			var states = trajectory.States;
			Instance.States = states.ToArray();
			Instance.StatesSplines = CalculateSplines(Instance.States);
			Instance._CurrentStateIdx = 0;

			var energies = states.Select(s => s.Energy);
			Instance.StateEnergyMinMax = new RangeAttribute(energies.Min(), energies.Max());

			var stateFile = ParseUtils.SaveStateAsPDB(states.First(), UnityClusterPackage.Constants.TemporaryFilesPath);
			GUIDisplay.Instance.OpenFileCallback(stateFile);

//			new System.Threading.Thread(new System.Threading.ThreadStart(() => {
//				System.Threading.Thread.Sleep(250);
				UIData.Instance.stateChanged = true;
//			})).Start();
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

