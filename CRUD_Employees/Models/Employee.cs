using CRUD_Employees.Models.Enum;

namespace CRUD_Employees.Models

{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }   
        public string Image { get; set; } = "/images/default_image.jpg";

        public Gender Gender { get; set; }

        public int  BirthYear { get; set; }

        public DateOnly StartedWorking { get; set; }

        public ContractType ContractType { get; set; }

        public DateOnly ContractDue { get; set; }
        public Department Department { get; set; }
        public int? VacationDays { get; set; }
        public int? DaysOff { get; set; }
        public int? PaidLeaveDays { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public Employee()
        {

        }
    }
}
