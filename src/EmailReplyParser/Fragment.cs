namespace EPEmailReplyParser;

public sealed class Fragment
{
    public string Content { get; private set; }
    public bool IsHidden { get; private set; }
    public bool IsSignature { get; private set; }
    public bool IsQuoted { get; private set; }

    public Fragment(string content, bool isHidden, bool isSignature, bool isQuoted)
    {
        this.Content = content;
        this.IsHidden = isHidden;
        this.IsSignature = isSignature;
        this.IsQuoted = isQuoted;
    }

    public override string ToString()
    {
        return this.Content;
    }
}
