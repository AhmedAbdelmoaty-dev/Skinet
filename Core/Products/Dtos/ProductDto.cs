namespace Application.Products.Dtos
{
    internal class ProductDto
    {
        public int Id { get; set; }
        public  string Name { get; set; }
        public  string Description { get; set; }
        public string Price { get; set; }
        public string Type { get; set; }
        public  string Brand { get; set; }
        public  int QuantityInStock { get; set; }
        public string PictureUrl { get; set; }
    }
}
