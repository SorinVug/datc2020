using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Lab2Application.webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {
        StudentRepo studRepo = new StudentRepo();

        [HttpGet("{id}")]

        public Student GetStudent(int id)
        {
            foreach (Student std in studRepo.studentList)
            {
                if (std.Id == id)
                    return std;
            }

            return null;
        }

        [HttpDelete("{id}")]

        public List<Student> DeleteStudent(int id)
        {
            foreach (Student std in studRepo.studentList)
            {
                if (std.Id == id) 
                {
                    studRepo.studentList.Remove(std);
                    return studRepo.studentList;
                }
            }
            return null;
        }
        
        [HttpPost]

        public List<Student> InsertStudent([FromBody] Student student)
        {
            studRepo.studentList.Add(student);
            return studRepo.studentList;
        }

        [HttpPut] 
        public Student UpdateStudent([FromBody] Student student)
        {
            foreach (Student std in studRepo.studentList) 
            {
                if (std.Id == student.Id) 
                {
                    std.Name = student.Name;
                    std.University = student.University;
                    return std;
                }
            }
            return null;
        }

        
    }
}