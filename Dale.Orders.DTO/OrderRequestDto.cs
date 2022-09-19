using Dale.Utils;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Dale.Orders.DTO
{
    public class OrderRequestDto : _ClassBaseDto, IValidable
    {
        [Required(ErrorMessage = "El valor de la orden es obligatorio.")]
        [Display(Name = "Valor")]
        [DataType(DataType.Currency)]
        public decimal Value { get; set; }

        [Required(ErrorMessage = "Los productos son obligatorios.")]
        [Display(Name = "Productos")]
        public List<ItemOrderRequestDto> Items { get; set; }

        [Required(ErrorMessage = "El cliente es obligatorio.")]
        [Display(Name = "Cliente")]
        public Guid ClientId { get; set; }

        [Required(ErrorMessage = "El número de la factura es obligatorio.")]
        [Display(Name = "Numero de factura")]
        [StringLength(10)]
        [DataType(DataType.Text)]
        public string Factura { get; set; }

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
