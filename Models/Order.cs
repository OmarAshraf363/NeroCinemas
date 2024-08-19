using System.ComponentModel.DataAnnotations.Schema;

namespace Nero.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId {  get; set; }
        [ForeignKey("UserId")]
        public AppUser AppUser { get; set; }
        public DateTime? CreatedAt { get; set; }=DateTime.Now;
        public DateTime? UpdatedAt { get; set; } //updated when user complete ticket 
        public int Status { get; set; } = 0;//updated when user complete ticket 
        public int? Quantity {  get; set; }
        public string? PaymentStatus{ get; set; }
        public string? StripeChargeId {  get; set; }//to refund

        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();



    }
}
