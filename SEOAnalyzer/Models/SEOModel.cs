using System;
using System.IO;
using System.Collections;

namespace SEOAnalyzer.Models
{
    public class SEOModel
    {
        public SEOModel()
        {
            initStopWords();
            stopFilterRes = new Hashtable();
            wordCountRes = new Hashtable();
        }
        public string SEOStr { get; set; }

        public Hashtable stopFilterRes { get; set; }

        public String stopFilterTable { get; set; }

        public Hashtable wordCountRes { get; set; }

        public String wordCountTable { get; set; }

        public string[] stopWords { get; set; }

        private void initStopWords()
        {
            stopWords = new string[] { "a", "about", "above", "above", "across", "after", "afterwards", "again", "against", "all", "almost", "alone", "along", "already", "also", "although", "always", "am", "among", "amongst", "amoungst", "amount", "an", "and", "another", "any", "anyhow", "anyone", "anything", "anyway", "anywhere", "are", "around", "as", "at", "back", "be", "became", "because", "become", "becomes", "becoming", "been", "before", "beforehand", "behind", "being", "below", "beside", "besides", "between", "beyond", "bill", "both", "bottom", "but", "by", "call", "can", "cannot", "cant", "co", "con", "could", "couldnt", "cry", "de", "describe", "detail", "do", "done", "down", "due", "during", "each", "eg", "eight", "either", "eleven", "else", "elsewhere", "empty", "enough", "etc", "even", "ever", "every", "everyone", "everything", "everywhere", "except", "few", "fifteen", "fify", "fill", "find", "fire", "first", "five", "for", "former", "formerly", "forty", "found", "four", "from", "front", "full", "further", "get", "give", "go", "had", "has", "hasnt", "have", "he", "hence", "her", "here", "hereafter", "hereby", "herein", "hereupon", "hers", "herself", "him", "himself", "his", "how", "however", "hundred", "ie", "if", "in", "inc", "indeed", "interest", "into", "is", "it", "its", "itself", "keep", "last", "latter", "latterly", "least", "less", "ltd", "made", "many", "may", "me", "meanwhile", "might", "mill", "mine", "more", "moreover", "most", "mostly", "move", "much", "must", "my", "myself", "name", "namely", "neither", "never", "nevertheless", "next", "nine", "no", "nobody", "none", "noone", "nor", "not", "nothing", "now", "nowhere", "of", "off", "often", "on", "once", "one", "only", "onto", "or", "other", "others", "otherwise", "our", "ours", "ourselves", "out", "over", "own", "part", "per", "perhaps", "please", "put", "rather", "re", "same", "see", "seem", "seemed", "seeming", "seems", "serious", "several", "she", "should", "show", "side", "since", "sincere", "six", "sixty", "so", "some", "somehow", "someone", "something", "sometime", "sometimes", "somewhere", "still", "such", "system", "take", "ten", "than", "that", "the", "their", "them", "themselves", "then", "thence", "there", "thereafter", "thereby", "therefore", "therein", "thereupon", "these", "they", "thick", "thin", "third", "this", "those", "though", "three", "through", "throughout", "thru", "thus", "to", "together", "too", "top", "toward", "towards", "twelve", "twenty", "two", "un", "under", "until", "up", "upon", "us", "very", "via", "was", "we", "well", "were", "what", "whatever", "when", "whence", "whenever", "where", "whereafter", "whereas", "whereby", "wherein", "whereupon", "wherever", "whether", "which", "while", "whither", "who", "whoever", "whole", "whom", "whose", "why", "will", "with", "within", "without", "would", "yet", "you", "your", "yours", "yourself", "yourselves", "the" };
        }

        public string trimAndDblSpc(string ori)
        {
            return ori.Replace("  ", " ").Trim();
        }

        private void fnSetWordCountTable()
        {
            Hashtable table = wordCountRes;
            string strTable = "";
            if (table.Count > 0)
            {
                int no = 1;
                strTable = "<table id=\"word-counter\" class=\"table table-striped table-bordered table-sm\" cellspacing=\"0\" width=\"100 % \">";
                strTable += "<thead>";
                strTable += "<tr>";
                strTable += "<th>No.</th>";
                strTable += "<th>Word</th>";
                strTable += "<th>Count</th>";
                strTable += "</tr>";
                strTable += "</thead>";
                strTable += "<tbody>";
                foreach (Object key in table.Keys)
                {
                    strTable += "<tr>";
                    strTable += "<td>" + no + "</td>";
                    strTable += "<td>" + key + "</td>";
                    strTable += "<td>" + table[key] + "</td>";
                    strTable += "</tr>";
                    no++;
                }
                strTable += "</tbody>";
                strTable += "</table>";
            }

            if(!string.IsNullOrEmpty(strTable))
            {
                wordCountTable = strTable;
            }
        }
        private void fnSetFilterTable()
        {
            Hashtable table = stopFilterRes;
            string strTable = "";
            if (table.Count > 0)
            {
                int no = 1;
                strTable = "<table id=\"stop-counter\" class=\"table table-striped table-bordered table-sm\" cellspacing=\"0\" width=\"100 % \">";
                strTable += "<thead>";
                strTable += "<tr>";
                strTable += "<th>No.</th>";
                strTable += "<th>Stop-Word</th>";
                strTable += "<th>Count</th>";
                strTable += "</tr>";
                strTable += "</thead>";
                strTable += "<tbody>";
                foreach (Object key in table.Keys)
                {
                    strTable += "<tr>";
                    strTable += "<td>" + no + "</td>";
                    strTable += "<td>" + key + "</td>";
                    strTable += "<td>" + table[key] + "</td>";
                    strTable += "</tr>";
                    no++;
                }
                strTable += "</tbody>";
                strTable += "</table>";
            }
            if (!string.IsNullOrEmpty(strTable))
            {
                stopFilterTable = strTable;
            }
        }

        //Task 1 - filter out the stop words
        public void fnExtract(string fnType)
        {
            if(fnType.Equals("stop-word"))
            {
                string[] arrTxt = SEOStr.Split(" ");
                string extracted = "";
                Boolean isStop;
                int count;
                foreach (string word in arrTxt)
                {
                    isStop = false;
                    foreach (string check in stopWords)
                    {
                        if(word.Replace(",", "").Replace(".", "").Equals(check))
                        {
                            isStop = true;
                            if(!stopFilterRes.ContainsKey(check))
                            {
                                stopFilterRes.Add(check, 1);
                            }
                            else
                            {
                                Console.WriteLine(stopFilterRes[check]);
                                count = ((int) stopFilterRes[check]) + 1;
                                stopFilterRes[check] = count;
                            }
                            break;
                        }
                    }
                    if(!isStop)
                    {
                        if(string.IsNullOrEmpty(extracted))
                        {
                            extracted = word;
                        }
                        else
                        {
                            extracted += " " + word;
                        }
                    }
                }
                this.SEOStr = extracted;
                fnSetFilterTable();
            }

        }

        //Task 2
        public void fnWordCount()
        {
            string[] arrTxt = SEOStr.Split(" ");
            int count;
            foreach (string word in arrTxt)
            {
                if (!wordCountRes.ContainsKey(word))
                {
                    wordCountRes.Add(word, 1);
                }
                else
                {
                    Console.WriteLine(wordCountRes[word]);
                    count = ((int) wordCountRes[word]) + 1;
                    wordCountRes[word] = count;
                }
            }
            fnSetWordCountTable();
        }

    }
}
