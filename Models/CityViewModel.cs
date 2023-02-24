using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CoreDemo.Models
{
    public class CityViewModel
    {
        public string Sehir { get; set; }
        public IList<SelectListItem> Sehirler { get; set; }
    }
}
