using System.ComponentModel.DataAnnotations.Schema;

namespace Dale.Domain
{
    [Table("ORDERS")]
    public class Order : BaseModel
    {

        [Column(TypeName = "decimal(18,2)")]
        public decimal Value { get; set; }

        public List<ItemOrder> Items { get; set; }

        public Guid ClientId { get; set; }

        [ForeignKey("ClientId")]
        public Client Client { get; set; }

        public string Factura { get; set; }


    }
}
