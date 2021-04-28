using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

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

                Byte[] data = new byte[10000];
                var response = stream.Read(data, 0, data.Length);
                var convertedResponse = Encoding.ASCII.GetString(data, 0, response);
                
                Console.WriteLine("RESPONSE: " + convertedResponse);

                var indexA = convertedResponse.IndexOf("<title>") + "<title>".Length;
                Console.WriteLine("indexA: " + indexA);
                var indexB = convertedResponse.LastIndexOf("</title>");
                Console.WriteLine("indexB: " + indexB);
                var title = convertedResponse.Substring(indexA, indexB - indexA);
                Console.WriteLine("TITLE: " + title);
                
                

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