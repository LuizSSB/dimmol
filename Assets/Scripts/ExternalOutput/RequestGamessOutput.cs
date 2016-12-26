using System;
using System.Net;
using System.IO;

namespace ExternalOutput
{
	public class RequestExternalOutput
	{
		static RequestExternalOutput() {
			var type = typeof(ServicePointManager);
			ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
		}

		public static string Fetch(
			string webAddress, string toFolder, Parse.ParseableOutputTypes? type,
			string proxyServer = null, int proxyPort = 0
		) {
			string fileFormat;
			if (type.HasValue) {
				switch (type.Value) {
					case Parse.ParseableOutputTypes.Gamess:
						fileFormat = "gms";
						break;
					case Parse.ParseableOutputTypes.Xyz_Xmol:
						fileFormat = "xmol";
						break;
					default:
						fileFormat = "txt";
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

			WebRequest request = WebRequest.Create(webAddress);
			request.Credentials = CredentialCache.DefaultCredentials;
			if (!string.IsNullOrEmpty(proxyServer)) {
				request.Proxy = new WebProxy(proxyServer, proxyPort);
			}

			using (var response = request.GetResponse())
			using (var dataStream = response.GetResponseStream())
			using (var sr = new StreamReader(dataStream))
			using (var sw = new StreamWriter(localFilePath))
			{
				string line;
				while ((line = sr.ReadLine()) != null) {
					sw.WriteLine(line);
				}
			}

			return localFilePath;
		}
	}
}

