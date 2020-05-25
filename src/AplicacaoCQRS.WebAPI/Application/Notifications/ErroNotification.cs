using MediatR;
using System;

namespace AplicacaoCQRS.WebAPI.Application.Notifications
{
    public class ErroNotification : INotification
    {
        public Exception Excecao { get; set; }
        public string PilhaErro { get; set; }
    }
}