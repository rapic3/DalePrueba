using Dale.Utils;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Dale.Orders.DTO
{
    public class ItemOrderRequestDto : _ClassBaseDto, IValidable
    {
        [Required(ErrorMessage = "El valor del producto es obligatorio.")]
        [Display(Name = "Valor unitario")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "La cantidad del producto es obligatorio.")]
        [Display(Name = "Cantidad")]
        [DataType(DataType.Text)]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "El producto es obligatorio.")]
        [Display(Name = "Producto")]
        public Guid ProductId { get; set; }

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
