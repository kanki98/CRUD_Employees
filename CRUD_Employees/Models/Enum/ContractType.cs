using System.ComponentModel.DataAnnotations;

namespace CRUD_Employees.Models.Enum
{
    public enum ContractType
    {
        [Display(Name = "Permanent Contract")]
        Permanent,

        [Display(Name = "Temporary Contract")]
        Temporary,

        [Display(Name = "Internship Program")]
        Internship,

        [Display(Name = "Freelance Work")]
        Freelance
    }
}
