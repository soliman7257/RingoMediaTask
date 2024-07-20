using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RingoMediaTask.Data;
using RingoMediaTask.Services;

namespace RingoMediaTask.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly DepartmentService _departmentService;

        public DepartmentController(DepartmentService departmentService)
        {
            _departmentService = departmentService;
        }
   

        [HttpGet]
        [Route("GetSubDepartments")]
        public async Task<ActionResult> GetSubDepartmentsAsync(int departmentId)
        {
           var result = await _departmentService.GetSubDepartmentsAsync(departmentId);
            if(result.IsSuccess == true)
                return Ok(result.SavedObject);
            else
                return NotFound(result.Message);
        }

        [HttpGet]
        [Route("GetParentDepartments")]
        public async Task<ActionResult> GetParentDepartmentsAsync(int departmentId)
        {
            var result = await _departmentService.GetParentDepartmentsAsync(departmentId);
            if (result.IsSuccess == true)
                return Ok(result.SavedObject);
            else
                return NotFound(result.Message);
        }



    }
}
