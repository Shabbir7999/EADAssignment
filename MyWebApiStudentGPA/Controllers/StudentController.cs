using DL.DbModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MyWebApiStudentGPA.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : Controller
    {
        private readonly StudentDbContext dbContext;
        public StudentController(StudentDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // GET: https://localhost:portnumber/api/Students
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            var students = dbContext.studentDbDto.ToList();
            return Ok(students);
        }


        [HttpPost]
        public IActionResult Create([FromBody] StudentDbDto studentDbDto)
        {


            dbContext.studentDbDto.Add(studentDbDto);
            dbContext.SaveChanges();

            return Ok();

        }

        [HttpPut("{studentId}")]
        public IActionResult EditStudent([FromRoute] int studentId, [FromBody] StudentDbDto updatedStudent)
        {

            if (studentId != updatedStudent.Id)
            {
                return BadRequest();
            }

            var student = dbContext.studentDbDto.Find(studentId);
            if (student == null)
            {
                return NotFound();
            }

            student.Name = updatedStudent.Name;
            student.RollNumber = updatedStudent.RollNumber;
            student.PhoneNumber = updatedStudent.PhoneNumber;

            dbContext.SaveChanges();
            return Ok("Updated Successfully");
        }



        [HttpDelete("{studentId}")]
        public IActionResult DeleteStudent([FromRoute] int studentId)
        {
            var student = dbContext.studentDbDto.Find(studentId);
            if (student == null)
            {
                return NotFound();
            }

            dbContext.studentDbDto.Remove(student);

            dbContext.SaveChanges();

            return Ok("Student Deleted Successfully");
        }
    }


}
