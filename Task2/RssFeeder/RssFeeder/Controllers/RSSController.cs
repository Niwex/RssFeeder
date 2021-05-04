using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RssFeeder.Models;



namespace RssFeeder.Controllers
{
    public class RSSController : Controller
    {
        public static string position;
        /*
        public IActionResult Index()
        {
            return View();
        }
        */
        //[HttpPost]
        public async Task<IActionResult> Index(string format, string RSSURL, string timeUpdate)
        {

            if (!string.IsNullOrEmpty(format))
            {
                position = format;
                ViewBag.FormatCheck = format;
                
            }
            WebClient wclient = new WebClient();
            XDocument xml;
            xml = XDocument.Load("settings.XML");
            string timer = (xml.Element("rsslinks").Element("update").Value);
            if (!string.IsNullOrEmpty(timeUpdate))
            {
                timer = timeUpdate;
                
            }
            
            ViewBag.Timer = timer;
            if (!string.IsNullOrEmpty(RSSURL))
            {
               string RSSdata = RSSURL;
               xml = XDocument.Load(RSSdata);
               var RSSFeedData = (from x in xml.Descendants("item")
                                   select new Item
                                   {
                                       Title = ((string)x.Element("title")),
                                       Link = ((string)x.Element("link")),
                                       Description = ((string)x.Element("description")),
                                       PubDate = DateTime.Parse(x.Element("pubDate").Value, CultureInfo.InvariantCulture),

                                   });
                ViewBag.Item = RSSFeedData;
                ViewBag.URL = RSSURL;
                return View();
            }
            

            foreach (XElement linkElement in xml.Element("rsslinks").Elements("newsfeed"))
            {
                XElement link = linkElement.Element("link");
                XDocument xlink = XDocument.Load(link.Value);
                var RSSFeedData = (from x in xlink.Descendants("item")
                                   select new Item
                                   {
                                       Title = ((string)x.Element("title")),
                                       Link = ((string)x.Element("link")),
                                       Description = ((string)x.Element("description")),
                                       PubDate = DateTime.Parse(x.Element("pubDate").Value, CultureInfo.InvariantCulture),

                                   });
                ViewBag.Item = RSSFeedData;
                ViewBag.URL = link.Value;
                
            }
            
            return View();
        }

        
    }   
}
