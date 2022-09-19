using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dale.Domain
{
    /// <summary>
    /// Registro de los terceros que se pueden relacionar a sellercenter, seller y usuarios
    /// </summary>
    /// <seealso cref="Dale.Domain.BaseModel" />
    /// <author>
    /// Robert Pineda
    /// </author>
    /// <remarks>
    /// 14/09/2022
    /// </remarks>
    [Table("CLIENTS")]
    public partial class Client : BaseModel
    {
        /// <summary>
        /// Nombres: El nombre o razon social del tercero. Permite Nulo: NO. Longitud: 100.
        /// </summary>
        [Required]
        public string Nombres { get; set; }

        /// <summary>
        /// Apellidos: Apellido o sigla del tercero. Permite Nulo: SI. Longitud: 100.
        /// </summary>
        [Required]
        public string Apellidos { get; set; }

        /// <summary>
        /// NumeroIdentificacion: El numero de identificacion del tercero. Permite Nulo: NO. Longitud: 30.
        /// </summary>
        [Required]
        public string NumeroIdentificacion { get; set; }

        /// <summary>
        /// Direccion: La direccion del tercero. Permite Nulo: NO. Longitud: 100.
        /// </summary>
        public string Direccion { get; set; }

        /// <summary>
        /// Celular: Celular de contacto del tercero. Permite Nulo: SI. Longitud: 26.
        /// </summary>
        public string Celular { get; set; }

        /// <summary>
        /// Email: El correo electronico del tercero. Permite Nulo: NO. Longitud: 100.
        /// </summary>
        [Required]
        public string Email { get; set; }

    }
}
