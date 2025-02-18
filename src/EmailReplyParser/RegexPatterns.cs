namespace EPEmailReplyParser;

using System.Collections.Generic;
using System.Text.RegularExpressions;

public static partial class RegexPatterns
{
    public static readonly IReadOnlyList<Regex> QuoteHeadersRegex =
    [
        OnDateNameEmailWroteEnRegex,
        OnDateNameEmailWroteFrRegex,
        OnDateNameEmailWroteEsRegex,
        OnDateNameEmailWroteItRegex,
        OnDateNameEmailWrotePtRegex,
        OnDateNameEmailWroteDeRegex,
        OnDateNameEmailWroteNlRegex,
        OnDateNameEmailWrotePlRegex,
        OnDateNameEmailWroteSvRegex,
        OnDateNameEmailWroteFiRegex,
        OnDateAndTimeNameEmailWroteDeRegex,
        OnDateNameEmailWroteZhRegex,
        OnDateNameEmailWroteKoRegex,
        OnDateNameEmailWroteJpRegex,
        NameEmailWroteDeRegex,
        NameOnDateWroteEnRegex,
        FromNameEmailEnRegex,
        FromNameEmailDeRegex,
        FromNameEmailEsRegex,
        FromNameEmailNlRegex,
        FromNameEmailPtRegex,
        FromNameEmailZhRegex,
        DateYmdNameEmailRegex,
        DateWroteNameEmailDaRegex,
        DateDmYNameEmailRegex,
        TimeDateNameEmailRegex,
        YearFromNameEmailEnRegex,
        OriginalMessageEnRegex,
        OriginalMessageDaRegex,
        OriginalMessageFrRegex,
    ];

    public static readonly IReadOnlyList<Regex> SignatureRegex =
    [
        SeparatorRegex,
        SeparatorLongDashRegex,
        SentFromEnRegex,
        GetOutlookEnRegex,
        CheersEnRegex,
        BestWishesEnRegex,
        RegardsEnRegex,
        SentByDeRegex,
        SentByForDeRegex,
        SentFromDaRegex,
        SentFromFrRegex,
        SentFromMyFrRegex,
        SentFromByFrRegex,
        GetOutlookFrRegex,
        OkYouFrRegex,
        RegardsFrRegex,
        GoodDayFrRegex,
        SentFromEsRegex,
        ShippedFromNlRegex,
        SentFromNlRegex,
    ];

    #region Quote headers
    [GeneratedRegex(@"^-*\s*(On\s.+\s.+\n?wrote:{0,1})\s{0,1}-*$", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Multiline, matchTimeoutMilliseconds: 3000)]
    private static partial Regex OnDateNameEmailWroteEnRegex { get; }

    [GeneratedRegex(@"^-*\s*(Le\s.+\s.+\n?écrit\s?:{0,1})\s{0,1}-*$", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Multiline, matchTimeoutMilliseconds: 3000)]
    private static partial Regex OnDateNameEmailWroteFrRegex { get; }

    [GeneratedRegex(@"^-*\s*(El\s.+\s.+\n?escribió:{0,1})\s{0,1}-*$", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Multiline, matchTimeoutMilliseconds: 3000)]
    private static partial Regex OnDateNameEmailWroteEsRegex { get; }

    [GeneratedRegex(@"^-*\s*(Il\s.+\s.+\n?scritto:{0,1})\s{0,1}-*$", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Multiline, matchTimeoutMilliseconds: 3000)]
    private static partial Regex OnDateNameEmailWroteItRegex { get; }

    [GeneratedRegex(@"^-*\s*(Em\s.+\s.+\n?escreveu:{0,1})\s{0,1}-*$", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Multiline, matchTimeoutMilliseconds: 3000)]
    private static partial Regex OnDateNameEmailWrotePtRegex { get; }

    [GeneratedRegex(@"^\s*(Am\s.+\s)\n?\n?schrieb.+\s?(\[|<).+(\]|>):$", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Multiline, matchTimeoutMilliseconds: 3000)]
    private static partial Regex OnDateNameEmailWroteDeRegex { get; }

    [GeneratedRegex(@"^\s*(Op\s[\s\S]+?\n?schreef[\s\S]+:)$", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Multiline, matchTimeoutMilliseconds: 3000)]
    private static partial Regex OnDateNameEmailWroteNlRegex { get; }

    [GeneratedRegex(@"^\s*((W\sdniu|Dnia)\s[\s\S]+?(pisze|napisał(\(a\))?):)$", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Multiline, matchTimeoutMilliseconds: 3000)]
    private static partial Regex OnDateNameEmailWrotePlRegex { get; }

    [GeneratedRegex(@"^\s*(Den\s.+\s\n?skrev\s.+:)$", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Multiline, matchTimeoutMilliseconds: 3000)]
    private static partial Regex OnDateNameEmailWroteSvRegex { get; }

