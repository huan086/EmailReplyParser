namespace EPEmailReplyParser;

using System.Collections.Generic;

// ReSharper disable once InconsistentNaming
internal sealed class FragmentDto
{
    public List<string> Lines { get; set; } = new List<string>();
    public bool IsHidden { get; set; }
    public bool IsSignature { get; set; }
    public bool IsQuoted { get; set; }
}
