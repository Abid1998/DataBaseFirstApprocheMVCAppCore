using Microsoft.AspNetCore.Mvc.Rendering;

namespace DataBaseFirstApprocheMVCAppCore.Models
{
    public class StudentModel
    {
        public int id { get; set; }
        public List<SelectListItem> StudentsList { get; set; }
    }
}
