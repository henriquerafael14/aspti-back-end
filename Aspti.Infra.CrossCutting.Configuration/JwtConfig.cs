using System;

namespace Aspti.Infra.CrossCutting.Configuration
{
    public static class JwtConfig
    {
        private const string SECTION = "JWT";
        private const string SECRET = "Secret";
        private const string EXPIRACAOHORAS = "ExpiracaoHoras";

        public static string ObterChaveSecreta = Configuracoes.GetSectionValue(SECTION, SECRET);
        public static int TempoExpiracao = Configuracoes.GetSectionValueInt(SECTION, EXPIRACAOHORAS);
    }
}
