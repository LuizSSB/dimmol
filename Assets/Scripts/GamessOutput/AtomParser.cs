using System;
using System.Collections.Generic;

namespace GamessOutput
{
	public class AtomParser : BaseParser
	{
		public AtomParser(List<List<Atom>> statesSoFar = null, Dictionary<string, object> userInfo = null)
			: base(statesSoFar, userInfo)
		{
			CurrentAtoms = statesSoFar[statesSoFar.Count - 1];
		}

		public List<Atom> CurrentAtoms {
			get;
			set;
		}
		
		
		#region IParser implementation
		public override IParser ParseLine (string line, int lineNumber)
		{
			if (line.Substring(0, 4) == "--- ")
			{
				return new SearchingParser(AtomsStates, UserInfo);
			}

			var lineParts = System.Text.RegularExpressions.Regex.Split(line.Trim(), " +");
			var atom = new Atom {
				Id = lineParts[0],
				Charge = float.Parse(lineParts[1]),
				X = lineParts[2],
				Y = lineParts[3],
				Z = lineParts[4]
			};

			CurrentAtoms.Add(atom);

			return this;
		}
		#endregion
	}
}

