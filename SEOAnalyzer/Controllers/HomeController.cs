using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SEOAnalyzer.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace SEOAnalyzer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //public IActionResult SEO_main()
        //{
        //    return View();
        //}

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
            ViewData["userLink"] = userLink;
            ViewData["userText"] = userText;

            SEOModel model = new SEOModel();
            
            if(!String.IsNullOrEmpty(userText))
            {
                model.SEOStr = userText;
            }

            String functions = "";
            //Do filter stop-word
            if(filter.Equals("on"))
            {
                model.fnExtract("stop-word");
                functions = "<hr><h4>Processed String: " + model.SEOStr + "</h4>";
                functions += "<h4>Stop-Word Result:</h4>" + model.stopFilterTable;
            }

            if (occur.Equals("on"))
            {
                model.fnWordCount();
                functions += "<hr><h4>Stop-Word Result:</h4>" + model.wordCountTable;
            }

            if (meta.Equals("on"))
            {
                if (!functions.Equals(""))
                {
                    functions += " and ";
                }

                functions += "Calculate meta text occurances";
            }

            if (ex_url.Equals("on"))
            {
                if (!functions.Equals(""))
                {
                    functions += " and ";
                }

                functions += "Calculate external links";
            }

            functions += ".";

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
