using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Reflection;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using FluentValidation;
using MediatR;
using WebApiTest.Model;
using System.Threading;
using System.Text.RegularExpressions;

namespace WebApiTest.CQRS.Commands
{
	public class Nuevo
	{
		public class Ejecuta : IRequest<string>
		{
			public long Identificacion { get; set; }
			public string Nombre { get; set; }
			public string Apellido { get; set; }
			public int Edad { get; set; }
			public string Casa { get; set; }
		}



		public class EjecutaValidacion : AbstractValidator<Ejecuta>
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
					.InclusiveBetween(1,99);

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
		public class Manejador : IRequestHandler<Ejecuta, string>
		{
			private readonly SolicitudContext _context;
			public Manejador(SolicitudContext context)
			{
				_context = context;
			}
			public async Task<string> Handle(Ejecuta request, CancellationToken cancellationToken)
			{
				var solicitud = new Solicitud
				{
					Identificacion = request.Identificacion,
					Nombre = request.Nombre,
					Apellido = request.Apellido,
					Edad = request.Edad,
					Casa = request.Casa
				};
				_context.solicitud.Add(solicitud);

				var valor = await _context.SaveChangesAsync();

				if (valor > 0)
				{
					return "Solicitud agregada: " + solicitud.Id;
				}

				return (System.Net.HttpStatusCode.BadRequest + " No se insertó la solicitud");
			}
		}
	}
}
