using System;
using System.Collections.Generic;

namespace GamessOutput
{
	public class SearchingParser : BaseParser
	{
		public SearchingParser(List<List<Atom>> statesSoFar = null, Dictionary<string, object> userInfo = null)
			: base(statesSoFar, userInfo)
		{
		}
		
		public override IParser ParseLine(string line, int lineNumber)
		{
			if (line == " ------------------------------------------------------------")
			{
				AtomsStates.Add(new List<Atom>());
				return new AtomParser(AtomsStates, UserInfo);
			}

			return this;
		}
	}
}

