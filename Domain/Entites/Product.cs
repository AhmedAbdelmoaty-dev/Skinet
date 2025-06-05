namespace Domain.Entites
{
    public class Product:BaseEntity
    {
        public required string Name {  get; set; }
        public required string Description { get; set; }
        public decimal Price { get; set; }
        public required string Type { get; set; }
        public required string Brand { get; set; }
        public required int QuantityInStock { get; set; }
        public string PictureUrl { get; set; }
    }
}
