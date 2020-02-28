using System.Text.RegularExpressions;

namespace EPEmailReplyParser
{
	public static class RegexPatterns
	{
		public static readonly Regex[] QuoteHeadersRegex = {
			new Regex(@"^\s*(On(?:(?!^>*\s*On\b|\bwrote(:)?)[\s\S]){0,1000}wrote:?)$", RegexOptions.Multiline | RegexOptions.Compiled), // On DATE, NAME <EMAIL> wrote:
			new Regex(@"^\s*(Le(?:(?!^>*\s*Le\b|\bécrit:)[\s\S]){0,1000}écrit :)$", RegexOptions.Multiline | RegexOptions.Compiled), // Le DATE, NAME <EMAIL> a écrit :
			new Regex(
				@"^\s*(El(?:(?!^>*\s*El\b|\bescribió:)[\s\S]){0,1000}escribió:)$", RegexOptions.Multiline | RegexOptions.Compiled), // El DATE, NAME <EMAIL> escribió:
			new Regex(
				@"^\s*(Il(?:(?!^>*\s*Il\b|\bscritto:)[\s\S]){0,1000}scritto:)$", RegexOptions.Multiline | RegexOptions.Compiled), // Il DATE, NAME <EMAIL> ha scritto:
			new Regex(
				@"^\s*(Em(?:(?!^>*\s*Em\b|\bescreveu:)[\s\S]){0,1000}escreveu:)$", RegexOptions.Multiline | RegexOptions.Compiled), // Em DATE, NAME <EMAIL>escreveu:
			new Regex(@"^\s*(Am\s.+\s)schrieb.+\s?(\[|<).+(\]|>):$", RegexOptions.Multiline | RegexOptions.Compiled), // Am DATE schrieb NAME <EMAIL>:

			new Regex(@"^\s*(Op\s[\s\S]+?schreef[\s\S]+:)$", RegexOptions.Multiline | RegexOptions.Compiled), // Il DATE, schreef NAME <EMAIL>:
			new Regex(
				@"^\s*((W\sdniu|Dnia)\s[\s\S]+?(pisze|napisał(\(a\))?):)$", RegexOptions.Multiline | RegexOptions.Compiled), // W dniu DATE, NAME <EMAIL> pisze|napisał:
			new Regex(@"^\s*(Den\s.+\sskrev\s.+:)$", RegexOptions.Multiline | RegexOptions.Compiled), // Den DATE skrev NAME <EMAIL>:
			new Regex(@"^\s*(pe\s.+\s.+kirjoitti:)$", RegexOptions.Multiline | RegexOptions.Compiled), // pe DATE NAME <EMAIL> kirjoitti: 
			new Regex(@"^\s*(Am\s.+\sum\s.+\sschrieb\s.+:)$", RegexOptions.Multiline | RegexOptions.Compiled), // Am DATE um TIME schrieb NAME:
			new Regex(@"^(在[\s\S]+写道：)$", RegexOptions.Multiline | RegexOptions.Compiled), // > 在 DATE, TIME, NAME 写道：
			new Regex(@"^(20[0-9]{2}\..+\s작성:)$", RegexOptions.Multiline | RegexOptions.Compiled), // DATE TIME NAME 작성:
			new Regex(@"^(20[0-9]{2}\/.+のメッセージ:)$", RegexOptions.Multiline | RegexOptions.Compiled), // DATE TIME、NAME のメッセージ:
			new Regex(@"^(.+\s<.+>\sschrieb:)$", RegexOptions.Multiline | RegexOptions.Compiled), // NAME <EMAIL> schrieb:
			new Regex(@"^(.+\son.*at.*wrote:)$", RegexOptions.Multiline | RegexOptions.Compiled), // NAME on DATE wrote:

            new Regex(
				@"^\s*(Date\s?:[^\n]+\n?([^\n]+\n?){0,2}From\s?:[^\n]+\n?([^\n]+\n?){0,2}To\s?:[^\n]+\n?([^\n]+\n?){0,2}Subject\s?:[^\n]+)", RegexOptions.Multiline | RegexOptions.Compiled), // for headers DATE goes first

            new Regex(
				@"^\s*(To\s?:[^\n]+\n?([^\n]+\n?){0,2}From\s?:[^\n]+\n?([^\n]+\n?){0,2}Subject\s?:[^\n]+)", RegexOptions.Multiline | RegexOptions.Compiled),// for headers TO goes first

            new Regex(
				@"^\s*(\*?From\s?:[^\n]+\n?([^\n]+\n?){0,2}\*?To\s?:[^\n]+\n?([^\n]+\n?){0,2}\*?Subject\s?:[^\n]+)", RegexOptions.Multiline | RegexOptions.Compiled),// for headers FROM goes first



			//new Regex(
		//		@"^\s*(From\s?:.+\s?\n?\s*[\[|<].+[\]|>])", RegexOptions.Multiline |RegexOptions.Compiled), // "From: NAME <EMAIL>" OR "From : NAME <EMAIL>" OR "From : NAME<EMAIL>"(With support whitespace before start and before <)
			new Regex(
				@"\s*(De\s?:.+\s?\n?\s*(\[|<).+(\]|>))", RegexOptions.Multiline |RegexOptions.Compiled), // "De: NAME <EMAIL>" OR "De : NAME <EMAIL>" OR "De : NAME<EMAIL>"  (With support whitespace before start and before <)
			new Regex(
				@"^\s*(Van\s?:.+\s?\n?\s*(\[|<).+(\]|>))", RegexOptions.Multiline |RegexOptions.Compiled), // "Van: NAME <EMAIL>" OR "Van : NAME <EMAIL>" OR "Van : NAME<EMAIL>"  (With support whitespace before start and before <)
			new Regex(
				@"^\s*(Da\s?:.+\s?\n?\s*(\[|<).+(\]|>))", RegexOptions.Multiline |RegexOptions.Compiled ), // "Da: NAME <EMAIL>" OR "Da : NAME <EMAIL>" OR "Da : NAME<EMAIL>"  (With support whitespace before start and before <)
			new Regex(
				@"^(20[0-9]{2})-([0-9]{2}).([0-9]{2}).([0-9]{2}):([0-9]{2})*.(.*)?\n?(.*)>:$", RegexOptions.Multiline | RegexOptions.Compiled), // 20YY-MM-DD HH:II GMT+01:00 NAME <EMAIL>:
			new Regex(@"^\s*([a-z]{3,4}\.\s[\s\S]+\sskrev\s[\s\S]+:)$", RegexOptions.Multiline | RegexOptions.Compiled), // DATE skrev NAME <EMAIL>:
			new Regex(
				@"^([0-9]{2}).([0-9]{2}).(20[0-9]{2})(.*)(([0-9]{2}).([0-9]{2}))(.*)""( *)<(.*)>( *):$", RegexOptions.Multiline | RegexOptions.Compiled), // DD.MM.20YY HH:II NAME <EMAIL>
            
			   };

