﻿using System;
using System.Collections.Generic;

namespace GamessOutput
{
	public class AtomParser : BaseParser
	{
		public AtomParser(List<OutputState> statesSoFar = null, Dictionary<string, object> userInfo = null)
			: base(statesSoFar, userInfo)
		{
		}
				
		#region IParser implementation
		public override IParser ParseLine (string line, int lineNumber)
		{
			if (line.Trim().Length == 0)
			{
				return new EnergyParser(AtomsStates, UserInfo);
			}

			var lineParts = System.Text.RegularExpressions.Regex.Split(line.Trim(), " +");
			var atom = new Atom {
				Id = lineParts[0],
				Charge = float.Parse(lineParts[1]),
				X = lineParts[2],
				Y = lineParts[3],
				Z = lineParts[4]
			};

			CurrentAtoms.Atoms.Add(atom);

			return this;
		}
		#endregion
	}
}
