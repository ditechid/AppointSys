using System.ComponentModel.DataAnnotations;

namespace AppointSys.ViewModels
{
    public class AppointmentViewModel
    {
        public string Code { get; set; }
        [DisplayFormat(DataFormatString = "{0:MMM dd yyyy - HH:mm}")]
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public CustomerViewModel Customer { get; set; }
    }
}