        public static readonly Regex[] SignatureRegex =
        {
            new Regex(@"^\s*-{2,4}$", RegexOptions.Compiled),
            new Regex(@"^\s*_{2,4}$", RegexOptions.Compiled),
            new Regex(@"^—", RegexOptions.Compiled),
            new Regex(@"^—\w", RegexOptions.Compiled),
            new Regex(@"^-\w", RegexOptions.Compiled),
            new Regex(@"^\u2013\w", RegexOptions.Compiled),
            new Regex(@"^\u2014\w", RegexOptions.Compiled),
            new Regex(@"^-- $", RegexOptions.Compiled),
            new Regex(@"^-- \s*.+$", RegexOptions.Compiled),
            new Regex(@"^Sent from (?:\s*.+)$", RegexOptions.Compiled),
            new Regex(@"^Envoyé depuis (?:\s*.+)$", RegexOptions.Compiled),
            new Regex(@"^Enviado desde (?:\s*.+)$", RegexOptions.Compiled),
            new Regex(@"^\+{2,4}$", RegexOptions.Compiled),
            new Regex(@"^\={2,4}$", RegexOptions.Compiled),
            new Regex(@"^________________________________$", RegexOptions.Compiled),
            new Regex(@"^Get Outlook for (iOS|Android)\s?<https?://[a-z0-9.-]+[a-zA-Z0-9/.,_:;#?%!@$&'()*+~=-]*>.*$", RegexOptions.Compiled),
            new Regex(@"^Outlook für (iOS|Android) beziehen\s?<https?://[a-z0-9.-]+[a-zA-Z0-9/.,_:;#?%!@$&'()*+~=-]*>.*$", RegexOptions.Compiled),

            new Regex(@"^Diese Nachricht wurde von mein.* gesendet\.?$", RegexOptions.Compiled),
            new Regex(@"^Von mein.* gesendet\.?$", RegexOptions.Compiled),
            new Regex(@"^Gesendet von mein.* ([a-zA-Z0-9_-]+\s*){1,3}\.?$", RegexOptions.Compiled),
            new Regex(@"^\s*--------\s*Original\s?Message\s*--------\s*$", RegexOptions.Compiled),

            //  ~R/^Diese Nachricht wurde von mein.* gesendet\.?$/,
            // ~r/^Von mein.* gesendet\.?$/ ])	        ~R/^Von mein.* gesendet\.?$/,
            //  ~R/^Gesendet von mein.* ([a-zA-Z0-9_-]+\s*){1,3}\.?$/,
        };
	}
}