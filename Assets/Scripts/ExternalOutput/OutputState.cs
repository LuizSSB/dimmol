using System;

namespace ExternalOutput
{
	[Serializable]
	public class OutputState
	{
		public OutputState()
		{
			Atoms = new System.Collections.Generic.List<Atom>();
		}
		
		public System.Collections.Generic.List<Atom> Atoms;
		public float Energy;
	}
}

