using System.ComponentModel.DataAnnotations;

namespace BudgetManagement.Models
{
    public class AccountType
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The {0} field is required")]
        [StringLength(maximumLength: 50, MinimumLength = 3, ErrorMessage = "the length of the {0} field must be between {2} and {1} characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The {0} field is required")]
        [Display(Name = "Order Number")]
        public int OrderNumber { get; set; }

        public int UserId { get; set; }
    }
}
