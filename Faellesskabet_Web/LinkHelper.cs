using System.Text.RegularExpressions;

namespace Faellesskabet_Web
{
	public static class LinkHelper
	{
		private static Regex _Rex = new Regex(@"(http|ftp|https):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])");

		public static string Convert(string StrWithLink)
		{
			if (StrWithLink == null || StrWithLink == string.Empty)
			{
				return StrWithLink;
			}

			StrWithLink = _Rex.Replace(StrWithLink, "<a target=\"_blank\"" + " href=\"" + "$0" + "\">" + "Link</a>");

			return StrWithLink;
		}

		public static string ReplaceWithText(string StrWithLink, string ReplacementText)
		{

			if (StrWithLink == null || StrWithLink == string.Empty)
			{
				return StrWithLink;
			}

			return _Rex.Replace(StrWithLink, ReplacementText);
		}
	}
}
