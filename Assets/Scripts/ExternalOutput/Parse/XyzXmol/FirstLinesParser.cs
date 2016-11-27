using System;
using System.Collections.Generic;

namespace ExternalOutput.Parse.XyzXmol
{
	public class FirstLinesParser : BaseParser
	{
		internal const string KeyQuantityAtoms = "externaloutput.parse.xyzxmol.firstlinesparser.keyquantityatoms";

		private bool mReadAtomsQuantityLine = false;

		public FirstLinesParser(List<OutputState> statesSoFar = null, Dictionary<string, object> userInfo = null) : base(statesSoFar, userInfo)
		{
		}

		public override IParser ParseLine(string line, int lineNumber)
		{
			if (!mReadAtomsQuantityLine) {
				UserInfo[KeyQuantityAtoms] = int.Parse(line.Trim());
				mReadAtomsQuantityLine = true;
				AtomsStates.Add(new OutputState());

				return this;
			} else {
				return new AtomParser(AtomsStates, UserInfo);
			}
		}
	}
}

