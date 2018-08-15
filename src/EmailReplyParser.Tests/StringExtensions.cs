using System.Text.RegularExpressions;

namespace EmailReplyParser.Tests
{
	public static class StringExtensions
	{
		public static bool Test(this string value, Fragment fragment)
		{
			var r = new Regex(value);
			return r.IsMatch(fragment.Content);
		}
	}
}