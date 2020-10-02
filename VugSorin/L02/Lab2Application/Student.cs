using System;

namespace Lab2Application
{
    public class Student
    {
        public Student () {}
        public int Id { get; set; }

        public string Name { get; set; }

        public string University { get; set; }

        
        public Student (int id = -1, string name = "TBD", string university = "TBD") {
            Id = id;
            Name = name;
            University = university;
        }
    }
}