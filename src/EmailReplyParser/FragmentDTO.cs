namespace EPEmailReplyParser;

using System.Collections.Generic;

// ReSharper disable once InconsistentNaming
internal sealed class FragmentDTO
{
    public List<string> Lines;
    public bool IsHidden;
    public bool IsSignature;
    public bool IsQuoted;

    public FragmentDTO()
    {
        this.Lines = new List<string>();
        this.IsHidden = false;
        this.IsSignature = false;
        this.IsQuoted = false;
    }
}
