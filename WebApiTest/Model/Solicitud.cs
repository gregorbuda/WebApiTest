using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebApiTest.Model
{
	public class Solicitud
	{
		public int Id { get; set; }
		public long Identificacion { get; set; }
		public string Nombre { get; set; }
		public string Apellido { get; set; }
		public int Edad { get; set; }
		public string Casa { get; set; }
	}
}
