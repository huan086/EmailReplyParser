using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace EmailReplyParser
{
	public sealed class EmailParser
	{
		private static readonly Regex QuoteRegex = new Regex(@"(>+)$", RegexOptions.Compiled);

		private readonly List<FragmentDTO> fragments = new List<FragmentDTO>();

		private EmailParser(Regex[] quoteHeaders, Regex[] signatureRegex)
		{
			quoteHeadersRegex = quoteHeaders;
			this.signatureRegex = signatureRegex;
		}

		private static string StringReverse(string text)
		{			
			var textElements = new List<string>();
			
			// To correctly preserve all graphemes
			var textEnumerator = StringInfo.GetTextElementEnumerator(text);

			while (textEnumerator.MoveNext())
			{
				textElements.Add(textEnumerator.GetTextElement());
			}

			textElements.Reverse();

			return string.Concat(textElements);
		}

		private static string StringRTrim(string text, char mask)
		{
			for (var i = text.Length - 1; i >= 0; i--)
			{
				// ReSharper disable once InvertIf
				if (mask != text[i])
				{
					text = text.Substring(0, i + 1);
					break;
				}
			}
			return text;
		}

		private static readonly Regex RegexWhitespace = new Regex(@"^\s+", RegexOptions.Compiled);

		private static string StringLTrim(string text)
		{
			return RegexWhitespace.Replace(text, string.Empty);
		}

		private static readonly Regex RegexLinefeedWithCarriage = new Regex("\r\n", RegexOptions.Compiled);
		
		public static Email Parse(string text, Regex[] quoteHeaders = null, Regex[] signatureRegex = null)
		{			
			var parser = new EmailParser(quoteHeaders ?? RegexPatterns.QuoteHeadersRegex, signatureRegex ?? RegexPatterns.SignatureRegex);
			return parser.ParseImpl(text);
		}

		private Email ParseImpl(string text)
		{
			text = RegexLinefeedWithCarriage.Replace(text, "\n");

			text = quoteHeadersRegex.Aggregate(text, (sourceText, regex) =>
			{
				var matches = regex.Match(sourceText);

				if (matches.Success)
				{
					var replaceBy = matches.Groups[1].Value.Replace("\n", " ");
					sourceText = sourceText.Replace(matches.Groups[1].Value, replaceBy);
				}

				return sourceText;
			});

			FragmentDTO fragment = null;

			foreach (var newLine in StringReverse(text).Split(new[] { "\n" }, StringSplitOptions.None))
			{
				var line = StringRTrim(newLine, '\n');

				if (!IsSignature(line))
				{
					line = StringLTrim(line);
				}

				if (fragment != null)
				{
					var last = fragment.Lines[fragment.Lines.Count - 1];
					if (IsSignature(last))
					{
						fragment.IsSignature = true;
						AddFragment(fragment);
						fragment = null;
					}
					else if (line == "" && IsQuoteHeader(last))
					{
						fragment.IsQuoted = true;
						AddFragment(fragment);
						fragment = null;
					}
				}

				var isQuoted = IsQuote(line);

				if (fragment == null || !IsFragmentLine(fragment, line, isQuoted))
				{
					if (fragment != null)
					{
						AddFragment(fragment);
					}

					fragment = new FragmentDTO { IsQuoted = isQuoted };
				}

				fragment.Lines.Add(line);
			}

			if (fragment != null)
			{
				AddFragment(fragment);
			}

			var email = CreateEmail(fragments);

			return email;
		}

		private static readonly Regex RegexStartsWithNewline = new Regex(@"^\n", RegexOptions.Compiled);
		private readonly Regex[] quoteHeadersRegex;
		private readonly Regex[] signatureRegex;

		private static Email CreateEmail(List<FragmentDTO> emailFragments)
		{
			emailFragments.Reverse();

			var transformedFragments = emailFragments.Select(fragment => new Fragment(
				RegexStartsWithNewline.Replace(StringReverse(string.Join("\n", fragment.Lines)), string.Empty),
				fragment.IsHidden,
				fragment.IsSignature,
				fragment.IsQuoted));

			return new Email(transformedFragments);
		}

		private bool IsQuoteHeader(string line)
		{
			return quoteHeadersRegex.Any((regex) => regex.IsMatch(StringReverse(line)));
		}

		private bool IsSignature(string line)
		{
			var text = StringReverse(line);

			return signatureRegex.Any(regex => regex.IsMatch(text));
		}

		private static bool IsQuote(string line)
		{
			return QuoteRegex.IsMatch(line);
		}

		private static bool IsEmpty(FragmentDTO fragment)
		{
			return "".Equals(string.Join("", fragment.Lines), StringComparison.Ordinal);
		}

		private bool IsFragmentLine(FragmentDTO fragment, string line, bool isQuoted)
		{
			return fragment.IsQuoted == isQuoted ||
			  fragment.IsQuoted && (IsQuoteHeader(line) || line == "");
		}

		private void AddFragment(FragmentDTO fragment)
		{
			if (fragment.IsQuoted || fragment.IsSignature || IsEmpty(fragment))
			{
				fragment.IsHidden = true;
			}

			fragments.Add(fragment);
		}
	}
}