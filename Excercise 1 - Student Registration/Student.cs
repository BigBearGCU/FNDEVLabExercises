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

        public override string ToString()
        {
            return string.Format("{0}-{1}-{2}-{3}-{4}", HouseNumber, StreetName, City, PostCode,Country);
        }
    }

    public struct Contact
    {
        public string HomePhoneNumber { get; set; }
        public string MobilePhoneNumber { get; set; }
        public string EmailAddress { get; set; }

        public override string ToString()
        {
            return string.Format("{0}-{1}-{2}", HomePhoneNumber, MobilePhoneNumber, EmailAddress);
        }
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
            return string.Format("{0}-{1}-{2}-{3}-{4}-{5}-{6}", Surname, Firstname,TermAddress,DateOfBirth,CourseStudying,Year,ContactDetails);
        }

        public void parse(string s)
        {
            string[] elements = s.Split('-');
            Surname = elements[0];
            Firstname = elements[1];
            //address
            Address address = new Address();
            address.HouseNumber = int.Parse(elements[2]);
            address.StreetName = elements[3];
            address.City = elements[4];
            address.PostCode = elements[5];
            address.Country = elements[6];
            TermAddress = address;

            DateOfBirth = DateTime.Parse(elements[7]);
            CourseStudying = elements[8];
            Year = (YearOfStudy)Enum.Parse(typeof(YearOfStudy), elements[9]);
            //contact
            Contact contact = new Contact();
            contact.HomePhoneNumber = elements[10];
            contact.MobilePhoneNumber = elements[11];
            contact.EmailAddress = elements[12];
            ContactDetails = contact;
        }
    }
}
