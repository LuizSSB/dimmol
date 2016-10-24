using System;

namespace GamessOutput
{
	public class RequestGamessOutput
	{
		public static string Fetch(string webAddress, string toFolder) {
			var localFilePath = System.IO.Path.Combine(
				                    toFolder,
				                    string.Format(
					                    "gamessoutput_{0}.txt",
					                    System.DateTime.Now.ToString("yyyyMMddhhmmss")
				                    )
			                    );
			var client = new System.Net.WebClient();
			client.DownloadFile(webAddress, localFilePath);
			return localFilePath;
		}
	}
}

