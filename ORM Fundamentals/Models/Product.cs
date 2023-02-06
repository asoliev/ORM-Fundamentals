using System.Text;

namespace ORM_Fundamentals.Models
{
    public class Product : IEquatable<Product>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Weight { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int Length { get; set; }

        public override string ToString()
        {
            var str = new StringBuilder();
            str.AppendLine($"ID: {Id};");
            str.AppendLine($"Name: {Name};");
            str.AppendLine($"Description: {Description};");
            str.AppendLine($"Weight: {Weight};");
            str.AppendLine($"Height: {Height};");
            str.AppendLine($"Width: {Width};");
            str.AppendLine($"Length: {Length};");
            return str.ToString();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Product);
        }

        public bool Equals(Product other)
        {
            return other != null &&
                   Name == other.Name &&
                   Description == other.Description &&
                   Weight == other.Weight &&
                   Height == other.Height &&
                   Width == other.Width &&
                   Length == other.Length;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Description, Weight, Height, Width, Length);
        }
    }
}