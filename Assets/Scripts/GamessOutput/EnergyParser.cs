using System;

namespace GamessOutput
{
	public class EnergyParser : BaseParser
	{
		public EnergyParser(System.Collections.Generic.List<OutputState> statesSoFar, System.Collections.Generic.Dictionary<string, object> userInfo) : base(statesSoFar, userInfo)
		{
		}

		public override IParser ParseLine(string line, int lineNumber)
		{
			Action getEnergy = () => {
				var lineParts = System.Text.RegularExpressions.Regex.Split(line.Trim(), " +");
				CurrentAtoms.Energy = float.Parse(lineParts[lineParts.Length - 1]);
			};
			if(line.Contains("TOTAL ENERGY =")) {
				getEnergy();
				return this;
			} else if (line.Contains("ENERGY=")) {
				getEnergy();
				return new SearchingParser(AtomsStates, UserInfo);
			}

			return this;
		}
	}
}

