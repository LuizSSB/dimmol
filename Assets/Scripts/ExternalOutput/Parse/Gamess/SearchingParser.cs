using System;
using System.Collections.Generic;

namespace ExternalOutput.Parse.Gamess
{
	public class SearchingParser : BaseParser
	{
		private int mCounter = 3;

		public SearchingParser(List<OutputState> statesSoFar = null, Dictionary<string, object> userInfo = null)
			: base(statesSoFar, userInfo)
		{
		}
		
		public override IParser ParseLine(string line, int lineNumber)
		{			
			do {
				if (line.Contains("COORDINATES OF")) {
					--mCounter;
					break;
				} else if (mCounter == 3) {
					break;
				}

				if (line.Contains("ATOM   CHARGE       X              Y              Z")) {
					--mCounter;
					break;
				}

				if (line.Contains("------------------------------------------------------------")) {
					--mCounter;
				}

				break;
			} while(false);

			if(mCounter == 0) {
				AtomsStates.Add(new OutputState());
				return new AtomParser(AtomsStates, UserInfo);
			}

			return this;
		}
	}
}

