    class Program
    {
        static void Main(string[] args)
        {

            //You must change the path to point to your .cer file location. 
            MyWebClient wb = new MyWebClient();
            wb.OldREader();
        }


        class MyWebClient : WebClient
        {
            protected override WebRequest GetWebRequest(Uri address)
            {
                HttpWebRequest request = base.GetWebRequest(address) as HttpWebRequest;
                request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
                return request;
            }

            public string ReadPage(string url)
            {
                string html = "";
                Uri ur  = new Uri("https://onet.pl");
                HttpWebRequest req = (HttpWebRequest) this.GetWebRequest(ur);

                using (HttpWebResponse response = (HttpWebResponse)req.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    html = reader.ReadToEnd();
                }

               
                return html;
            }


            public void OldREader()
            {
                HttpWebRequest Request = (HttpWebRequest)WebRequest.Create("https://onet.pl");
                Request.UserAgent = "Client Cert Sample";
                Request.Method = "GET";
                HttpWebResponse Response = (HttpWebResponse)Request.GetResponse();
            }
        }
}