using System;
using System.Collections.Generic;

namespace Lab2Application.webapi
{
    public class StudentRepo
    {
        public List<Student> studentList = new List<Student>() 
        {
            new Student(1, "Vug","UPT"),
            new Student(2, "Sadoveac","UVT"),
            new Student(3, "Popescu","UMFT"),
        };
    }
}