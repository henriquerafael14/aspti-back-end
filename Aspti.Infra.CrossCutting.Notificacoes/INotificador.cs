using FluentValidation.Results;
using System.Collections.Generic;

namespace Aspti.Infra.CrossCutting.Notificacoes
{
	public interface INotificador
    {
        void AdicionarNotificacao(Notificacao notificacao);
        void AdicionarNotificacao(string notificacao);
        List<Notificacao> ObterNotificacoes();
        bool TemNotificacao();
        void AdicionarNotificacoes(List<ValidationFailure> erros);
        void AdicionarNotificacoes(IEnumerable<string> erros);
    }
}
