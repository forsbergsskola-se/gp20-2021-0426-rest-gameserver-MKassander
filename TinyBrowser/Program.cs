﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace TinyBrowser
{
    class Program
    {
        static void Main()
        {
            var acme = "acme.com";
            var hostName = acme;
            
            
            
            while (true)
            {
                try
                {
                    var request = "GET / HTTP/1.1\r\nHost: " + hostName + "\r\n\r\n";
                    TcpClient client = new TcpClient("acme.com", 80);
                    var convertedRequest = Encoding.ASCII.GetBytes(request);

                    var stream = client.GetStream();
                    stream.Write(convertedRequest, 0,convertedRequest.Length);
                    Console.WriteLine("Request: " + request);
                    var streamReader = new StreamReader(stream);
                    var result = streamReader.ReadToEnd();
                
                    var title = FindTitle(result, "<title>", "</title>");
                    Console.WriteLine("TITLE: " + title);
                
                    string startOfLink = "<a href=";
                    string endOfLink = "</a>";
                    var links = new List<string>();
                    var urls = new List<string>();
                    var displayTexts = new List<string>();

                    SeparateDesiredStrings(result, startOfLink, endOfLink, links,urls, displayTexts);

                    PrintLists(links,displayTexts,urls);

                    Console.WriteLine("Enter a number corresponding to the page you want to visit:");
                    var input= Console.ReadLine();
                
                    if (int.TryParse(input, out int parsedInput) && 
                        parsedInput >= 0 && parsedInput <= links.Count)
                    {
                        hostName = acme + "/" + urls[parsedInput]; // behövs ändras om man trycker fler ggr(?)
                    }else Console.WriteLine("Invalid input!");

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

        static string FindTitle(string result, string startTerm, string endTerm)
        {
            var indexA = result.IndexOf(startTerm) + startTerm.Length;
            var indexB = result.LastIndexOf(endTerm);
            var title = result.Substring(indexA, indexB - indexA);
            return title;
        }

        static void SeparateDesiredStrings(string res, string startOf, string endOf, 
            List<string> linksList, List<string> urlList, List<string> displayList)
        {
            while (res.Contains(startOf))
            {
                var indexA = res.IndexOf(startOf);
                var indexB = indexA;
                while (res.Substring(indexB, endOf.Length) != endOf)
                    indexB++;
                indexB += endOf.Length;
                var linkSection = res.Substring(indexA, indexB - indexA);
                linksList.Add(linkSection);

                string quotation = "\"";
                var indexC = linkSection.IndexOf(quotation) +1;
                var indexD = indexC;
                while (linkSection.Substring(indexD, 1) != quotation)
                    indexD++;

                var url = linkSection.Substring(indexC, indexD - indexC);
                urlList.Add(url);

                var indexE = linkSection.IndexOf(">") +1;
                var indexF = indexE;
                while (linkSection.Substring(indexF, endOf.Length) != endOf)
                    indexF++;
                var displayText = linkSection.Substring(indexE, indexF - indexE);
                displayList.Add(displayText);
                    
                res = res.Remove(indexA, indexB - indexA);
            }
        }
        
        static void PrintLists(List<string> linksList, List<string> displayTextList, List<string> urlList)
        {
            for (int i = 0; i < linksList.Count; i++)
            {
                if (displayTextList[i].StartsWith("<img"))
                {
                    Console.WriteLine($"{i}: Image ()");
                    urlList[i] = "";
                }else Console.WriteLine($"{i}: {displayTextList[i]} ({urlList[i]})");
            }
        }
    }
}