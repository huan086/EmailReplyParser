namespace EPEmailReplyParser;

public sealed class Fragment
{
    public readonly string Content;
    public readonly bool IsHidden;
    public readonly bool IsSignature;
    public readonly bool IsQuoted;

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
