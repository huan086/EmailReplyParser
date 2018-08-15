using System.Collections.Generic;

namespace EmailReplyParser
{
	// ReSharper disable once InconsistentNaming
	internal sealed class FragmentDTO
	{
		public List<string> Lines;
		public bool IsHidden;
		public bool IsSignature;
		public bool IsQuoted;

		public FragmentDTO()
		{
			Lines = new List<string>();
			IsHidden = false;
			IsSignature = false;
			IsQuoted = false;
		}
	}
}