using System;
using System.Collections.Generic;

namespace ExternalOutput.Parse
{
	public interface IParser
	{
		Dictionary<string, object> UserInfo {
			get;
			set;
		}

		List<OutputState> AtomsStates {
			get;
			set;
		}

		IParser ParseLine (string line, int lineNumber);
	}

	public static class ParserFactory {
		public static IParser GetInitialParser(string forFile, ParseableOutputTypes ofType) {
			switch (ofType) {
				case ParseableOutputTypes.Gamess:
					return new Gamess.SearchingParser();
					break;

				case ParseableOutputTypes.Xyz_Xmol:
					return new XyzXmol.FirstLinesParser();
					break;
			}

			throw new ArgumentException("Unrecognized output type");
		}
	}

	public abstract	class BaseParser : IParser
	{
		protected BaseParser(List<OutputState> statesSoFar = null, Dictionary<string, object> userInfo = null)
		{
			AtomsStates = statesSoFar ?? new List<OutputState>();
			UserInfo = userInfo ?? new Dictionary<string, object>();

			if(AtomsStates.Count > 0)
				CurrentAtoms = statesSoFar[statesSoFar.Count - 1];
		}

		protected OutputState CurrentAtoms {
			get;
			private set;
		}

		protected T GetUserInfo<T>(string key) {
			return (T)UserInfo[key];
		}

		#region IParser implementation
		public abstract IParser ParseLine (string line, int lineNumber);

		public Dictionary<string, object> UserInfo {
			get;
			set;
		}

		public List<OutputState> AtomsStates {
			get;
			set;
		}
		#endregion
	}
}

