namespace EPEmailReplyParser;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public sealed class Email
{
    public readonly Fragment[] Fragments;

    public Email(IEnumerable<Fragment> fragments)
    {
        this.Fragments = fragments.ToArray();
    }

    public string GetVisibleText()
    {
        return this.FilterText(fragment => !fragment.IsHidden);
    }

    public string GetQuotedText()
    {
        return this.FilterText(fragment => fragment.IsQuoted);
    }

    private static readonly Regex Filter = new Regex("~*$");

    private string FilterText(Func<Fragment, bool> filter)
    {
        var filteredFragments = this.Fragments.Where(filter);

        return Filter.Replace(string.Join(@"\n", filteredFragments), "").TrimEnd();
    }
}
