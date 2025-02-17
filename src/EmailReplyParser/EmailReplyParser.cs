namespace EPEmailReplyParser;

public static class EmailReplyParser
{
    public static Email Read(string text)
    {
        return EmailParser.Parse(text);
    }

    // ReSharper disable once UnusedMember.Global
    public static string ParseReply(string text)
    {
        return Read(text).GetVisibleText();
    }

    // ReSharper disable once UnusedMember.Global
    public static string ParseReplied(string text)
    {
        return Read(text).GetQuotedText();
    }
}
