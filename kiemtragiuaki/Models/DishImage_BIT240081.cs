using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kiemtragiuaki.Models
{
    [Table("BIT240081_DishImages")]
    public class DishImage_BIT240081
    {
        [Key]
        [Column("BIT240081_Id")]
        public int Id { get; set; }

        [Column("BIT240081_ImageUrl")]
        [Display(Name = "Field_ImageUrl")]
        [Required(ErrorMessage = "Val_Required")]
        [Url(ErrorMessage = "Val_Url")]
        [StringLength(1000, ErrorMessage = "Val_MaxLength")]
        public string ImageUrl { get; set; } = string.Empty;

        [Column("BIT240081_IsThumbnail")]
        [Display(Name = "Field_IsThumbnail")]
        public bool IsThumbnail { get; set; }

        [Column("BIT240081_DishId")]
        public int DishId { get; set; }

        public Dish_BIT240081? Dish { get; set; }
    }
}
