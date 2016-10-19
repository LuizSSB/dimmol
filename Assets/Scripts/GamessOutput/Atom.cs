using System;

namespace GamessOutput
{
	[Serializable]
	public class Atom
	{
		public string Id;

		public float Charge;

		public float FloatX {
			get {
				return float.Parse(X);
			}
			set {
				X = value.ToString();
			}
		}

		public float FloatY {
			get {
				return float.Parse(Y);
			}
			set {
				Y = value.ToString();
			}
		}

		public float FloatZ {
			get {
				return float.Parse(Z);
			}
			set {
				Z = value.ToString();
			}
		}

		public string X;

		public string Y;

		public string Z;
	}
}

