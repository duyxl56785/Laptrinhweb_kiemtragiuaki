namespace kiemtragiuaki.Models
{
    public class DishFilterViewModel
    {
        public string? Search { get; set; }
        public int? CategoryId { get; set; }
        // "" (all) | "available" | "unavailable"
        public string? Status { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        // "" | "price_asc" | "price_desc" | "prep_asc"
        public string? Sort { get; set; }

        // Overall stats shown in the page header (not affected by filters).
        public int TotalDishes { get; set; }
        public int AvailableDishes { get; set; }

        public List<string> Errors { get; } = new();
        public List<DishCategory_BIT240081> Categories { get; set; } = new();
        public List<Dish_BIT240081> Dishes { get; set; } = new();
    }
}
