using MediatR;

namespace AplicacaoCQRS.WebAPI.Application.Commands
{
    public class ExcluiPessoaCommand : IRequest<string>
    {
        public int Id { get; set; }
    }
}