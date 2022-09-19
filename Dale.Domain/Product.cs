using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dale.Domain
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
    public partial class Product : BaseModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string ReferenceCode { get; set; }

        public string Description { get; set; }

        [Required]
        public double Value { get; set; }
    }
}
