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

		List<List<Atom>> AtomsStates {
			get;
			set;
		}

		IParser ParseLine (string line, int lineNumber);
	}

	public abstract	class BaseParser : IParser
	{
		protected BaseParser(List<List<Atom>> statesSoFar = null, Dictionary<string, object> userInfo = null)
		{
			AtomsStates = statesSoFar ?? new List<List<Atom>>();
			UserInfo = userInfo ?? new Dictionary<string, object>();
		}

		#region IParser implementation
		public abstract IParser ParseLine (string line, int lineNumber);

		public Dictionary<string, object> UserInfo {
			get;
			set;
		}

		public List<List<Atom>> AtomsStates {
			get;
			set;
		}
		#endregion
	}
}

