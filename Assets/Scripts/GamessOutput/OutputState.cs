using System;

namespace GamessOutput
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