    [GeneratedRegex(@"^\s*(pe\s.+\s.+\n?kirjoitti:)$", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Multiline, matchTimeoutMilliseconds: 3000)]
    private static partial Regex OnDateNameEmailWroteFiRegex { get; }

    [GeneratedRegex(@"^\s*(Am\s.+\sum\s.+\s\n?schrieb\s.+:)$", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Multiline, matchTimeoutMilliseconds: 3000)]
    private static partial Regex OnDateAndTimeNameEmailWroteDeRegex { get; }

    [GeneratedRegex(@"^(在[\s\S]+写道：)$", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Multiline, matchTimeoutMilliseconds: 3000)]
    private static partial Regex OnDateNameEmailWroteZhRegex { get; }

    [GeneratedRegex(@"^(20[0-9]{2}\..+\s작성:)$", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Multiline, matchTimeoutMilliseconds: 3000)]
    private static partial Regex OnDateNameEmailWroteKoRegex { get; }

    [GeneratedRegex(@"^(20[0-9]{2}\/.+のメッセージ:)$", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Multiline, matchTimeoutMilliseconds: 3000)]
    private static partial Regex OnDateNameEmailWroteJpRegex { get; }

    [GeneratedRegex(@"^(.+[\t\p{Zs}]<.+>\sschrieb:)$", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Multiline, matchTimeoutMilliseconds: 3000)]
    private static partial Regex NameEmailWroteDeRegex { get; }

    [GeneratedRegex(@"^(.+[\t\p{Zs}]on.*at.*wrote:)$", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Multiline, matchTimeoutMilliseconds: 3000)]
    private static partial Regex NameOnDateWroteEnRegex { get; }

    [GeneratedRegex(@"^\s*(From\s?:.+\s?\n?\s*[\[|<].+[\]|>])", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Multiline, matchTimeoutMilliseconds: 3000)]
    private static partial Regex FromNameEmailEnRegex { get; }

    [GeneratedRegex(@"^\s*(Von\s?:.+\s?\n?\s*[\[|<].+[\]|>])", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Multiline, matchTimeoutMilliseconds: 3000)]
    private static partial Regex FromNameEmailDeRegex { get; }

    [GeneratedRegex(@"^\s*(De\s?:.+\s?\n?\s*(\[|<).+(\]|>))", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Multiline, matchTimeoutMilliseconds: 3000)]
    private static partial Regex FromNameEmailEsRegex { get; }

    [GeneratedRegex(@"^\s*(Van\s?:.+\s?\n?\s*(\[|<).+(\]|>))", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Multiline, matchTimeoutMilliseconds: 3000)]
    private static partial Regex FromNameEmailNlRegex { get; }

    [GeneratedRegex(@"^\s*(Da\s?:.+\s?\n?\s*(\[|<).+(\]|>))", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Multiline, matchTimeoutMilliseconds: 3000)]
    private static partial Regex FromNameEmailPtRegex { get; }

    [GeneratedRegex(@"^\s*(寄件者\s?[:：].+\s?\n?\s*(\[|<).+(\]|>))", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Multiline, matchTimeoutMilliseconds: 3000)]
    private static partial Regex FromNameEmailZhRegex { get; }

    [GeneratedRegex(@"^(20[0-9]{2})-([0-9]{2}).([0-9]{2}).([0-9]{2}):([0-9]{2})\n?(.*)>:$", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Multiline, matchTimeoutMilliseconds: 3000)]
    private static partial Regex DateYmdNameEmailRegex { get; }

    [GeneratedRegex(@"^\s*([a-z]{3,4}\.\s[\s\S]+\sskrev\s[\s\S]+:)$", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Multiline, matchTimeoutMilliseconds: 3000)]
    private static partial Regex DateWroteNameEmailDaRegex { get; }

    [GeneratedRegex(@"^([0-9]{2}).([0-9]{2}).(20[0-9]{2})(.*)(([0-9]{2}).([0-9]{2}))(.*)""( *)<(.*)>( *):$", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Multiline, matchTimeoutMilliseconds: 3000)]
    private static partial Regex DateDmYNameEmailRegex { get; }

    [GeneratedRegex(@"^[0-9]{2}:[0-9]{2}(.*)[0-9]{4}(.*)""( *)<(.*)>( *):$", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Multiline, matchTimeoutMilliseconds: 3000)]
    private static partial Regex TimeDateNameEmailRegex { get; }

    [GeneratedRegex(@"^(.*)[0-9]{4}(.*)from(.*)<(.*)>:$", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Multiline, matchTimeoutMilliseconds: 3000)]
    private static partial Regex YearFromNameEmailEnRegex { get; }

