using System.ComponentModel.DataAnnotations;

namespace CRUD_Employees.Models.Enum
{
    public enum Gender
    {
        [Display(Name = "Male")]
        Male,

        [Display(Name = "Female")]
        Female,

        [Display(Name = "Other / Prefer not to say")]
        Other
    }
}
