using System.ComponentModel.DataAnnotations;

namespace MyBudgetApp.Models
{
    public class Expense
    {
        public int Id { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "The value must be greater than 0.")]
        public decimal Value { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string? Description { get; set; }
    }
}