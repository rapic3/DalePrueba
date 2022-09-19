namespace Dale.Utils.Enum
{
    public class DataTypeEnum
    {
        public enum DataType
        {
            [StringValue("1")]
            Entero,
            [StringValue("2")]
            String,
            [StringValue("3")]
            Compuesto,
            [StringValue("4")]
            Boleano,
            [StringValue("5")]
            Guid,
            [StringValue("6")]
            Moneda
        }

        public enum ValidationType
        {
            [StringValue("1")]
            SQL,
            [StringValue("2")]
            ExpresionRegular,
        }

        public enum ProfileType
        {
            [StringValue("1")]
            Administrador,
            [StringValue("2")]
            Usuario,
        }
    }
}
