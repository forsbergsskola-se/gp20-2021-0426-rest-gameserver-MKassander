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
                
                Console.WriteLine("RESPONSE: " + result);

                var indexA = result.IndexOf("<title>") + "<title>".Length;
                var indexB = result.LastIndexOf("</title>");
                var title = result.Substring(indexA, indexB - indexA);
                Console.WriteLine("TITLE: " + title);

                string startOfLink = "<a href =";
                var links = new List<string>();
                while (result.Contains(startOfLink))
                {
                    indexA = result.IndexOf(startOfLink);
                    //fixa indexB
                    indexB = indexA;
                    for (int i = 1; i != 0;)
                    {
                        indexB++;
                        if (result[indexB].Equals("<") &&
                            result[indexB +1].Equals("/") &&
                            result[indexB +2].Equals("a"))
                        {
                            i--;
                        }
                    }

                    var link = result.Substring(indexA, indexB - indexA);
                    links.Add(link);
                    result.Remove(indexA, indexB - indexA);
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
    }
}
//"acme.com", 80