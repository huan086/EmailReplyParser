#pragma warning disable xUnit2000
#pragma warning disable xUnit2004
#pragma warning disable xUnit2012
#pragma warning disable xUnit2013

namespace EmailReplyParser.Tests;

using System.IO;
using System.Linq;
using EPEmailReplyParser;
using Xunit;

public sealed class EmailParserTests
{
    private static readonly string COMMON_FIRST_FRAGMENT =
        @"Fusce bibendum, quam hendrerit sagittis tempor, dui turpis tempus erat, pharetra sodales ante sem sit amet metus.
Nulla malesuada, orci non vulputate lobortis, massa felis pharetra ex, convallis consectetur ex libero eget ante.
Nam vel turpis posuere, rhoncus ligula in, venenatis orci. Duis interdum venenatis ex a rutrum.
Duis ut libero eu lectus consequat consequat ut vel lorem. Vestibulum convallis lectus urna,
et mollis ligula rutrum quis. Fusce sed odio id arcu varius aliquet nec nec nibh.".Replace("\r\n", "\n");

    private static Email Get_email(string name)
    {
        var data = File.ReadAllText(Path.Combine("resources", $"{name}.txt"));

        return EPEmailReplyParser.EmailReplyParser.Read(data);
    }

    private static string Get_raw_email(string name)
    {
        var data = File.ReadAllText(Path.Combine("resources", $"{name}.txt"));
        return data;
    }

    [Fact]
    public void Test_complex_body_with_only_one_fragment()
    {
        var email = Get_email("email_5");

        var fragments = email.Fragments;

        Assert.Equal(1, fragments.Length);
    }

    [Fact]
    public void Test_deals_with_multiline_reply_headers()
    {
        var email = Get_email("email_6");

        var fragments = email.Fragments;

        Assert.Equal(true, @"^I get".Test(fragments[0]));
        Assert.Equal(true, @"^On".Test(fragments[1]));
        Assert.Equal(true, @"Was this".Test(fragments[1]));
    }

    [Fact]
    public void Test_email_22()
    {
        var email = Get_email("email_22");

        var fragments = email.Fragments;

        Assert.Equal(COMMON_FIRST_FRAGMENT, fragments[0].ToString().Trim());
    }

    [Fact]
    public void Test_email_23()
    {
        var email = Get_email("email_23");

        var fragments = email.Fragments;

        Assert.Equal(COMMON_FIRST_FRAGMENT, fragments[0].ToString().Trim());
    }

    [Fact]
    public void Test_email_da_into_italian()
    {
        var email = Get_email("email_13");

        var fragments = email.Fragments;

        Assert.Equal(COMMON_FIRST_FRAGMENT, fragments[0].ToString().Trim());
    }

    [Fact]
    public void Test_email_emoji()
    {
        var email = Get_email("email_emoji");

        Assert.Equal("🎉", email.GetVisibleText());
    }

    [Fact]
    public void Test_email_finnish()
    {
        var email = Get_email("email_finnish");

        var fragments = email.Fragments;

        Assert.Equal(COMMON_FIRST_FRAGMENT, fragments[0].ToString().Trim());
    }

    [Fact]
    public void Test_email_german()
    {
        var email = Get_email("email_german");

        var fragments = email.Fragments;

        Assert.Equal(COMMON_FIRST_FRAGMENT, fragments[0].ToString().Trim());
    }

    [Fact]
    public void Test_email_gmail_no()
    {
        var email = Get_email("email_norwegian_gmail");

        var fragments = email.Fragments;

        Assert.Equal(COMMON_FIRST_FRAGMENT, fragments[0].ToString().Trim());
    }

    [Fact]
    public void Test_email_header_polish()
    {
        var email = Get_email("email_14");

        var fragments = email.Fragments;

        Assert.Equal(COMMON_FIRST_FRAGMENT, fragments[0].ToString().Trim());
    }

