using System;

namespace ExternalOutput
{
	public class RequestExternalOutput
	{
		public static string Fetch(string webAddress, string toFolder, Parse.ParseableOutputTypes? type) {
			string fileFormat;
			if (type.HasValue) {
				switch (type.Value) {
					case Parse.ParseableOutputTypes.Gamess:
						fileFormat = ".gms";
						break;
					case Parse.ParseableOutputTypes.Xyz_Xmol:
						fileFormat = ".xmol";
						break;
					default:
						fileFormat = ".txt";
						break;
				}
			} else {
				fileFormat = webAddress.Substring(webAddress.LastIndexOf(".") + 1);
			}

			var localFilePath = System.IO.Path.Combine(
				                    toFolder,
				                    string.Format(
					                    "externaloutput_{0}." + fileFormat,
					                    System.DateTime.Now.ToString("yyyyMMddhhmmss")
				                    )
			                    );
			var client = new System.Net.WebClient();
			client.DownloadFile(webAddress, localFilePath);
			return localFilePath;
		}
	}
}

