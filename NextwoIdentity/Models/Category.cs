using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace NextwoIdentity.Models
{
    public class Category
    {
       
        public int CategoryId { get; set; }
       
        public string? CategoryName { get; set; }    

        public string? CategoryDescription { get; set;}
  


    }
}
