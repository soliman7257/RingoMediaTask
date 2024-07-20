
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RingoMediaTask.Data;
using RingoMediaTask.Models;
using System.Collections.Generic;

namespace RingoMediaTask.Services
{
    public class DepartmentService
    {
        private readonly ILogger<DepartmentService> _logger;
        private readonly ApplicationDbContext _dbContext;

        public DepartmentService(ILogger<DepartmentService> logger, ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _logger = logger;
        }


        public async Task<SaveResultWithSavedObject<IEnumerable<Department>>> GetSubDepartmentsAsync(int departmentId)
        {
            SaveResultWithSavedObject<IEnumerable<Department>> result = new SaveResultWithSavedObject<IEnumerable<Department>>(true);
            try
            {
               var SubDepartments = _dbContext.Departments.Where(d => d.ParentDepartmentId == departmentId);
                if (!SubDepartments.Any())
                {
                    result.IsSuccess = false;
                    result.Message = "No Sub Departments";
                }
                else
                {
                    result.SavedObject = await SubDepartments.ToListAsync();
                }


            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching Sub Departments");
                result.IsSuccess = false;
                result.Message = "Error occurred while fetching Sub Departments";

            }
            return result;
        }




        public async Task<SaveResultWithSavedObject<IEnumerable<DepartmentDto>>> GetParentDepartmentsAsync(int departmentId)
        {
            var result = new SaveResultWithSavedObject<IEnumerable<DepartmentDto>>(true);
            try
            {
                var department = await _dbContext.Departments
                    .Include(d => d.ParentDepartment)
                    .FirstOrDefaultAsync(d => d.Id == departmentId);

                if (department == null)
                {
                    result.IsSuccess = false;
                    result.Message = "Department not found";
                    return result;
                }

                var parents = new List<DepartmentDto>();
                var current = department;

                while (current != null)
                {
                    parents.Add(new DepartmentDto

                    {
                        Id = current.Id,
                        Name = current.Name,
                        Logo = current.Logo,
                        ParentDepartmentId = current.ParentDepartmentId
                    }
                        );
                   
                    if(current.ParentDepartmentId != null && current.ParentDepartment == null)
                    {
                        current = await _dbContext.Departments
                    .Include(d => d.ParentDepartment)
                    .FirstOrDefaultAsync(d => d.Id == current.ParentDepartmentId);
                    }
                    else
                        current = current.ParentDepartment;
                }

                parents.Reverse(); // To get the top-level department first

                result.SavedObject = parents;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching Parent Departments");
                result.IsSuccess = false;
                result.Message = "Error occurred while fetching Parent Departments";
            }

            return result;
        }


        

    }
}
