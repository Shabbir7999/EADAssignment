using DL.DbModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyWebApiStudentGPA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly StudentDbContext dbContext;
        public SubjectController(StudentDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // GET: api/<SubjectController>
        [HttpGet]
        public IActionResult  GetSubjects()
        {
            var subjects = dbContext.subjectDbDto.ToList();
            return Ok(subjects);
        }

       
        [HttpGet("{subjectId}")]
        public IActionResult Get(int subjectId)
        {
            var subject = dbContext.subjectDbDto.Find(subjectId);
            if (subject == null)
            {
                return NotFound();
            }
            return Ok(subject);
        }

        [HttpPost]
        public IActionResult AddSubject([FromBody] SubjectDbDto subjectDbDto)
        {
            dbContext.subjectDbDto.Add(subjectDbDto);
            dbContext.SaveChanges();

            return Ok();
        }

        [HttpPut("{subjectId}")]
        public IActionResult EditSubject([FromRoute]int subjectId, [FromBody] SubjectDbDto updatedSubject)
        {
            if (subjectId != updatedSubject.id)
            {
                return BadRequest();
            }

            var subject = dbContext.subjectDbDto.Find(subjectId);
            if (subject == null)
            {
                return NotFound();
            }

            subject.Name = updatedSubject.Name;
            
            dbContext.SaveChanges();
            return Ok("Updated Successfully");
        }

        [HttpDelete("{subjectId}")]
        public IActionResult DeleteStudent([FromRoute] int subjectId)
        {
            var subject = dbContext.subjectDbDto.Find(subjectId);
            if (subject == null)
            {
                return NotFound();
            }

            dbContext.subjectDbDto.Remove(subject);

            dbContext.SaveChanges();

            return Ok("Student Deleted Successfully");
        }
    }
}
