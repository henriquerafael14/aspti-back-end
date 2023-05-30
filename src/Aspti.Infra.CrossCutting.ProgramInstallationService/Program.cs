using OpenQA.Selenium.Chrome;

namespace Aspti.Infra.CrossCutting.ProgramInstallation
{
    public class Program
    {
        public static void Main()
        {
            var web = new ProgramInstallationService(new ChromeDriver());
            web.TerteWeb();
        }
    }
}
