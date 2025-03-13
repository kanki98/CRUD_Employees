using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CRUD_Employees.Data;
using CRUD_Employees.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using CRUD_Employees.Helpers;
using CRUD_Employees.Models.Enum;

namespace CRUD_Employees.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment; // To access wwwroot
        public EmployeesController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            return View(await _context.Employees.ToListAsync());
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            ViewBag.Genders = EnumHelper.GetEnumSelectList<Gender>();
            ViewBag.ContractTypes = EnumHelper.GetEnumSelectList<ContractType>();
            ViewBag.Departments = EnumHelper.GetEnumSelectList<Department>();

            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Employee employee, IFormFile? imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null)
                {
                    // Process the uploaded file
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(fileStream);
                    }

                    // Store the relative path in the database
                    employee.Image = "images/" + uniqueFileName;
                }

                if (employee.ContractType == ContractType.Permanent)
                {
                    employee.ContractDue = null;
                }

                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Genders = EnumHelper.GetEnumSelectList<Gender>();
            ViewBag.ContractTypes = EnumHelper.GetEnumSelectList<ContractType>();
            ViewBag.Departments = EnumHelper.GetEnumSelectList<Department>();

            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            ViewBag.Genders = EnumHelper.GetEnumSelectList<Gender>();
            ViewBag.ContractTypes = EnumHelper.GetEnumSelectList<ContractType>();
            ViewBag.Departments = EnumHelper.GetEnumSelectList<Department>();

            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Employee employee, IFormFile? imageFile)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingEmployee = await _context.Employees.FindAsync(id);
                    if (existingEmployee == null)
                    {
                        return NotFound();
                    }
                    // Retain the old image if new image isn't uploaded
                    if (imageFile != null && imageFile.Length > 0)
                    {
                        // Process the uploaded file
                        string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                        string uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        // Save the new image
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await imageFile.CopyToAsync(fileStream);
                        }

                        // Delete the old image if it exists from previous entry
                        if (!string.IsNullOrEmpty(existingEmployee.Image) && existingEmployee.Image != "images/default_image.jpg")
                        {
                            string oldFilePath = Path.Combine(_webHostEnvironment.WebRootPath, existingEmployee.Image);
                            if (System.IO.File.Exists(oldFilePath))
                            {
                                System.IO.File.Delete(oldFilePath);
                            }
                        }

                        // Store the new relative image path
                        existingEmployee.Image = "images/" + uniqueFileName;
                    }

                    if (employee.ContractType == ContractType.Permanent)
                    {
                        employee.ContractDue = null;
                    }

                    existingEmployee.FirstName = employee.FirstName;
                    existingEmployee.LastName = employee.LastName;
                    existingEmployee.Gender = employee.Gender;
                    existingEmployee.BirthYear = employee.BirthYear;
                    existingEmployee.StartedWorking = employee.StartedWorking;
                    existingEmployee.ContractType = employee.ContractType;
                    existingEmployee.ContractDue = employee.ContractDue;
                    existingEmployee.Department = employee.Department;
                    existingEmployee.VacationDays = employee.VacationDays;
                    existingEmployee.DaysOff = employee.DaysOff;
                    existingEmployee.PaidLeaveDays = employee.PaidLeaveDays;

                    _context.Update(existingEmployee);                
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Genders = EnumHelper.GetEnumSelectList<Gender>();
            ViewBag.ContractTypes = EnumHelper.GetEnumSelectList<ContractType>();
            ViewBag.Departments = EnumHelper.GetEnumSelectList<Department>();

            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            // Prevent deletion of the default image
            if (!string.IsNullOrEmpty(employee.Image) && employee.Image != "images/default_image.jpg")
            {
                var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, employee.Image);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
    }
}
