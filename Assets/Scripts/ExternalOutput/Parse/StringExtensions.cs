using System;

namespace ExternalOutput.Parse
{
	public static class StringExtensions
	{
		public static string[] GetWordsSimple(this string str)
		{
			return System.Text.RegularExpressions.Regex.Split(str.Trim(), " +");
		}
	}
}

