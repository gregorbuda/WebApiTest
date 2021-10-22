using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApiTest.CQRS.Commands;
using WebApiTest.CQRS.Queries;
using WebApiTest.Model;
using Newtonsoft;

namespace WebApiTest.Controllers
{

	[ApiController]
	public class SolicitudController : MiControllerPadre
	{
		[HttpPost]
		public async Task<ActionResult<string>> Crear(Nuevo.Ejecuta data)
		{
			return await Mediator.Send(data);
		}


		[HttpGet]
		public async Task<ActionResult<List<Solicitud>>> Get()
		{
			return await Mediator.Send(new Consulta.ListaSolicitud());
		}

		[HttpPut("{id}")]
		public async Task<ActionResult<string>> Editar(int id, Edicion.Ejecutar data)
		{
			data.Id = id;
			return await Mediator.Send(data);
		}


		[HttpDelete("{id}")]
		public async Task<ActionResult<string>> Eliminar(int id)
		{

			return await Mediator.Send(new Eliminar.Ejecuta { Id = id });
		}

	}
}
