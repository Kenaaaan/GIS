using System.Collections.Generic;

namespace Gis.Api.Models
{
    public class UcenikFilter
    {
        public int? Level { get; set; }
        public string Teritorija { get; set; }
        public string Starost { get; set; }
        public string Spol { get; set; }
        public string EducationStatus { get; set; }
        public int? Skip { get; set; }
        public int? Limit { get; set; } = 100;
        public string SortBy { get; set; } = "Teritorija";
        public bool SortDescending { get; set; } = false;
    }
}