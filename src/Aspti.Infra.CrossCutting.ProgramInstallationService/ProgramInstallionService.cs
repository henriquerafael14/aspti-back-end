using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Aspti.Infra.CrossCutting.ProgramInstallation
{
    public class ProgramInstallationService
    {
        public ProgramInstallationService(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public IWebDriver _webDriver;

        public void TerteWeb()
        {
            _webDriver.Navigate().GoToUrl("https://www.google.com/");
        }
    }
}
