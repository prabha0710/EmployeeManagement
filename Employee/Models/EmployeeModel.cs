using System.ComponentModel.DataAnnotations;

namespace Employee.Models
{
    public class EmployeeModel
    {
        public int EmpId { get; set; }
        public string EmpName { get; set; }
        [RegularExpression(@"^(\+91[\-\s]?)?[0]?(91)?[6-9]\d{9}$", ErrorMessage = "Invalid phone number.")]
        public string ContactNo { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string DepartmentName { get; set; }
        public string ProjectName { get; set; }
        [Required(ErrorMessage = "Please select a Date.")]
        [Display(Name = "Selected Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}" , ApplyFormatInEditMode = true)]
        public DateTime JoiningDate { get; set; }

    }
}
