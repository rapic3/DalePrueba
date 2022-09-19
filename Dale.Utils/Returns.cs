using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static Dale.Utils.Enum.GeneralEnum;

namespace Dale.Utils
{
    /// <summary>
    /// Clase Respuestas para los retornos del Negocio y los controladores
    /// </summary>
    /// <typeparam name="T">Entidad</typeparam>
    /// <author>
    /// Robert Pineda
    /// </author>
    public class Returns<T>
    {
        /// <summary>
        /// Estado del proceso
        /// </summary>
        /// <value>
        ///   <c>true</c> if el proceso es exitoso; otherwise, <c>false</c>.
        /// </value>
        public bool State { get; set; } = false;

        /// <summary>
        /// La lista de mensajes a enviar como respuesta
        /// </summary>
        /// <value>
        /// Los mensajes.
        /// </value>
        public List<string> Message { get; set; }

        /// <summary>
        /// Donde se retorna la informacion que se requiere del proceso
        /// </summary>
        /// <value>
        /// La informacion.
        /// </value>
        public T Information { get; set; }

        /// <summary>
        /// El tipo de respuesta del servicio
        /// </summary>
        /// <value>
        /// El tipo (Error, OK, Advertencia)
        /// </value>
        public EnumTypeReturn Type { get; set; } = EnumTypeReturn.OK;

        /// <summary>
        /// El codigo estado http.
        /// </summary>
        /// <value>
        /// El codigo estado.
        /// </value>
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// La fecha de la respuesta.
        /// </summary>
        /// <value>
        /// La fecha.
        /// </value>
        public DateTime Date { get; set; } = DateTime.Now;

        public string GetMessage()
        {
            string mensaje = JsonConvert.SerializeObject(Message);
            return mensaje;
        }
    }
}
