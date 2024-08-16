using System.ComponentModel.DataAnnotations.Schema;

namespace Nero.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId {  get; set; }
        [ForeignKey(nameof(OrderId))]
        public Order Order { get; set; }
        public int MovieId { get; set; }
        [ForeignKey(nameof(MovieId))]
        public Movie Movie { get; set; }
        public int Quantity {  get; set; }
        public decimal Price {  get; set; }
        public decimal TotalPrice {  get; set; }

    }
}
