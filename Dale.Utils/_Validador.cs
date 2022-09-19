using System.ComponentModel.DataAnnotations;

namespace Dale.Utils
{
    public class _Validador : IValidador
    {
        private static _Validador validator = new _Validador();

        public static Tuple<bool, List<ValidationResult>> Validate(IValidable objToValidate)
        {
            if (validator == null)
                validator = new _Validador();

            return validator.ValidObject(objToValidate);
        }

        public Tuple<bool, List<ValidationResult>> ValidObject(IValidable objToValidate)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(objToValidate, null, null);
            var obj = Validator.TryValidateObject(objToValidate, context, results, true);

            return new Tuple<bool, List<ValidationResult>>(obj, results);
        }
    }
}
