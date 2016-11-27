using System;
using System.Collections.Generic;

namespace ExternalOutput.Parse.XyzXmol
{
	public class AtomParser : BaseParser
	{
		private int mAtomsIncluded = 0;
		private int mLinesToRead;

		public AtomParser(List<OutputState> statesSoFar, Dictionary<string, object> userInfo) : base(statesSoFar, userInfo)
		{
			mLinesToRead = GetUserInfo<int>(FirstLinesParser.KeyQuantityAtoms);
		}

		public override IParser ParseLine(string line, int lineNumber)
		{
			line = line.Trim();

			if (string.IsNullOrEmpty(line)) {
				throw new ArgumentException("line " + lineNumber + " is empty");
			}

			var lineParts = line.GetWordsSimple();

			// Luiz: there may be more than 4 parameters, because of the xmol format.
			if (lineParts.Length < 4) {
				throw new ArgumentException("line " + lineNumber + " has too few parts");
			}

			CurrentAtoms.Atoms.Add(new ExternalOutput.Atom() {
				Id = lineParts[0],
				X = lineParts[1],
				Y = lineParts[2],
				Z = lineParts[3]
			});

			if (++mAtomsIncluded >= mLinesToRead)
				return new FirstLinesParser(AtomsStates, UserInfo);
			else
				return this;
		}
	}
}

