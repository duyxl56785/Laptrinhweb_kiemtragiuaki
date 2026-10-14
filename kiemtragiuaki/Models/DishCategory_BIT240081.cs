using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kiemtragiuaki.Models
{
    [Table("BIT240081_DishCategories")]
    public class DishCategory_BIT240081
    {
        [Key]
        [Column("BIT240081_Id")]
        public int Id { get; set; }

        [Column("BIT240081_Name")]
        [Display(Name = "Field_Name")]
        [Required(ErrorMessage = "Val_Required")]
        [StringLength(100, ErrorMessage = "Val_MaxLength")]
        public string Name { get; set; } = string.Empty;

        [Column("BIT240081_Description")]
        [Display(Name = "Field_Description")]
        [StringLength(500, ErrorMessage = "Val_MaxLength")]
        public string? Description { get; set; }

        public ICollection<Dish_BIT240081> Dishes { get; set; } = new List<Dish_BIT240081>();
    }
}
