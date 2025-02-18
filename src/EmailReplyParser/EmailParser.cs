namespace EPEmailReplyParser;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public sealed partial class EmailParser
{
    [GeneratedRegex(@"^(>+)", RegexOptions.CultureInvariant, matchTimeoutMilliseconds: 3000)]
    private static partial Regex QuoteRegex { get; }

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
        var remainingSpan = text.AsSpan();
        int indexOfNewLine;
        do
        {
            indexOfNewLine = remainingSpan.LastIndexOf('\n');
            var line = remainingSpan[(indexOfNewLine + 1)..].TrimStart('\n');
            if (!this.IsSignature(line))
            {
                line = line.TrimEnd();
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

            fragment.Lines.Add(line.ToString());

            if (indexOfNewLine >= 0)
            {
                remainingSpan = remainingSpan[..indexOfNewLine];
            }
        }
        while (indexOfNewLine >= 0);

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
        var transformedFragments = emailFragments
            .Reverse<FragmentDto>()
            .Select(fragment =>
            {
                var lines = string.Join("\n", fragment.Lines.Reverse<string>());
                if (lines.Length > 0 && lines[0] == '\n')
                {
                    lines = lines[1..];
                }

                return new Fragment(
                    lines,
                    fragment.IsHidden,
                    fragment.IsSignature,
                    fragment.IsQuoted);
            });

        return new Email(transformedFragments);
    }

    private bool IsQuoteHeader(ReadOnlySpan<char> line)
    {
        foreach (var regex in this.quoteHeadersRegex)
        {
            if (regex.IsMatch(line))
            {
                return true;
            }
        }

        return false;
    }

    private bool IsSignature(ReadOnlySpan<char> line)
    {
        foreach (var regex in this.signatureRegex)
        {
            if (regex.IsMatch(line))
            {
                return true;
            }
        }

        return false;
    }

    private static bool IsQuote(ReadOnlySpan<char> line)
    {
        return QuoteRegex.IsMatch(line);
    }

    private static bool IsEmpty(FragmentDto fragment)
    {
        return fragment.Lines.All(line => string.Empty.Equals(line, StringComparison.Ordinal));
    }

    private bool IsFragmentLine(FragmentDto fragment, ReadOnlySpan<char> line, bool isQuoted)
    {
        return fragment.IsQuoted == isQuoted ||
          (fragment.IsQuoted && (this.IsQuoteHeader(line) || line.Length == 0));
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
