﻿using AplicacaoCQRS.WebAPI.Application.Commands;
using AplicacaoCQRS.WebAPI.Application.Models;
using AplicacaoCQRS.WebAPI.Application.Notifications;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AplicacaoCQRS.WebAPI.Application.EventHandlers
{
    public class ExcluiPessoaCommandHandler : IRequestHandler<ExcluiPessoaCommand, string>
    {
        private readonly IMediator _mediator;
        private readonly IRepository<Pessoa> _repository;
        public ExcluiPessoaCommandHandler(IMediator mediator, IRepository<Pessoa> repository)
        {
            this._mediator = mediator;
            this._repository = repository;
        }

        public async Task<string> Handle(ExcluiPessoaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _repository.Delete(request.Id);

                await _mediator.Publish(new PessoaExcluidaNotification { Id = request.Id, IsEfetivado = true });

                return await Task.FromResult("Pessoa excluída com sucesso");
            }
            catch (Exception ex)
            {
                await _mediator.Publish(new PessoaExcluidaNotification { Id = request.Id, IsEfetivado = false });
                await _mediator.Publish(new ErroNotification { Excecao = ex, PilhaErro = ex.StackTrace });
                return await Task.FromResult("Ocorreu um erro no momento da exclusão");
            }
        }
    }
}