    [Fact]
    public void Test_email_header_polish_with_date_in_iso8601()
    {
        var email = Get_email("email_17");

        var fragments = email.Fragments;

        Assert.Equal(COMMON_FIRST_FRAGMENT, fragments[0].ToString().Trim());
    }

    [Fact]
    public void Test_email_header_polish_with_dnia_and_napisala()
    {
        var email = Get_email("email_16");

        var fragments = email.Fragments;

        Assert.Equal(COMMON_FIRST_FRAGMENT, fragments[0].ToString().Trim());
    }

    [Fact]
    public void Test_email_not_a_signature()
    {
        var email = Get_email("email_not_a_signature");

        Assert.False(email.Fragments.Any(x => x.IsSignature));
    }

    [Fact]
    public void Test_email_outlook_en()
    {
        var email = Get_email("email_18");

        var fragments = email.Fragments;

        Assert.Equal(COMMON_FIRST_FRAGMENT, fragments[0].ToString().Trim());
    }

    [Fact]
    public void Test_email_portuguese()
    {
        var email = Get_email("email_portuguese");

        var fragments = email.Fragments;

        Assert.Equal(COMMON_FIRST_FRAGMENT, fragments[0].ToString().Trim());
    }

    [Fact]
    public void Test_email_sent_from_my()
    {
        var email = Get_email("email_15");

        var fragments = email.Fragments;

        Assert.Equal(COMMON_FIRST_FRAGMENT, fragments[0].ToString().Trim());
    }

    [Fact]
    public void Test_email_square_brackets()
    {
        var email = Get_email("email_12");

        var fragments = email.Fragments;

        Assert.Equal(COMMON_FIRST_FRAGMENT, fragments[0].ToString().Trim());
    }

    [Fact]
    public void Test_email_whitespace_before_header()
    {
        var email = Get_email("email_11");

        var fragments = email.Fragments;

        Assert.Equal(COMMON_FIRST_FRAGMENT, fragments[0].ToString().Trim());
    }

    [Fact]
    public void Test_email_with_correct_signature()
    {
        var email = Get_email("correct_sig");

        var fragments = email.Fragments;

        Assert.Equal(2, fragments.Length);
        Assert.Equal(false, fragments[1].IsQuoted);
        Assert.Equal(false, fragments[0].IsSignature);
        Assert.Equal(true, fragments[1].IsSignature);
        Assert.Equal(false, fragments[0].IsHidden);
        Assert.Equal(true, fragments[1].IsHidden);

        Assert.Equal(true, @"^--\nrick".Test(fragments[1]));
    }

    [Fact]
    public void Test_email_with_dutch()
    {
        var email = Get_email("email_8");

        var fragments = email.Fragments;

        Assert.Equal(COMMON_FIRST_FRAGMENT, fragments[0].ToString().Trim());
    }

    [Fact]
    public void Test_email_with_hotmail()
    {
        var email = Get_email("email_10");

        var fragments = email.Fragments;

        Assert.Equal(COMMON_FIRST_FRAGMENT, fragments[0].ToString().Trim());
    }

    [Fact]
    public void Test_email_with_italian()
    {
        var email = Get_email("email_7");

        var fragments = email.Fragments;

        Assert.Equal(COMMON_FIRST_FRAGMENT, fragments[0].ToString().Trim());
    }

    [Fact]
    public void Test_email_with_signature()
    {
        var email = Get_email("email_9");

        var fragments = email.Fragments;

        Assert.Equal(COMMON_FIRST_FRAGMENT, fragments[0].ToString().Trim());
    }

    [Fact]
    public void Test_one_is_not_one()
    {
        var email = Get_email("email_one_is_not_on");

        var fragments = email.Fragments;

        Assert.Equal(true, @"One outstanding question".Test(fragments[0]));
        Assert.Equal(true, @"^On Oct 1, 2012".Test(fragments[1]));
    }

