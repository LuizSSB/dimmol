using System;
using System.Collections.Generic;
using System.IO;

namespace GamessOutput
{
	public class ParseUtils
	{
		public static List<List<Atom>> ExtractStates(string fromOutputFilePath)
		{
			IParser parser = new SearchingParser();
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

		public static string[] SaveStatesAsPDBs(List<List<Atom>> states, string toFolder)
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
	}
}

