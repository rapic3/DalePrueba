using Dale.Utils;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Dale.Environment.DTO
{
    /// <summary>
    /// Registro de los terceros que se pueden relacionar a clientes, empleados, usuarios
    /// </summary>
    public partial class ClientDto : _ClassBaseDto, IValidable
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Nombres: El nombre o razon social del tercero. Permite Nulo: NO. Longitud: 100.
        /// </summary>
        [Required(ErrorMessage = "El campo nombres o razon social es obligatorio.")]
        [Display(Name = "Nombres/Razon Social")]
        [StringLength(50)]
        [DataType(DataType.Text)]
        public string Nombres { get; set; }

        /// <summary>
        /// Apellidos: Apellido o sigla del tercero. Permite Nulo: SI. Longitud: 100.
        /// </summary>
        [Required(ErrorMessage = "El campo apellidos o sigla es obligatorio.")]
        [Display(Name = "Apellidos/Sigla")]
        [StringLength(50)]
        [DataType(DataType.Text)]
        public string Apellidos { get; set; }

        /// <summary>
        /// NumeroIdentificacion: El numero de identificacion del tercero. Permite Nulo: NO. Longitud: 30.
        /// </summary>
        [Required(ErrorMessage = "El campo identificación es obligatorio.")]
        [Display(Name = "Identificacion")]
        [StringLength(20)]
        [DataType(DataType.Text)]
        public string NumeroIdentificacion { get; set; }

        /// <summary>
        /// Direccion: La direccion del tercero. Permite Nulo: NO. Longitud: 100.
        /// </summary>
        [Required(ErrorMessage = "El campo dirección es obligatorio.")]
        [Display(Name = "Dirección")]
        [StringLength(50)]
        [DataType(DataType.Text)]
        public string Direccion { get; set; }

        /// <summary>
        /// Celular: Celular de contacto del tercero. Permite Nulo: SI. Longitud: 26.
        /// </summary>
        [Required(ErrorMessage = "El campo celular es obligatorio.")]
        [Display(Name = "Celular")]
        [StringLength(11)]
        [DataType(DataType.Text)]
        public string Celular { get; set; }

        /// <summary>
        /// Email: El correo electronico del tercero. Permite Nulo: NO. Longitud: 100.
        /// </summary>
        [Required(ErrorMessage = "El campo correo electronico es obligatorio.")]
        [Display(Name = "Correo electronico")]
        [StringLength(100)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string NombreCompleto
        {
            get
            {
                return Nombres + " " + Apellidos;
            }
            set
            {
                NombreCompleto = value;
            }
        }

        #region Miembros de IValidable

        private List<ValidationResult> _validationErrors;

        [JsonIgnore]
        public bool EsValido
        {
            get
            {
                var validator = _Validador.Validate(this);
                _validationErrors = validator.Item2;
                return validator.Item1;
            }
        }

        [JsonIgnore]
        public List<ValidationResult> ErroresValidacion
        {
            get
            {
                return _validationErrors;
            }
            set
            {
                _validationErrors = value;
            }
        }

        public List<string> GetErrores()
        {
            List<string> errores = new List<string>();

            foreach (var error in this.ErroresValidacion)
            {
                errores.Add(error.ErrorMessage);
            }

            return errores;
        }

        #endregion Miembros de IValidable
    }
}
