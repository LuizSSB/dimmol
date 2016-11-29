using System;
using System.Collections.Generic;
using System.IO;

namespace ExternalOutput.Parse
{
	public class ParseUtils
	{
		public static List<OutputState> ExtractStates(string fromOutputFilePath, ParseableOutputTypes ofType)
		{
			IParser parser = ParserFactory.GetInitialParser(fromOutputFilePath, ofType);

			using (var reader = File.OpenText(fromOutputFilePath))
			{
				string line;
				for (int idx = 0; (line = reader.ReadLine()) != null; ++idx)
				{
					parser = parser.ParseLine(line, idx);
				}
			}

			return parser.AtomsStates;
		}

		public static string SaveStateAsPDB(OutputState state, string toFolder) {
			return SaveStatesAsPDBs(
				new List<OutputState> { state },
				toFolder
			)[0];
		}

		public static string[] SaveStatesAsPDBs(List<OutputState> states, string toFolder)
		{
			int stateIdx = 0;
			var pdbsPaths = new string[states.Count];
			foreach (var state in states)
			{
				pdbsPaths[stateIdx] = Path.Combine(toFolder, "state" + stateIdx + ".pdb");
				string pdbFile = PDBMaker.MakePDB(state);
				File.WriteAllText (
					pdbsPaths[stateIdx],
					pdbFile
				);
				++stateIdx;
			}

			return pdbsPaths;
		}

		public static OutputState ExtractState(string pdbPath) {
			var state = new OutputState();
			using (var reader = File.OpenText(pdbPath))
			{
				string line;
				while ((line = reader.ReadLine()) != null)
				{
					if(line.StartsWith("ATOM")) {
						state.Atoms.Add(new Atom {
							Id = line.Substring(13, 4).Trim(),
							X = line.Substring(30, 8).Trim(),
							Y = line.Substring(38, 8).Trim(),
							Z = line.Substring(46, 8).Trim()
						});
					}
					else if (line.StartsWith("TITLE")) {
						state.Energy = float.Parse(line.Substring(line.IndexOf("energy=") + "energy=".Length));
					}
				}
			}

			return state;
		}

		public static string SerializeState(Atom[] state) {
			var serialized = new System.Text.StringBuilder("[");
			foreach(var atom in state) {
				serialized.Append(UnityEngine.JsonUtility.ToJson(atom) + ",");
			}
			serialized.Append("]");
			return serialized.ToString();
		}

		public static Atom[] DeserializeState(string serializedState) {
			var atoms = serializedState.Trim('[', ']').Replace("},", "};").Split(';');
			var atomsArray = new Atom[atoms.Length - 1];
			var idxAtom = 0;
			foreach(var atom in atoms) {
				if(atom.Trim().Length > 0)
				atomsArray[idxAtom++] = UnityEngine.JsonUtility.FromJson<Atom>(atom);
			}
			return atomsArray;
		}
	}
}

