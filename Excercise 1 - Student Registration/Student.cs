using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excercise_1___Student_Registration
{
    public struct Address
    {
        public int HouseNumber{get;set;}
        public string StreetName { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public string Country { get; set; }
    }

    public struct Contact
    {
        public string HomePhoneNumber { get; set; }
        public string MobilePhoneNumber { get; set; }
        public string EmailAddress { get; set; }
    }

    public enum YearOfStudy
    {
        FirstYear,
        SecondYear,
        ThirdYear,
        FourthYear,
        MastersYear
    }

    public class Student
    {
        public string Surname { get; set; }
        public string Firstname { get; set; }
        public Address TermAddress { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string CourseStudying { get; set; }
        public YearOfStudy Year { get; set; }
        public Contact ContactDetails { get; set; }

        public override string ToString()
        {
            return string.Format("{0}, {1}", Surname, Firstname);
        }

    }
}
