using Dale.Utils;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Dale.Orders.DTO
{
    public class OrderDto : _ClassBaseDto, IValidable
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "El valor de la orden es obligatorio.")]
        [Display(Name = "Valor")]
        [StringLength(16)]
        [DataType(DataType.Currency)]
        public decimal Value { get; set; }

        public List<ItemOrderDto> Items { get; set; }

        public Guid ClientId { get; set; }

        public ClientDto Client { get; set; }

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
