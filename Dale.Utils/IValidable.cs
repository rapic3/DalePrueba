using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Dale.Utils
{
    /// <summary>
    /// Contrato  IValidable para validaro los datos con dataanotation
    /// </summary>
    /// <author>
    /// Robert Pineda
    /// </author>
    /// <remarks>
    /// Creado 14/09/2022
    /// </remarks>
    public interface IValidable
    {
        [JsonIgnore]
        bool IsValid { get; }

        [JsonIgnore]
        List<ValidationResult> ErrorsValidation { get; set; }
    }
}
