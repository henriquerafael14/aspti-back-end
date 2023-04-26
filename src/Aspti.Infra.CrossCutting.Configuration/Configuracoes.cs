using Microsoft.Extensions.Configuration;
using System.IO;

namespace Aspti.Infra.CrossCutting.Configuration
{
    public static partial class Configuracoes
    {
        private const string DEFAULT_SECTION_CONN_STRING = "ConnectionStrings";
        private const string DEFAULT_VALUE_CONN_STRING = "DefaultConnection";
        private const string TEMPO_SESSAO = "TempoDaSessao";
        private const string EMAIL = "EMAIL";
        public const string Usuario = "Usuario";
        public const string Senha = "Senha";
        private const string Host = "Host";
        private const string Porta = "Porta";
        private const string SslEmail = "Ssl";
        private const string HangFireUrl = "HangFireUrl";
        private const string CONFIG_FILE_NAME = "appsettings.json";
        private const string ResetSenhaUrl = "ResetSenhaUrl";

        private static IConfiguration _configuration;

        public static IConfiguration Conf
        {
            get
            {
                if (_configuration == null)
                    _configuration = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile(CONFIG_FILE_NAME)
                        .Build();

                return _configuration;
            }
        }

        public static string GetValueString(string value)
        {
            return Conf.GetValue<string>(value);
        }

        public static IConfigurationSection GetSection(string section)
        {
            return Conf.GetSection(section);
        }

        public static string GetSectionValue(string section, string value)
        {
            return Conf.GetSection(section).GetValue<string>(value);
        }

        public static string GetDefaultConnectionString()
        {
            return Conf.GetSection(DEFAULT_SECTION_CONN_STRING).GetValue<string>(DEFAULT_VALUE_CONN_STRING);
        }

        public static int ObterTempoSessao()
        {
            return Conf.GetValue<int>(TEMPO_SESSAO);
        }

        public static string ObterHangFireUrl()
        {
            return Conf.GetValue<string>(HangFireUrl);
        }

        public static string ObterUsuarioEmail()
        {
            return Conf.GetSection(EMAIL).GetValue<string>(Usuario);
        }

        public static string ObterSenhaEmail()
        {
            return Conf.GetSection(EMAIL).GetValue<string>(Senha);
        }

        public static string ObterHostEmail()
        {
            return Conf.GetSection(EMAIL).GetValue<string>(Host);
        }

        public static int ObterPortaSMTP()
        {
            return Conf.GetSection(EMAIL).GetValue<int>(Porta);
        }

        public static bool ObterSslEmail()
        {
            return Conf.GetSection(EMAIL).GetValue<bool>(SslEmail);
        }

        public static int GetSectionValueInt(string section, string value)
        {
            return Conf.GetSection(section).GetValue<int>(value);
        }

        public static string ObterUrlResetSenha()
        {
            return Conf.GetValue<string>(ResetSenhaUrl);
        }
    }
}
