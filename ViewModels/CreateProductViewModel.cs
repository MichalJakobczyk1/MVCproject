namespace MVCproject.ViewModels
{
    public class CreateProductViewModel
    {
        public int Id { get; set; }
        public IFormFile Image { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }

        public decimal Price { get; set; }
        public decimal Volume { get; set; }
        public decimal? AlcVolume { get; set; }
    }
}
