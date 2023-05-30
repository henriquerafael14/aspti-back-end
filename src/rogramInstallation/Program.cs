using Aspti.Infra.CrossCutting.ProgramInstallation;
using OpenQA.Selenium.Chrome;

var web = new ProgramInstallationService(new ChromeDriver());
web.WhatsAppDownload();