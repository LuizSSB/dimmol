using System;
using UnityEngine;

namespace AssemblyCSharp
{
	public static class PlatformUtils
	{
		public static bool IsMobile {
			get {
				return Application.platform == RuntimePlatform.Android ||
					Application.platform == RuntimePlatform.IPhonePlayer;
			}
		}

		public static bool IsDesktop {
			get {
				return Application.platform == RuntimePlatform.WindowsEditor ||
					Application.platform == RuntimePlatform.WindowsPlayer ||
					Application.platform == RuntimePlatform.OSXEditor ||
					Application.platform == RuntimePlatform.OSXPlayer;
			}
		}
	}
}

