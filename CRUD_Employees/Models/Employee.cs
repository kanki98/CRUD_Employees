﻿using System.ComponentModel.DataAnnotations;
using CRUD_Employees.Models.Enum;

namespace CRUD_Employees.Models

{
    public class Employee
    {
        public int Id { get; set; }

        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Display(Name = "Profile Image")]
        public string Image { get; set; } = "/images/default_image.jpg";

        [Display(Name = "Gender")]
        public Gender Gender { get; set; }

        [Display(Name = "Birth year")]
        public int BirthYear { get; set; }

        [Display(Name = "Start date")]
        public DateOnly StartedWorking { get; set; }    

        [Display(Name = "Contract type")]
        public ContractType ContractType { get; set; }

        [Display(Name = "Contract due date")]
        public DateOnly? ContractDue { get; set; }

        [Display(Name = "Department")]
        public Department Department { get; set; }

        [Display(Name = "Vacation days")]
        [Range(0, int.MaxValue, ErrorMessage = "Vacation days cannot be negative.")]
        public int? VacationDays { get; set; }

        [Display(Name = "Days off")]
        [Range(0, int.MaxValue, ErrorMessage = "Days off cannot be negative.")]
        public int? DaysOff { get; set; }

        [Display(Name = "Paid leave days")]
        [Range(0, int.MaxValue, ErrorMessage = "Paid leave days cannot be negative.")]
        public int? PaidLeaveDays { get; set; }
        public string FullName => $"{FirstName} {LastName}";

    }
}