    [Fact]
    public void Test_reads_bottom_post()
    {
        var email = Get_email("email_2");

        var fragments = email.Fragments.ToArray();
        Assert.Equal(6, fragments.Count());

        Assert.Equal("Hi,", fragments[0].Content);
        Assert.Equal(true, @"^On [^\:]+\:".Test(fragments[1]));
        Assert.Equal(true, @"^You can list".Test(fragments[2]));
        Assert.Equal(true, @"^>".Test(fragments[3]));
        Assert.Equal(true, @"^_".Test(fragments[5]));
    }

    [Fact]
    public void Test_reads_email_with_signature_with_no_empty_line_above()
    {
        var email = Get_email("sig_no_empty_line");

        var fragments = email.Fragments;

        Assert.Equal(2, fragments.Length);
        Assert.Equal(false, fragments[0].IsQuoted);
        Assert.Equal(false, fragments[1].IsQuoted);

        Assert.Equal(false, fragments[0].IsSignature);
        Assert.Equal(true, fragments[1].IsSignature);

        Assert.Equal(false, fragments[0].IsHidden);
        Assert.Equal(true, fragments[1].IsHidden);

        Assert.Equal(true, @"^--\nrick".Test(fragments[1]));
    }

    [Fact]
    public void Test_reads_simple_body()
    {
        var reply = Get_email("email_1");

        Assert.Equal(3, reply.Fragments.Length);


        Assert.True(reply.Fragments.All(x => !x.IsQuoted));

        Assert.Equal(new[] { false, true, true }, reply.Fragments.Select(x => x.IsSignature));
        Assert.Equal(new[] { false, true, true }, reply.Fragments.Select(x => x.IsHidden));

        Assert.Equal(
            "Hi folks\n\nWhat is the best way to clear a Riak bucket of all key, values after\nrunning a test?\nI am currently using the Java HTTP API.\n",
            reply.Fragments[0].Content);

        Assert.Equal("-Abhishek Kona\n\n", reply.Fragments[1].Content);
    }

    [Fact]
    public void Test_reads_top_post()
    {
        var email = Get_email("email_3");

        var fragments = email.Fragments.ToArray();
        Assert.Equal(5, fragments.Count());
        Assert.Equal(false, fragments[0].IsQuoted);
        Assert.Equal(false, fragments[1].IsQuoted);
        Assert.Equal(true, fragments[2].IsQuoted);
        Assert.Equal(false, fragments[3].IsQuoted);
        Assert.Equal(false, fragments[4].IsQuoted);
        Assert.Equal(false, fragments[0].IsSignature);
        Assert.Equal(true, fragments[1].IsSignature);
        Assert.Equal(false, fragments[2].IsSignature);
        Assert.Equal(false, fragments[3].IsSignature);
        Assert.Equal(true, fragments[4].IsSignature);
        Assert.Equal(false, fragments[0].IsHidden);
        Assert.Equal(true, fragments[1].IsHidden);
        Assert.Equal(true, fragments[2].IsHidden);
        Assert.Equal(true, fragments[3].IsHidden);
        Assert.Equal(true, fragments[4].IsHidden);
        Assert.Equal(true, @"^Oh thanks.\n\nHaving".Test(fragments[0]));
        Assert.Equal(true, @"^-A".Test(fragments[1]));
        Assert.Equal(true, @"^On [^\:]+\:".Test(fragments[2]));
        Assert.Equal(true, @"^_".Test(fragments[4]));
    }

    [Fact]
    public void Test_recognizes_data_string_above_quote()
    {
        var email = Get_email("email_4");

        var fragments = email.Fragments;

        Assert.Equal(true, @"^Awesome".Test(fragments[0]));
        Assert.Equal(true, @"^On".Test(fragments[1]));
        Assert.Equal(true, @"Loader".Test(fragments[1]));
    }

    [Theory]
    [InlineData("Les Misאֳrables")]
    public void Test_graphemes_preserved_in_reversing(string text)
    {
        var email = EPEmailReplyParser.EmailReplyParser.Read(text);

        Assert.Equal(text, email.Fragments[0].Content);
    }

