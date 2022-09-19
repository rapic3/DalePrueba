using System.ComponentModel.DataAnnotations;

namespace Dale.Utils
{
    public interface IValidador
    {
        Tuple<bool, List<ValidationResult>> ValidObject(IValidable objToValidate);
    }
}
