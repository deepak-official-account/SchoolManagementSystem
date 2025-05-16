using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.API.Models;
using SchoolManagementSystem.API.Repository;

namespace SchoolManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentApiController : ControllerBase
    {

        #region Private Variables
        private readonly IStudentRepository _studentRepository;
        #endregion

        #region Constructor
        public StudentApiController(IStudentRepository studentRepository)
        {
            this._studentRepository = studentRepository;
        }
        #endregion

        #region Controller Methods

        [HttpPost]
        [Route("add-student")]
        public async Task<ActionResult> AddStudent([FromBody]Student student) // Mark method as async
        {
            if (ModelState.IsValid)
            {
                var response = await this._studentRepository.AddStudentAsync(student); // Await the Task<ResponseDto>
                if (response.IsSuccess) 
                {
                    return Ok(response);
                    
                }
                else
                {
                    return BadRequest(response);
                }
            }
            return BadRequest(new ResponseDto { message="Student Information is Not valid"});
        }

        [HttpGet]
        [Route("get-all-students")]
        public async Task<IEnumerable<Student>> GetAllStudents()
        {
            return await this._studentRepository.GetAllStudentsAsync();
        }

        [HttpPut]
        [Route("update-student/{rollNo:int}")]
        public async Task<ActionResult> UpdateStudent([FromBody]Student student,[FromRoute]int rollNo)
        {
            if (ModelState.IsValid) {
                var response = await this._studentRepository.UpdateStudentAsync(student, rollNo);

                if (response.IsSuccess)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response);
                }
            }
            return BadRequest(new ResponseDto { message = "Student Information is Not valid" });
        }

        [HttpDelete]
        [Route("delete-student/{rollNo:int}")]
        public async Task<IActionResult> DeleteStudent([FromRoute]int rollNo)
        {
            var response = await this._studentRepository.DeleteStudentAsync(rollNo);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }
        // Other methods remain unchanged
    }
    #endregion
}
