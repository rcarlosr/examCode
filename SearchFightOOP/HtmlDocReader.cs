using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SearchFightOOP
{
    public class HtmlDocReader
    {
        public static HtmlDocument Read(string fullQuery)
        {
            WebBrowser browser = new WebBrowser();
            browser.AllowNavigation = true;
            browser.ScriptErrorsSuppressed = true;
            browser.Navigate(fullQuery);
            WaitLoad(browser);
            return browser.Document;
        }

        private static void WaitLoad(WebBrowser ctl)
        {
            WebBrowserReadyState loadStatus;
            int waitTime = 1000;
            int counter = 0;

            while (true)
            {
                loadStatus = ctl.ReadyState;
                Application.DoEvents();
                if ((counter > waitTime) || (loadStatus == WebBrowserReadyState.Uninitialized) || (loadStatus == WebBrowserReadyState.Loading) || (loadStatus == WebBrowserReadyState.Interactive))
                {
                    break;
                }
                counter++;
            }

            while (true)
            {
                loadStatus = ctl.ReadyState;
                Application.DoEvents();
                if ((loadStatus == WebBrowserReadyState.Complete && !ctl.IsBusy))
                {
                    break;
                }
            }
        }
    }
}
