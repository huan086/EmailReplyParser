# EmailReplyParser

Email reply parser.

Quick .NET (C#) port of https://github.com/crisp-im/email-reply-parser forked from https://github.com/jokokko/EmailReplyParser with update from different ports of the same library.

### Usage
```csharp
var email = EmailParser.Parse(emailContent);
// Amending the default header quote regex patterns with pattern for Outlook displaynames...
var otherEmail = EmailParser.Parse(otherEmail, RegexPatterns.QuoteHeadersRegex.Concat(new [] {new Regex( @"^\s*(From\s?:.+\s?(\[|\().+(\]|\)))", RegexOptions.Compiled)} ).ToArray());

foreach (var fragment in otherEmail.Fragments)
{
    Console.WriteLine(fragment.Content);
}
```

### Credits

* GitHub (https://github.com/github/email_reply_parser)
* William Durand <william.durand1@gmail.com> (https://github.com/willdurand/EmailReplyParser)
* Crisp IM (https://github.com/crisp-dev/email-reply-parser)
* Joona-Pekka Kokko <jokokko@gmail.com> (https://github.com/jokokko/EmailReplyParser)
* LogicSoftware
