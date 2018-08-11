using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.IO;
using System.Text;
using System.Web;

namespace AspNetVS2017.Capitulo02.Http.Testes
{
    [TestClass]
    public class HttpTeste
    {
        [TestMethod]
        public void RequestGetTeste()
        {
            /*             
             C - POST
             R - GET
             U - PUT
             D - DELETE
             */

            //var request = WebRequest.Create("http://www.example.com?usuarioId=42&cpf=12348684");
            var request = (HttpWebRequest)WebRequest.Create("http://www.example.com?nome=Ricardo&cpf=251");


            request.Method = "GET";          
            request.Date = DateTime.Now;
            request.UserAgent = "Visual Studio";    


            Console.WriteLine(GetRequestToString(request));

            Console.WriteLine(new string('-', 100));

            Console.WriteLine("Query string: " + request.RequestUri.Query);

            Console.WriteLine(new string('-', 100));

            var response = (HttpWebResponse)request.GetResponse();

            Console.WriteLine(GetResponseToString(response));
        }

        [TestMethod]
        public void RequestPostTeste()
        {
            var request = (HttpWebRequest)WebRequest.Create("https://httpbin.org/post");
            request.Method = "POST";

            var dados = "nome=Ricardo&cpf=2014&endereco=R.Cas";
            var bytes = new ASCIIEncoding().GetBytes(HttpUtility.HtmlEncode(dados));

            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = bytes.Length;
            request.GetRequestStream().Write(bytes, 0, bytes.Length);

            Console.WriteLine(GetRequestToString(request, dados));

            Console.WriteLine(new string('-', 100));

            var response = (HttpWebResponse)request.GetResponse();

            Console.WriteLine(GetResponseToString(response));
        }    



        private string GetResponseToString(HttpWebResponse response)
        {
            var statusLine = $"HTTP/{response.ProtocolVersion} {(int)response.StatusCode} {response.StatusDescription} {response.Server}";
      
            var reader = new StreamReader(response.GetResponseStream());

            return statusLine +
                Environment.NewLine +
                GetHeaders(response.Headers) +
                Environment.NewLine +
                reader.ReadToEnd();
        }

        private string GetRequestToString(HttpWebRequest request,  string dados = null)
        {
            var requestLine = $"{request.Method} {request.RequestUri} HTPP/{request.ProtocolVersion}";

            return requestLine +
                Environment.NewLine +
                GetHeaders(request.Headers)+
                Environment.NewLine +
                dados;


        }

        private string GetHeaders(WebHeaderCollection header)
        {
            var headers = "";

            foreach (var key in header.AllKeys)
            {
                headers += $"{key}: {header[key]}" + Environment.NewLine;
            }
         
            return headers;
        }
    }
}
