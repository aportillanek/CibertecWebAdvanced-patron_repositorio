using System.ComponentModel.DataAnnotations;

namespace Cibertec.Models
{
    public class Customer
    {        
        public int Id { get; set; }   
        [Display(Name="First Name")]
        [Required(ErrorMessage ="This field is required")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "This field is required")]
        public string LastName { get; set; }
        public string City { get; set; }
        public string Country{ get; set; }
        public string Phone { get; set; }        
    }
}
