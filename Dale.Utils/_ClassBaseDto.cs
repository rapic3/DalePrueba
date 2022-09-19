using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Dale.Utils
{
    //// <summary>
    ///Clase para contenido comun entre las clases de dto
    /// <summary>
    /// Clase para 
    /// </summary>
    /// <seealso cref="Dale.Utils.IValidable" />
    /// <author>
    /// Robert Pineda
    /// </author>
    /// <remarks>
    /// 14/09/2022
    /// </remarks>
    public class _ClassBaseDto : IValidable
    {
        /// <summary> 
        /// id: Consecutivo del documento. Permite Nulo: NO. Longitud: 40. 
        /// </summary> 
        [JsonIgnore]
        public Guid Id { get; set; }

        /// <summary>
        /// Agrega el valor bit el estado InActivo del mongo db
        /// </summary>
        /// <param name="InActivo">el bool a llenar </param>
        public void SetInActivo(bool InActivo)
        {
            bool _ID;

            _ID = InActivo;

            this.InActivo = InActivo;
        }

        /// <summary>
        /// Retorna el estado InActivo
        /// </summary>
        /// <returns>el bool del InActivo</returns>
        public bool GetInactivo()
        {
            return this.InActivo;
        }

        /// <summary> 
        /// InActivo:estado de visualizacion  
        /// </summary> 
        [JsonIgnore]
        public bool InActivo { get; set; }

        [JsonIgnore]
        public DateTime CreatedDate { get; set; }

        #region Miembros de IValidable

        private List<ValidationResult> _validationErrors;

        [JsonIgnore]
        public bool IsValid
        {
            get
            {
                var validator = _Validador.Validate(this);
                _validationErrors = validator.Item2;
                return validator.Item1;
            }
        }

        [JsonIgnore]
        public List<ValidationResult> ErrorsValidation
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

        public List<string> GetErrors()
        {
            List<string> errors = new List<string>();

            foreach (var error in this.ErrorsValidation)
            {
                errors.Add(error.ErrorMessage ?? "");
            }

            return errors;
        }

        #endregion Miembros de IValidable
    }
}
