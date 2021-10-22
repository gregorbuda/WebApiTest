using System;
using System.Net;

namespace Aplicacion.ManejadorError
{
    public class ManejadorException : Exception

    {
        private object httpStatusCode;

        public HttpStatusCode Codigo { get; }
        public object Errores { get; }
        public ManejadorException(HttpStatusCode codigo, object errores = null)
        {
            Codigo = codigo;
            Errores = errores;
        }

        public ManejadorException(object httpStatusCode)
        {
            this.httpStatusCode = httpStatusCode;
        }
    }
}