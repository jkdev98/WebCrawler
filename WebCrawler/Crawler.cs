using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;


class Crawler
{
   
    private static HashSet<string> _linksToVisitQueue = new HashSet<string>();
    private static HashSet<string> _linksToCheck = new HashSet<string>();
    private static HashSet<string> _alreadyChecked = new HashSet<string>(); 
    private static HashSet<string> _linksToCheckLater = new HashSet<string>(); 
    
    private static int lengthCount = 0;
    private static Boolean found = false;
    static void Main(string[] args)
    {
       
        SearchForArticle();
      
    }

    private static void SearchForArticle()
    {
        _linksToVisitQueue = GetLinksIntoHashSet("https://en.wikipedia.org/wiki/Special:Random");
        if (_linksToVisitQueue.Contains("/wiki/Donald_Trump"))
        {
            Console.WriteLine("Found Donald Trump!" + "\n" + "Length from the start: " + lengthCount + "\n" + "Found the article from random");
            return;
        }
            while (!found)
        {
            lengthCount++;
            for (int i = 0; i < _linksToVisitQueue.Count; i++)
            {
                IEnumerable<string> e = _linksToVisitQueue.AsEnumerable();
                string f = e.ElementAt(i);
                
                
                _linksToCheck = GetLinksIntoHashSet("https://en.wikipedia.org" + f);
                _linksToCheckLater.UnionWith(_linksToCheck);


                foreach (string s in _linksToCheck)
                {
                    _alreadyChecked.Add(s);
                    Console.WriteLine(s);
                    if (_linksToCheck.Contains("/wiki/Donald_Trump"))
                    {

                        found = true;
                        Console.WriteLine("Found Donald Trump!" + "\n" + "Length from the start: " + lengthCount + "\n" + "Last checked link: https://en.wikipedia.org" + f);
                        break;
                    }
                    
                }
                if(found)
                {
                    break;
                }
                _alreadyChecked.Add(f);


            }
            _linksToVisitQueue.Clear();
            _linksToVisitQueue.UnionWith(_linksToCheckLater);
            _linksToCheck.Clear();
            
        }
    }

    private static HashSet<string> GetLinksIntoHashSet(String url)
    {
        HashSet<string> returnList = new HashSet<string>();

        HtmlWeb hw = new HtmlWeb();
        HtmlDocument doc = hw.Load(url);
        foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//a[@href]"))
        {
            HtmlAttribute att = link.Attributes["href"];
            if (att.Value.StartsWith("/wiki/"))
            {
                   returnList.Add(att.Value);
                
            }

        }

        returnList.ExceptWith(_alreadyChecked);

        return returnList;
    }

}