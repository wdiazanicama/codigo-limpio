using System.Collections.Generic;

namespace BusinessLayer
{
    public class WebBrowser
	{
		public BrowserName Name { get; set; }
		public int MajorVersion { get; set; }

        public WebBrowser(string name, int majorVersion)
        {
            Name = TranslateStringToBrowserName(name);
			MajorVersion = majorVersion;            
		}

		private BrowserName TranslateStringToBrowserName(string name)
		{
            switch (name)
            {
                case "IE":
                    return BrowserName.InternetExplorer;
                case "FI":
                    return BrowserName.Firefox;
                case "CH":
                    return BrowserName.Chrome;
                case "OP":
                    return BrowserName.Opera;
                case "SA":
                    return BrowserName.Safari;
                case "DO":
                    return BrowserName.Dolphin;
                case "KO":
                    return BrowserName.Konqueror;
                case "LI":
                    return BrowserName.Linx;
                default:
                    return BrowserName.Unknown;
            }
		}

		public enum BrowserName
		{
			Unknown,
			InternetExplorer,
			Firefox,
			Chrome,
			Opera,
			Safari,
			Dolphin,
			Konqueror,
			Linx
		}
	}
}
