using System.Text;

namespace ORM_Fundamentals.Models
{
    public class Order : IEquatable<Order>
    {
        public int Id { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int ProductId { get; set; }

        public override string ToString()
        {
            var str = new StringBuilder();
            str.AppendLine($"ID: {Id};");
            str.AppendLine($"Status: {Status};");
            str.AppendLine($"CreatedDate: {CreatedDate};");
            str.AppendLine($"UpdatedDate: {UpdatedDate};");
            str.AppendLine($"ProductId: {ProductId};");
            return str.ToString();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Order);
        }

        public bool Equals(Order other)
        {
            return other != null &&
                   Status == other.Status &&
                   CreatedDate.ToShortDateString() == other.CreatedDate.ToShortDateString() &&
                   UpdatedDate.ToShortDateString() == other.UpdatedDate.ToShortDateString() &&
                   ProductId == other.ProductId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(
                Status,
                CreatedDate.ToShortDateString(),
                UpdatedDate.ToShortDateString(),
                ProductId
            );
        }
    }
}