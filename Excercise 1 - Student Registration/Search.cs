using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excercise_1___Student_Registration
{
    public class Search
    {
        public enum SearchType
        {
            firstname,
            surname,
            course,
        }

        public SearchType TypeOfSearch { get; set; }
        public string SearchTerm{get;set;}

        
        public Search()
        {
            TypeOfSearch = SearchType.firstname;
            SearchTerm = "";
        }


    }
}
