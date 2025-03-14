using System.ComponentModel.DataAnnotations;

namespace CRUD_Employees.Models.Enum
{
    public enum Department
    {
        [Display(Name = "Human Resources")]
        HR,

        [Display(Name = "Information Technology")]
        IT,

        [Display(Name = "Finance & Accounting")]
        Finance,

        [Display(Name = "Sales & Marketing")]
        Sales,

        [Display(Name = "Marketing & Advertising")]
        Marketing,

        [Display(Name = "Executive Management")]
        Managment
    }
}
