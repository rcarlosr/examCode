using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SearchFightOOP
{
    public class RequestString
    {
        private string _url;
        private string _searchPrefix;
        private string _defaultParameters;
        private string _symbolSpace;

        public string URL
        {
            get
            {
                return string.IsNullOrWhiteSpace(_url) ? string.Empty : _url;
            }
            set
            {
                _url = value;
            }
        }

        public string SearchPrefix
        {
            get
            {
                return string.IsNullOrWhiteSpace(_searchPrefix) ? string.Empty : _searchPrefix;
            }
            set
            {
                _searchPrefix = value;
            }
        }

        public string DefaultParameters
        {
            get
            {
                return string.IsNullOrWhiteSpace(_defaultParameters) ? string.Empty : _defaultParameters;
            }
            set
            {
                _defaultParameters = value;
            }
        }

        public string SymbolSpace
        {
            get
            {
                return string.IsNullOrWhiteSpace(_symbolSpace) ? string.Empty : _symbolSpace;
            }
            set
            {
                _symbolSpace = value;
            }
        }


        public string CreateRequestString(string queryText)
        {
            string urlQuery = string.Empty;
            queryText = queryText.Replace(" ", SymbolSpace);
            urlQuery = URL + SearchPrefix + queryText + DefaultParameters;
            return urlQuery;
        }

        public static bool URIExists(string uri)
        {
            try
            {
                HttpWebRequest request = WebRequest.Create(uri) as HttpWebRequest;
                request.Method = "HEAD";
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                response.Close();
                return (response.StatusCode == HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                Console.WriteLine(uri + "   " + ex.Message);
                return false;
            }
        }
    }
}
