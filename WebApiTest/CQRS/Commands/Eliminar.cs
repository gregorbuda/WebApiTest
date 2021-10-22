using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using WebApiTest.Model;
using System.Threading;

namespace WebApiTest.CQRS.Commands
{
	public class Eliminar
	{
        public class Ejecuta : IRequest<string>
        {
            public int Id { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta, string>
        {
            private readonly SolicitudContext _context;
            public Manejador(SolicitudContext context)
            {
                _context = context;
            }
            public async Task<string> Handle(Ejecuta request, CancellationToken cancellationToken)
            {

                var solicitud = await _context.solicitud.FindAsync(request.Id);

                if (solicitud == null)
                {
                    return (System.Net.HttpStatusCode.BadRequest + " No se Encontró la solicitud" );
                }

                _context.Remove(solicitud);

                var resultado = await _context.SaveChangesAsync();

                if (resultado > 0)
                {
                    return "Solicitud eliminada: " + solicitud.Id;
                }

                return (System.Net.HttpStatusCode.BadRequest + " No se elimninó la solicitud");
            }
        }
    }
}
