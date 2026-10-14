using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kiemtragiuaki.Models
{
    [Table("BIT240081_Dishes")]
    public class Dish_BIT240081
    {
        public const int QuickThresholdMinutes = 15;

        [Key]
        [Column("BIT240081_Id")]
        public int Id { get; set; }

        [Column("BIT240081_Name")]
        [Display(Name = "Field_Name")]
        [Required(ErrorMessage = "Val_Required")]
        [StringLength(150, ErrorMessage = "Val_MaxLength")]
        public string Name { get; set; } = string.Empty;

        [Column("BIT240081_Price", TypeName = "numeric(12,2)")]
        [Display(Name = "Field_Price")]
        [Required(ErrorMessage = "Val_Required")]
        [Range(0.01, 1_000_000_000, ErrorMessage = "Val_PricePositive")]
        public decimal Price { get; set; }

        [Column("BIT240081_PreparationTime")]
        [Display(Name = "Field_PreparationTime")]
        [Required(ErrorMessage = "Val_Required")]
        [Range(1, 10_000, ErrorMessage = "Val_PrepPositive")]
        public int PreparationTime { get; set; }

        [Column("BIT240081_IsAvailable")]
        [Display(Name = "Field_IsAvailable")]
        public bool IsAvailable { get; set; } = true;

        [Column("BIT240081_Description")]
        [Display(Name = "Field_Description")]
        [StringLength(1000, ErrorMessage = "Val_MaxLength")]
        public string? Description { get; set; }

        [Column("BIT240081_DishCategoryId")]
        [Display(Name = "Field_Category")]
        [Range(1, int.MaxValue, ErrorMessage = "Val_CategoryRequired")]
        public int DishCategoryId { get; set; }

        public DishCategory_BIT240081? DishCategory { get; set; }

        public ICollection<DishImage_BIT240081> Images { get; set; } = new List<DishImage_BIT240081>();

        // Computed at display time only; not mapped to a database column.
        [NotMapped]
        public bool IsQuick => PreparationTime <= QuickThresholdMinutes;
    }
}
