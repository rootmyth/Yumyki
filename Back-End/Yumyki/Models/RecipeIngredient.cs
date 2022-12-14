namespace Yumyki.Models
{
    public class RecipeIngredient
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }
        public decimal Quantity { get; set; }
        public string? QuantityUnit { get; set; }
        public string? Note { get; set; }
    }
}
