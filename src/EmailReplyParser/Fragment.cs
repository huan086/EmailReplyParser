namespace EPEmailReplyParser
{
	public sealed class Fragment
	{
		public readonly string Content;
		public readonly bool IsHidden;
		public readonly bool IsSignature;
		public readonly bool IsQuoted;

		public Fragment(string content, bool isHidden, bool isSignature, bool isQuoted)
		{
			Content = content;
			IsHidden = isHidden;
			IsSignature = isSignature;
			IsQuoted = isQuoted;
		}

		public override string ToString()
		{
			return Content;
		}
	}
}