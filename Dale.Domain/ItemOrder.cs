using System.ComponentModel.DataAnnotations.Schema;

namespace Dale.Domain
{
    [Table("ITEMSORDER")]
    public class ItemOrder : BaseModel
    {
        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public Guid OrderId { get; set; }

        [ForeignKey("OrderId")]
        public Order Order { get; set; }

        public Guid ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
