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
                var title = FindTitle(result, "<title>", "</title>");
                Console.WriteLine("TITLE: " + title);


                string startOfLink = "<a href=";
                string endOfLink = "</a>";
                var links = new List<string>();
                var urls = new List<string>();
                var displayTexts = new List<string>();

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

                    // find display text  => from > => to </a>
                    var indexE = linkSection.IndexOf(">") +1;
                    var indexF = indexE;
                    while (linkSection.Substring(indexF, endOfLink.Length) != endOfLink)
                        indexF++;
                    var displayText = linkSection.Substring(indexE, indexF - indexE);
                    displayTexts.Add(displayText);
                    
                    result = result.Remove(indexA, indexB - indexA);
                }

                for (int i = 0; i < links.Count; i++)
                {
                    Console.WriteLine($"{i}: {displayTexts[i]} ({urls[i]})");
                }

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

        static string FindTitle(string result, string startTerm, string endTerm)
        {
            var indexA = result.IndexOf(startTerm) + startTerm.Length;
            var indexB = result.LastIndexOf(endTerm);
            var title = result.Substring(indexA, indexB - indexA);
            return title;
        }
    }
}