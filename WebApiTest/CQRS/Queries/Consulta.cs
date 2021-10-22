using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using WebApiTest.Model;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace WebApiTest.CQRS.Queries
{
    public class Consulta
    {
        public class ListaSolicitud: IRequest<List<Solicitud>>
        {

        }

        public class Manejador : IRequestHandler<ListaSolicitud, List<Solicitud>>
        {
            private readonly SolicitudContext _context;
            public Manejador(SolicitudContext context)
            {
                _context = context;
            }
            public async Task<List<Solicitud>> Handle(ListaSolicitud request, CancellationToken cancellationToken)
            {
                var ListaSolicitud = await _context.solicitud.ToListAsync();

                return ListaSolicitud;
            }
        }
    }
}
