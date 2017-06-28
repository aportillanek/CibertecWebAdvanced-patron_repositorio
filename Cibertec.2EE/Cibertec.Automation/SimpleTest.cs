using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cibertec.Automation
{
    public class SimpleTest
    {
        public static void Navigate()
        {

            var drive = new ChromeDriver();
            drive.Navigate().GoToUrl("http://localhost:5000");
            drive.Close();
        }
    }
}
