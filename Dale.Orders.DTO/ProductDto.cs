using Dale.Utils;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Dale.Orders.DTO
{
    /// <summary>
    /// Clase para los productos
    /// </summary>
    /// <seealso cref="Dale.Domain.BaseModel" />
    /// <author>
    /// Robert Pineda
    /// </author>
    /// <remarks>
    /// 14/09/2022
    /// </remarks>
    [Table("PRODUCTS")]
    public partial class ProductDto : _ClassBaseDto, IValidable
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "El campo nombres es obligatorio.")]
        [Display(Name = "Nombres")]
        [StringLength(50)]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required(ErrorMessage = "El codigo del producto es obligatorio.")]
        [Display(Name = "Codigo de referencia")]
        [StringLength(50)]
        [DataType(DataType.Text)]
        public string ReferenceCode { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "El valor del producto es obligatorio.")]
        [Display(Name = "Valor")]
        public double Value { get; set; }

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