    [Fact]
    public void Test_sent_from()
    {
        var email = Get_email("email_sent_from");

        Assert.Equal("Hi it can happen to any texts you type, as long as you type in between words or paragraphs.", email.GetVisibleText());
    }

    [Fact]
    public void Test_outlook()
    {
        var email = Get_email("outlook");

        Assert.Equal("13:33", email.GetVisibleText());
    }

    [Fact]
    public void Test_outlook_2()
    {
        var email = Get_email("outlook_2");

        Assert.Equal("Outlook email reply with a team mention in the email body.", email.GetVisibleText());
    }


    [Fact]
    public void Test_email_2_3()
    {
        var email = Get_email("email_2_3");

        Assert.Equal("Outlook with a reply directly above line", email.GetVisibleText());
    }

    [Fact]
    public void Test_email_email_abnormal_quote_header_1()
    {
        var email = Get_email("email_abnormal_quote_header_1");

        Assert.Equal("Thank you kindly!", email.GetVisibleText());
    }

    [Fact]
    public void Test_email_email_abnormal_quote_header_2()
    {
        var email = Get_email("email_abnormal_quote_header_2");

        Assert.Equal("Thank you very much for your email!\n\nem—dash coming through..", email.GetVisibleText());
    }

    [Fact]
    public void Test_email_email_abnormal_quote_header_3()
    {
        var email = Get_email("email_abnormal_quote_header_3");

        Assert.Equal("Hi Daniel,\n\n\nThank you very much for your email.\n\nSincerely,\nHomer Simpson\nNuclear Safety Inspector\n\nnuclear power plant, sector 7-G", email.GetVisibleText());
    }

    [Fact]
    public void Test_email_email_abnormal_quote_header_4()
    {
        var email = Get_email("email_abnormal_quote_header_4");

        Assert.Equal("From: Homer Simpson\nTo: Support\n\nEn–dash coming through~\n\nThank you very much for your email!", email.GetVisibleText());
    }

    [Fact]
    public void Test_email_email_abnormal_quote_header_5()
    {
        var email = Get_email("email_abnormal_quote_header_5");

        Assert.Equal("Hello from outlook.com!", email.GetVisibleText());
    }

    [Fact]
    public void Test_email_email_abnormal_quote_header_long()
    {
        var email = Get_email("email_abnormal_quote_header_long");

        Assert.Equal("*Caution* This is a really long email.", email.GetVisibleText());
    }

    [Fact]
    public void Test_email_email_dashes_between_words()
    {
        var email = Get_email("email_dashes_between_words");

        Assert.Equal("The text below is not a signature!\n\nParsing works correctly with mulit--dash between the words.\n\nThis__also!\n\n--Daniel", email.GetVisibleText());
    }

    [Fact]
    public void Test_email_email_em_dash()
    {
        var email = Get_email("email_em_dash");

        Assert.Equal("Thank you.", email.GetVisibleText());
    }

    [Fact]
    public void Test_email_email_en_dash()
    {
        var email = Get_email("email_en_dash");

        Assert.Equal("Thank you.", email.GetVisibleText());
    }

    [Fact]
    public void Test_email_email_gmail()
    {
        var email = Get_email("email_gmail");

        Assert.Equal("This is a test for inbox replying to a github message.", email.GetVisibleText());

    }

    [Fact]
    public void Test_email_email_gmail_split_line_from()
    {
        var email = Get_email("email_gmail_split_line_from");

        Assert.Equal("Fusce bibendum, quam hendrerit sagittis tempor, dui turpis tempus erat, pharetra sodales ante sem sit amet metus.\nNulla malesuada, orci non vulputate lobortis, massa felis pharetra ex, convallis consectetur ex libero eget ante.\nNam vel turpis posuere, rhoncus ligula in, venenatis orci. Duis interdum venenatis ex a rutrum.\nDuis ut libero eu lectus consequat consequat ut vel lorem. Vestibulum convallis lectus urna,\net mollis ligula rutrum quis. Fusce sed odio id arcu varius aliquet nec nec nibh.", email.GetVisibleText());

    }

