using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeSiteAspNet.Models
{
    public class Img
    {
        [Key]
        public int ImgID { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
    }
}
