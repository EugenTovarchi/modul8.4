using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;

namespace Task4
{
    internal class Student
    {
        public string Name { get; set; }
        public string Group { get; set; }

        private DateTime dateOfBirth;
        public DateTime DateOfBirth
        {
            get => dateOfBirth.Date;
            set => dateOfBirth = value.Date;
        }                                     
        public decimal AverageScore { get; set; }
    }
}
