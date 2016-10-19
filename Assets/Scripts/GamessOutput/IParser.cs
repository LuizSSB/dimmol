using System;
using System.Collections.Generic;

namespace GamessOutput
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

