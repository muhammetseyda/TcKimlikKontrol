using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TcKimlikKontrol.Services
{
    public class NviService
    {
        private readonly HttpClient _httpClient;

        public NviService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> DogrulaAsync(string tcKimlikNo, string ad, string soyad, DateTime dogumTarihi)
        {
            try
            {
                long formattedtcKimlikNo =  Convert.ToInt64(tcKimlikNo);
                int formattedYear = dogumTarihi.Year;

                string url = "https://tckimlik.nvi.gov.tr/Service/KPSPublic.asmx";
                string soapEnvelope =
                    $@"<soap12:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap12=""http://www.w3.org/2003/05/soap-envelope"">
                        <soap12:Body>
                            <TCKimlikNoDogrula xmlns=""http://tckimlik.nvi.gov.tr/WS"">
                                <TCKimlikNo>{formattedtcKimlikNo}</TCKimlikNo>
                                <Ad>{ad}</Ad>
                                <Soyad>{soyad}</Soyad>
                                <DogumYili>{formattedYear}</DogumYili>
                            </TCKimlikNoDogrula>
                        </soap12:Body>
                    </soap12:Envelope>";

                var content = new StringContent(soapEnvelope, Encoding.UTF8, "application/soap+xml");

                using var response = await _httpClient.PostAsync(url, content);
                if (!response.IsSuccessStatusCode)
                    return false;

                var responseContent = await response.Content.ReadAsStringAsync();
                XDocument xDocument = XDocument.Parse(responseContent);

                XNamespace ns = "http://tckimlik.nvi.gov.tr/WS";
                XName resultElementName = XName.Get("TCKimlikNoDogrulaResult", ns.NamespaceName);
                bool.TryParse(xDocument.Descendants(resultElementName).FirstOrDefault()?.Value, out bool result);

                return result;
            }
            catch
            {
                return false;
            }
        }
    }
}
