namespace EPEmailReplyParser;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public sealed partial class Email
{
    [GeneratedRegex(@"~*$", RegexOptions.CultureInvariant, matchTimeoutMilliseconds: 3000)]
    private static partial Regex Filter { get; }

    public IReadOnlyList<Fragment> Fragments { get; }

    public Email(IEnumerable<Fragment> fragments)
    {
        this.Fragments = fragments.ToList().AsReadOnly();
    }

    public string GetVisibleText()
    {
        return this.FilterText(fragment => !fragment.IsHidden);
    }

    public string GetQuotedText()
    {
        return this.FilterText(fragment => fragment.IsQuoted);
    }

    private string FilterText(Func<Fragment, bool> filter)
    {
        var filteredFragments = this.Fragments.Where(filter);

        return Filter.Replace(string.Join(@"\n", filteredFragments), "").TrimEnd();
    }
}
