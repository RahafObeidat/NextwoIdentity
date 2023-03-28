using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NextwoIdentity.Models
{
    public class Product
    {

        public int ProductId { get; set; }

        [Required(ErrorMessage = "Please Enter Prodyct Name")]
        [Display(Name = " Product Name")]
        public string? ProductName { get; set; }    

        public string? ProductDescription { get; set;}

        [ForeignKey("Category")]
        public int? CategoryId { get; set; }
        
        public Category? Category { get; set;}

    }
}
