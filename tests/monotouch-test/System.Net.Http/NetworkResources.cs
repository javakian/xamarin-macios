using System;
using Foundation;

namespace MonoTests.System.Net.Http
{
	[Preserve (AllMembers = true)]
	public static class NetworkResources
	{
		public static readonly string MicrosoftUrl = "https://www.microsoft.com";
		public static readonly Uri MicrosoftUri = new Uri (MicrosoftUrl);
		public static readonly string XamarinUrl = "https://xamarin.com";
		public static readonly Uri XamarinUri = new Uri (XamarinUrl);

		public static readonly string [] Urls = {
			MicrosoftUrl,
		};

		// Robots urls, useful when we want to get a small file
		public static readonly string MicrosoftRobotsUrl = "https://www.microsoft.com/robots.txt";
		public static readonly string XamarinRobotsUrl = "https://www.xamarin.com/robots.txt";
		public static readonly string BingRobotsUrl = "http://www.bing.com/robots.txt";
		public static readonly string XboxRobotsUrl = "https://www.xbox.com/robots.txt";
		public static readonly string MSNRobotsUrl = "https://www.msn.com/robots.txt";
		public static readonly string VisualStudioRobotsUrl = "https://visualstudio.microsoft.com/robots.txt";

		public static readonly string [] RobotsUrls = {
			MicrosoftRobotsUrl,
			XamarinRobotsUrl,
			BingRobotsUrl,
			XboxRobotsUrl,
			MSNRobotsUrl,
			VisualStudioRobotsUrl,
		};

		public static class Httpbin {
			public static readonly string Url = "https://httpbin.org";
			public static readonly Uri Uri = new Uri ("https://httpbin.org");
			public static readonly string DeleteUrl = "https://httpbin.org/delete";
			public static readonly string GetUrl = "https://httpbin.org/get";
			public static readonly string PatchUrl = "https://httpbin.org/patch";
			public static readonly string PostUrl = "https://httpbin.org/post";
			public static readonly string PutUrl = "https://httpbin.org/put";

			public static string GetAbsoluteRedirectUrl (int count) => $"https://httpbin.org/absolute-redirect/{count}";
			public static string GetRedirectUrl (int count) => $"https://httpbin.org/redirect/{count}";
			public static string GetRelativeRedirectUrl (int count) => $"http://httpbin.org/relative-redirect/{count}";

		}
	}
}
