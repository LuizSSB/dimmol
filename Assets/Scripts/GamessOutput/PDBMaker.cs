using System;
using System.Collections.Generic;

namespace GamessOutput
{
	public static class PDBMaker
	{
		public static string MakePDB (OutputState state)
		{
			var pdbBuilder = new System.Text.StringBuilder();
			pdbBuilder.AppendLine("COMPND    MAGICKOMPOUND");
			pdbBuilder.AppendLine("TITLE     MAGICKOMPOUND, energy=" + state.Energy);
			pdbBuilder.AppendLine("AUTHOR    Foo bar");

			var idx = 1;
			foreach (var atom in state.Atoms)
			{
				pdbBuilder.Append(
					"ATOM  "
					+ idx.ToString().PadLeft(5)
					+ " "
					+ atom.Id.Substring(0, Math.Min(4, atom.Id.Length)).PadRight(4)
					+ "         1    "
					+ FixNumber(atom.X)
					+ FixNumber(atom.Y)
					+ FixNumber(atom.Z)
					+ "  1.00 10.00           "
					+ "\n"
				);

				++idx;
			}
			pdbBuilder.AppendLine(
				"TER   "
				+ idx.ToString().PadLeft(5)
			);
			pdbBuilder.AppendLine("END");

			return pdbBuilder.ToString();
		}

		private static string FixNumber(string number, int maxLength = 8)
		{
			int indexOfE = number.IndexOf("E", StringComparison.CurrentCultureIgnoreCase);
			double trueNumber;

			if (indexOfE > -1)
			{
				int power = int.Parse(number.Substring(indexOfE + 1));
				if (power < 0 && power >= -4)
				{
					number = number.Substring(0, indexOfE);
					trueNumber = double.Parse(number);
					trueNumber *= Math.Pow(10, power);
				}
				else
				{
					var notation = number.Substring(indexOfE);
					var remainingLength = maxLength - notation.Length;
					var content = double.Parse(number.Substring(0, indexOfE));
					var contentString = content.ToString("#.#################");
					if (contentString.Length > remainingLength)
						contentString = contentString.Substring(0, remainingLength);
					return SuperPad(contentString + notation, maxLength);
				}
			}
			else
			{
				trueNumber = double.Parse(number);
			}

			var trueString = trueNumber.ToString("#.#################");

			if (trueString.Length == 0)
				return "00000000";

			if (trueString.Length > maxLength)
				trueString = trueString.Substring(0, maxLength);

			return SuperPad(trueString, maxLength);
		}

		private static string SuperPad(string stuff, int maxLength)
		{
			var diff = maxLength - stuff.Length;

			if (diff == 0)
				return stuff;
			
			if (diff > 0)
			{
				var pad = 0.ToString("D" + diff);
				return stuff[0] == '-' ? "-" + pad + stuff.Substring(1) : pad + stuff;
			}

			throw new ArgumentException("stuff");
		}
	}
}

