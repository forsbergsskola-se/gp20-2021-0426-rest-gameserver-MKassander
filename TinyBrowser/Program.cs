using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;

namespace TinyBrowser
{
    class Program
    {
        static void Main()
        {
            try
            {
                TcpClient client = new TcpClient("acme.com", 80);

                var request = "GET / HTTP/1.1\r\nHost: acme.com\r\n\r\n";
                var convertedRequest = Encoding.ASCII.GetBytes(request);

                var stream = client.GetStream();
                stream.Write(convertedRequest, 0,convertedRequest.Length);
                Console.WriteLine("SENT");
                var streamReader = new StreamReader(stream);
                var result = streamReader.ReadToEnd();
                
                //Console.WriteLine("RESPONSE: " + result);
                FindTitle(result);
                

                string startOfLink = "<a href=";
                string endOfLink = "</a>";
                var links = new List<string>();
                var urls = new List<string>();

                while (result.Contains(startOfLink))
                {
                    var indexA = result.IndexOf(startOfLink);
                    var indexB = indexA;
                    while (result.Substring(indexB, endOfLink.Length) != endOfLink)
                        indexB++;

                    indexB += endOfLink.Length;

                    var linkSection = result.Substring(indexA, indexB - indexA);
                    links.Add(linkSection);

                    string quotation = "\"";
                    var indexC = linkSection.IndexOf(quotation) +1;
                    var indexD = indexC;
                    while (linkSection.Substring(indexD, 1) != quotation)
                        indexD++;

                    var url = linkSection.Substring(indexC, indexD - indexC);
                    urls.Add(url);

                    result = result.Remove(indexA, indexB - indexA);
                }

                foreach (var link in links)
                    Console.WriteLine(link);

                foreach (var url in urls)
                    Console.WriteLine(url);

                stream.Close();
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e);
            }
            catch (SocketException e)
            {
                Console.WriteLine(e);
            }
            
        }

        static void FindTitle(string result)
        {
            var indexA = result.IndexOf("<title>") + "<title>".Length;
            var indexB = result.LastIndexOf("</title>");
            var title = result.Substring(indexA, indexB - indexA);
            Console.WriteLine("TITLE: " + title);
        }
    }
}
//"acme.com", 80