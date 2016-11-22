using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

namespace UI
{
	[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
	public sealed class ClearableSingletonAttribute : Attribute
	{
	}

	public static class SingletonCleaner
	{
		private static IEnumerable<FieldInfo> sSingletonFields;

		static SingletonCleaner() {
			var clearableAttrType = typeof(ClearableSingletonAttribute);
			var assembly = Assembly.Load("Assembly-CSharp");
			sSingletonFields = assembly.GetTypes()
				.Where(t => t.GetCustomAttributes(clearableAttrType, true).Length > 0)
				.Select(t => t.GetField("sInstance", BindingFlags.NonPublic | BindingFlags.Static));
		}
		
		public static void Clean() {
			foreach (var field in sSingletonFields) {
				field.SetValue(null, null);
			}
			UnityEngine.Debug.Log("Cleared singletons");
		}
	}
}

