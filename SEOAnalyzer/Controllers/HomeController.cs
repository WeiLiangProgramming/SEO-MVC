using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SEOAnalyzer.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System.Net;

namespace SEOAnalyzer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult SEO_main()
        {
            return View();
        }

        public IActionResult SEO_analyze(string userLink, string userText,
            string filter = "off", string occur = "off", string meta = "off", string ex_url = "off")
        {
            SEOModel model = new SEOModel();
            bool exception = false;
            if (!String.IsNullOrEmpty(userLink))
            {
                ViewData["userText"] = "<h2>URL: " + userLink + "</h2>";
                String text = "";
                try
                {
                    using (WebClient web1 = new WebClient())
                    {
                        string data = web1.DownloadString(userLink);
                        text += data;
                    }
                    model.SEOStr = text;
                    model.SEOType = "URL";
                }
                catch
                {
                    exception = true;
                }
            }
            else
            {
                ViewData["userText"] = "<h2>Original Text:</h2><p>" + userText + "</p>";
                model.SEOStr = userText;
                model.SEOType = "Text";
            }

            String functions = "";

            if (exception)
            {
                functions = "<h2>Exception occured: Please check the text or URL and try again.</h2>";
            }
            else
            {
                //Do filter stop-word
                if (filter.Equals("on"))
                {
                    model.fnExtract("stop-word");
                    if (model.SEOType.Equals("Text")) {
                        functions = "<hr><h4>Processed String:</h4><p>" + model.SEOStr + "</p>";
                    }
                    functions += "<h4>Stop-Word Count Result:</h4>" + model.stopFilterTable;
                }

                if (occur.Equals("on"))
                {
                    model.fnWordCount();
                    functions += "<hr><h4>Word Count Result:</h4>" + model.wordCountTable;
                }

                if (meta.Equals("on"))
                {
                    model.fnMetaKeywordCount();
                    if (model.metaCountRes.Count > 0)
                    {
                        functions += "<hr><h4>Meta Count Result:</h4>" + model.metaCountTable;
                    }
                    else
                    {
                        functions += "<hr><h4>No Meta tag keywords</h4>";
                    }
                }

                if (ex_url.Equals("on"))
                {
                    model.fnUrlCount();
                    functions += "<hr><h4>External URL Count Result:</h4>" +
                                    "<h4>URL Count = " + model.URLCount + "</h4>" +
                                    "<h4>URL Listing:</h4>";
                    if (model.URLList.Count > 0)
                    {
                        functions += "<ul>";
                        foreach (string url in model.URLList)
                        {
                            functions += "<li>" + url + "</li>";
                        }
                        functions += "</ul>";
                    }
                }
            }

            ViewData["userFunction"] = functions;
            ViewData["SEOObj"] = model;

            return View();
        }

        public String setNullToString(string str)
        {
            if(str == null)
            {
                return "";
            }
            else
            {
                return str;
            }
        }
    }
}