    [Fact]
    public void Test_email_email_headers_no_delimiter()
    {
        var email = Get_email("email_headers_no_delimiter");

        Assert.Equal("And another reply!", email.GetVisibleText());

    }

    [Fact]
    public void Test_email_email_ios_outlook()
    {
        var email = Get_email("email_ios_outlook");

        Assert.Equal(COMMON_FIRST_FRAGMENT, email.GetVisibleText());
    }

    [Fact]
    public void Test_email_email_msn()
    {
        var email = Get_email("email_msn");

        Assert.Equal(COMMON_FIRST_FRAGMENT, email.GetVisibleText());
    }

    [Fact]
    public void Test_email_email_outlook_split_line_from()
    {
        var email = Get_email("email_outlook_split_line_from");

        Assert.Equal(COMMON_FIRST_FRAGMENT, email.GetVisibleText());
    }

    [Fact]
    public void Test_email_email_partial_quote_header()
    {
        var email = Get_email("email_partial_quote_header");

        Assert.Equal("On your remote host you can run:\n\n     telnet 127.0.0.1 52698\n\nThis should connect to TextMate (on your Mac, via the tunnel). If that\nfails, the tunnel is not working.", email.GetVisibleText());

    }

    [Fact]
    public void Test_email_email_reply_header()
    {
        var email = Get_email("email_reply_header");

        Assert.Equal("This is the latest reply.\n\nThe thread contains two previous replies, and the most recent reply header has a line break in it.\n\nIf we don't patch the line break, the reply header ends up in THIS fragment, instead of in the quoted fragment.\n\nOn the one hand, there could be a line in the reply starting with the word 'On.'\n\nOn the other hand, it could happen TWICE!\n\nStranger", email.GetVisibleText());
    }

    [Fact]
    public void Test_email_email_sig_delimiter_in_middle_of_line()
    {
        var email = Get_email("email_sig_delimiter_in_middle_of_line");

        Assert.Equal("Hi there!\n\nStuff happened.\n\nAnd here is a fix -- this is not a signature.\n\nkthxbai", email.GetVisibleText());
    }

    [Fact]
    public void Test_email_get_outlook()
    {
        var email = Get_email("get_outlook");

        Assert.Equal("\nI am fine, thanks.\n\nJohn.", email.GetVisibleText());
    }

    [Fact]
    public void Test_email_greedy_on()
    {
        var email = Get_email("greedy_on");

        Assert.Equal("On your remote host you can run:\n\n     telnet 127.0.0.1 52698\n\nThis should connect to TextMate (on your Mac, via the tunnel). If that\nfails, the tunnel is not working.", email.GetVisibleText());
    }

    [Fact]
    public void Test_email_pathological()
    {
        var email = Get_email("pathological");

        Assert.Equal("I think you're onto something. I will try to fix the problem as soon as I\nget back to a computer.", email.GetVisibleText());
    }

    [Fact]
    public void Test_email_email_24()
    {
        var email = Get_email("email_24");

        Assert.Equal(COMMON_FIRST_FRAGMENT, email.GetVisibleText());
    }

    [Fact]
    public void Test_email_list_not_signature()
    {
        var email = Get_email("list_not_signature");

        Assert.Equal("Hi folks\nMy list\n- first\n- second\n- third", email.GetVisibleText());
    }

    [Fact]
    public void Test_email_unusial_outlook_format()
    {
        var email = Get_email("unusial_outlook_format");

        Assert.Equal("Outlook with a reply above headers using unusual format", email.GetVisibleText());
    }

    [Fact]
    public void Test_email_yahoo()
    {
        var email = Get_email("yahoo");

        Assert.Equal("Yahoo email reply.", email.GetVisibleText());
    }

    [Fact]
    public void Test_email_webmail()
    {
        var email = Get_email("webmail");

        Assert.Equal("Thank you Rebecca", email.GetVisibleText());
    }
}
