using OpenQA.Selenium;

namespace Aspti.Infra.CrossCutting.ProgramInstallation
{
    public class ProgramInstallationService
    {
        public ProgramInstallationService(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public IWebDriver _webDriver;

        public void TeamViewerDownload()
        {
            // Navegar para o site de download do TeamViewer
            _webDriver.Navigate().GoToUrl("https://www.teamviewer.com/pt-br/download/windows/");

            try
            {
                // Localizar o botão de download e clicar nele
                var downloadButton = _webDriver.FindElement(By.PartialLinkText("href=\"https://download.teamviewer.com/download/TeamViewer_Setup_x64.exe\""));
                downloadButton.Click();

                // Aguardar o tempo necessário para o download e instalação do programa
                Thread.Sleep(TimeSpan.FromSeconds(10)); // Espera de 10 segundos para o download
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            // Fechar o driver do WebDriver
            _webDriver.Quit();
            

        }

        public void WhatsAppDownload() 
        {
            // Navegar para o site de download do WhatsApp
            _webDriver.Navigate().GoToUrl("https://www.whatsapp.com/download?lang=pt_BR");

            try
            {
                // Localizar o botão de download e clicar nele
                var downloadButton = _webDriver.FindElement(By.CssSelector("img[alt='Baixar da Microsoft Store']"));
                downloadButton.Click();

                // Aguardar o tempo necessário para o download e instalação do programa
                Thread.Sleep(TimeSpan.FromSeconds(10)); // Espera de 10 segundos para o download
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            // Fechar o driver do WebDriver
            _webDriver.Quit();
        }
    }
}
