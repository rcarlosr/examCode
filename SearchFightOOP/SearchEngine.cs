using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;

namespace SearchFightOOP
{
    public class SearchEngine
    {
        public ResultXMLElement resXMLElement;
        public RequestString reqString;
        private string _name = "";
        public string Name
        {
            get
            {
                return string.IsNullOrWhiteSpace(_name) ? string.Empty : _name;
            }
            set
            {
                _name = value;
            }
        }

        public long RetrieveResults(string queryText)
        {
            long Results = 0;
            HtmlDocument doc = HtmlDocReader.Read(reqString.CreateRequestString(queryText));
            HtmlElement element = null;
            HtmlElementCollection elementSet;

            if (doc != null)
            {
                if (resXMLElement.elemDivName != "")
                    element = doc.GetElementById(resXMLElement.elemDivName);

                if (element == null)
                {
                    elementSet = doc.GetElementsByTagName("Body");
                    if (elementSet != null)
                        element = elementSet[0];
                }

                if (element != null)
                {
                    element = searchInnerNodes(element, resXMLElement);
                    Results = ProcessOutput.ExtractOutput(element.InnerText, resXMLElement.elemMultiplier);    // The node contains the results
                    if(Results == -1)
                    {   
                        Console.WriteLine(Name + ": No results found in Html document.");
                        Results = 0;
                    }
                }
                else
                {
                    Console.WriteLine(Name + ": Html document does not have the required DIV neither a Body.");
                }
            }
            else
            {
                Console.WriteLine(Name + ": Html document did not load.");
            }
            return Results;
        }

        private HtmlElement searchInnerNodes(HtmlElement htmlElem, ResultXMLElement result)
        {
            if (htmlElem.InnerHtml != null)
            {
                if (htmlElem.InnerHtml.ToLower().Contains(result.elemHtmlKeyword))
                {
                    foreach (HtmlElement htmlChildElem in htmlElem.Children)
                    {
                        if (htmlChildElem.Children.Count > 0)
                            htmlElem = searchInnerNodes(htmlChildElem, result);
                        else
                        {
                            if (htmlChildElem.InnerText != null)
                                if (htmlChildElem.InnerText.ToLower().Contains(result.elemHtmlKeyword) && (result.elemTagName.Contains(htmlChildElem.TagName)))
                                    return htmlChildElem;
                        }

                        if (htmlElem != null)
                        {
                            if (htmlElem.Children.Count <= result.elemLevelsBelow && htmlElem.InnerText != null)
                                if (htmlElem.InnerText.ToLower().Contains(result.elemHtmlKeyword) && (result.elemTagName.Contains(htmlChildElem.TagName)))
                                    return htmlElem;
                        }
                    }
                }
            }
            return htmlElem;
        }
    }
}
