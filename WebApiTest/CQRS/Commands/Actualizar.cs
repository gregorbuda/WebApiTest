using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using WebApiTest.Model;
using System.Threading;
using System.Text.RegularExpressions;

namespace WebApiTest.CQRS.Commands
{
    public class Edicion
    {

        public class Ejecutar : IRequest<string>
        {
            
            public int Id { get; set; }
            public long Identificacion { get; set; }
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public int Edad { get; set; }
            public string Casa { get; set; }
        }



        public class EjecutaValidacion : AbstractValidator<Ejecutar>
        {
            public EjecutaValidacion()
            {
                RuleFor(x => x.Identificacion).NotNull()
                    .WithMessage("Debe ingresar Identificación")
                        .InclusiveBetween(1, 999999999);

                RuleFor(x => x.Nombre).NotEmpty().WithMessage("Debe ingresar el Nombre")
                    .Must(x => HasLetters(x)).WithMessage("El nombre debe tener solamente letras")
                    .Must(x => LengthString(x)).WithMessage("El nombre debe tener como maximo 20 caracteres");

                RuleFor(x => x.Apellido).NotEmpty().WithMessage("Debe ingresar el Apellido")
                .Must(x => HasLetters(x)).WithMessage("El apellido debe tener solamente letras")
                .Must(x => LengthString(x)).WithMessage("El apellido debe tener como maximo 20 caracteres");

                RuleFor(x => x.Edad).NotNull()
                    .WithMessage("Debe Ingresar Edad")
                    .InclusiveBetween(1, 99);

                RuleFor(x => x.Casa).NotEmpty().WithMessage("Debe ingresar la casa")
                    .Must(x => x.Equals("Gryffindor") || x.Equals("Hufflepuff") || x.Equals("Ravenclaw") || x.Equals("Slytherin")).WithMessage("Solo tiene las siguientes posibilidades: Gryffindor, Hufflepuff, Ravenclaw, Slytherin");
            }


            private bool HasLetters(string pw)
            {
                if (pw.All(c => Char.IsLetter(c)))
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }

            private bool LengthString(string pw)
            {
                if (pw.Length > 20)
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }


        }

        public class Manejador : IRequestHandler<Ejecutar, string>
        {
            private readonly SolicitudContext _context;
            public Manejador(SolicitudContext context)
            {
                _context = context;
            }
            public async Task<string> Handle(Ejecutar request, CancellationToken cancellationToken)
            {
                var solicitud = await _context.solicitud.FindAsync(request.Id);
                if (solicitud == null)
                {
                    return (System.Net.HttpStatusCode.NotFound + " No se Encontró la solicitud");
                }

                solicitud.Nombre = request.Nombre ?? solicitud.Nombre;
                solicitud.Apellido = request.Apellido ?? solicitud.Apellido;
                solicitud.Casa = request.Casa ?? solicitud.Casa;
                
                var resultado = await _context.SaveChangesAsync();

                if (resultado > 0)
                {
                   return "Solicitud Actualizada: " + solicitud.Id;
                }
                else
                {
                    return (System.Net.HttpStatusCode.BadRequest + " No se actualizó la solicitud");
                }
            }
        }

    }
}
