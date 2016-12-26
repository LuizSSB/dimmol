using System;
using UnityEngine;

namespace UnityClusterPackage
{
	public static class Constants
	{
		public static string TemporaryFilesPath {
			get {
				return Application.isMobilePlatform ? 
					Application.temporaryCachePath :
					Application.temporaryCachePath;
			}
		}

		public static string ConfigFilesPath {
			get {
				return Application.isMobilePlatform ? 
					Application.persistentDataPath :
					Application.streamingAssetsPath;
			}
		}
	}
}