    [GeneratedRegex(@"^[\t\p{Zs}]*-{1,12} ?original message ?-{1,12}$", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Multiline, matchTimeoutMilliseconds: 3000)]
    private static partial Regex OriginalMessageEnRegex { get; }

    [GeneratedRegex(@"^[\t\p{Zs}]*-{1,12} ?oprindelig besked ?-{1,12}$", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Multiline, matchTimeoutMilliseconds: 3000)]
    private static partial Regex OriginalMessageDaRegex { get; }

    [GeneratedRegex(@"^[\t\p{Zs}]*-{1,12} ?message d'origine ?-{1,12}$", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Multiline, matchTimeoutMilliseconds: 3000)]
    private static partial Regex OriginalMessageFrRegex { get; }
    #endregion

    #region Signatures
    [GeneratedRegex(@"^\s*[-_+=]{2,4}$", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Multiline, matchTimeoutMilliseconds: 3000)]
    private static partial Regex SeparatorRegex { get; }

    [GeneratedRegex(@"^________________________________$", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Multiline, matchTimeoutMilliseconds: 3000)]
    private static partial Regex SeparatorLongDashRegex { get; }

    // EN
    [GeneratedRegex(@"^Sent from (?:\s*.+)$", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Multiline, matchTimeoutMilliseconds: 3000)]
    private static partial Regex SentFromEnRegex { get; }

    [GeneratedRegex(@"^Get Outlook for (?:\s*.+).*", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Multiline, matchTimeoutMilliseconds: 3000)]
    private static partial Regex GetOutlookEnRegex { get; }

    [GeneratedRegex(@"^Cheers,?!?$", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Multiline, matchTimeoutMilliseconds: 3000)]
    private static partial Regex CheersEnRegex { get; }

    [GeneratedRegex(@"^Best wishes,?!?$", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Multiline, matchTimeoutMilliseconds: 3000)]
    private static partial Regex BestWishesEnRegex { get; }

    [GeneratedRegex(@"^\w{0,20}\s?(\sand\s)?Regards,?!?！?$", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Multiline, matchTimeoutMilliseconds: 3000)]
    private static partial Regex RegardsEnRegex { get; }

    // DE
    [GeneratedRegex(@"^Von (?:\s*.+) gesendet$", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Multiline, matchTimeoutMilliseconds: 3000)]
    private static partial Regex SentByDeRegex { get; }

    [GeneratedRegex(@"^Gesendet von (?:\s*.+) für (?:\s*.+)$", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Multiline, matchTimeoutMilliseconds: 3000)]
    private static partial Regex SentByForDeRegex { get; }

    // DA
    [GeneratedRegex(@"^Sendt fra (?:\s*.+)$", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Multiline, matchTimeoutMilliseconds: 3000)]
    private static partial Regex SentFromDaRegex { get; }

    // FR
    [GeneratedRegex(@"^Envoyé depuis (?:\s*.+)$", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Multiline, matchTimeoutMilliseconds: 3000)]
    private static partial Regex SentFromFrRegex { get; }

    [GeneratedRegex(@"^Envoyé de mon (?:\s*.+)$", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Multiline, matchTimeoutMilliseconds: 3000)]
    private static partial Regex SentFromMyFrRegex { get; }

    [GeneratedRegex(@"^Envoyé à partir de (?:\s*.+)$", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Multiline, matchTimeoutMilliseconds: 3000)]
    private static partial Regex SentFromByFrRegex { get; }

    [GeneratedRegex(@"^Télécharger Outlook pour (?:\s*.+).*", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Multiline, matchTimeoutMilliseconds: 3000)]
    private static partial Regex GetOutlookFrRegex { get; }

    [GeneratedRegex(@"^Bien . vous,?!?$", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Multiline, matchTimeoutMilliseconds: 3000)]
    private static partial Regex OkYouFrRegex { get; }

    [GeneratedRegex(@"^\w{0,20}\s?cordialement,?!?$", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Multiline, matchTimeoutMilliseconds: 3000)]
    private static partial Regex RegardsFrRegex { get; }

    [GeneratedRegex(@"^Bonne (journ.e|soir.e)!?$", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Multiline, matchTimeoutMilliseconds: 3000)]
    private static partial Regex GoodDayFrRegex { get; }

    // ES
    [GeneratedRegex(@"^Enviado desde (?:\s*.+)$", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Multiline, matchTimeoutMilliseconds: 3000)]
    private static partial Regex SentFromEsRegex { get; }

    // NL
    [GeneratedRegex(@"^Verzonden vanaf (?:\s*.+)$", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Multiline, matchTimeoutMilliseconds: 3000)]
    private static partial Regex ShippedFromNlRegex { get; }

    [GeneratedRegex(@"^Verstuurd vanaf (?:\s*.+)$", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Multiline, matchTimeoutMilliseconds: 3000)]
    private static partial Regex SentFromNlRegex { get; }
    #endregion
}
