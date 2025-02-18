namespace EPEmailReplyParser;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

public sealed partial class EmailParser
{
    [GeneratedRegex(@"(>+)$", RegexOptions.CultureInvariant, matchTimeoutMilliseconds: 3000)]
    private static partial Regex QuoteRegex { get; }

    [GeneratedRegex(@"^\n", RegexOptions.CultureInvariant, matchTimeoutMilliseconds: 3000)]
    private static partial Regex RegexStartsWithNewline { get; }

    [GeneratedRegex(@"^\s+", RegexOptions.CultureInvariant, matchTimeoutMilliseconds: 3000)]
    private static partial Regex RegexWhitespace { get; }

    [GeneratedRegex(@"\r\n", RegexOptions.CultureInvariant, matchTimeoutMilliseconds: 3000)]
    private static partial Regex RegexLinefeedWithCarriage { get; }

    private readonly IReadOnlyList<Regex> quoteHeadersRegex;
    private readonly IReadOnlyList<Regex> signatureRegex;

    private readonly List<FragmentDto> fragments = new List<FragmentDto>();

    private EmailParser(IReadOnlyList<Regex> quoteHeaders, IReadOnlyList<Regex> signatureRegex)
    {
        this.quoteHeadersRegex = quoteHeaders;
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
                text = text[..(i + 1)];
                break;
            }
        }

        return text;
    }

    private static string StringLTrim(string text)
    {
        return RegexWhitespace.Replace(text, string.Empty);
    }

    public static Email Parse(string text, IReadOnlyList<Regex> quoteHeaders = null, IReadOnlyList<Regex> signatureRegex = null)
    {
        var parser = new EmailParser(quoteHeaders ?? RegexPatterns.QuoteHeadersRegex, signatureRegex ?? RegexPatterns.SignatureRegex);
        return parser.ParseImpl(text);
    }

    private string FixBrokenSignatures(string text)
    {
        text = this.quoteHeadersRegex.Aggregate(text, (sourceText, regex) =>
        {
            var matches = regex.Match(sourceText);
            if (matches.Success
                && matches.Groups.Count > 1
                && matches.Groups[1].ValueSpan.Contains('\n'))
            {
                var replaceBy = matches.Groups[1].Value.Replace('\n', ' ');
                sourceText = sourceText.Replace(matches.Groups[1].Value, replaceBy);
            }

            return sourceText;
        });
        return text;
    }

    private Email ParseImpl(string text)
    {
        text = RegexLinefeedWithCarriage.Replace(text, "\n");
        text = this.FixBrokenSignatures(text);

        FragmentDto fragment = null;
        foreach (var newLine in StringReverse(text).Split("\n", StringSplitOptions.None))
        {
            var line = StringRTrim(newLine, '\n');

            if (!this.IsSignature(line))
            {
                line = StringLTrim(line);
            }

            if (fragment != null)
            {
                var last = fragment.Lines[^1];
                if (this.IsSignature(last))
                {
                    fragment.IsSignature = true;
                    this.AddFragment(fragment);
                    fragment = null;
                }
                else if (line.Length == 0 && this.IsQuoteHeader(last))
                {
                    fragment.IsQuoted = true;
                    this.AddFragment(fragment);
                    fragment = null;
                }
            }

            var isQuoted = IsQuote(line);
            if (fragment == null || !this.IsFragmentLine(fragment, line, isQuoted))
            {
                if (fragment != null)
                {
                    this.AddFragment(fragment);
                }

                fragment = new FragmentDto { IsQuoted = isQuoted };
            }

            fragment.Lines.Add(line);
        }

        if (fragment != null)
        {
            this.AddFragment(fragment);
        }

        var email = CreateEmail(this.fragments);
        this.fragments.Clear();
        return email;
    }

    private static Email CreateEmail(List<FragmentDto> emailFragments)
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
        return this.quoteHeadersRegex.Any((regex) => regex.IsMatch(StringReverse(line)));
    }

    private bool IsSignature(string line)
    {
        var text = StringReverse(line);

        return this.signatureRegex.Any(regex => regex.IsMatch(text));
    }

    private static bool IsQuote(string line)
    {
        return QuoteRegex.IsMatch(line);
    }

    private static bool IsEmpty(FragmentDto fragment)
    {
        return fragment.Lines.All(line => string.Empty.Equals(line, StringComparison.Ordinal));
    }

    private bool IsFragmentLine(FragmentDto fragment, string line, bool isQuoted)
    {
        return fragment.IsQuoted == isQuoted ||
          (fragment.IsQuoted && (this.IsQuoteHeader(line) || string.Empty.Equals(line, StringComparison.Ordinal)));
    }

    private void AddFragment(FragmentDto fragment)
    {
        if (fragment.IsQuoted || fragment.IsSignature || IsEmpty(fragment))
        {
            fragment.IsHidden = true;
        }

        this.fragments.Add(fragment);
    }
}
