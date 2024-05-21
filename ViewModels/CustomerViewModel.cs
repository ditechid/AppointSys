using System.ComponentModel;

namespace AppointSys.ViewModels
{
    public class CustomerViewModel
    {
        [DisplayName("ID")]
        public string Id { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [DisplayName("Full Name")]
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        [DisplayName("E-mail")]
        public string Email { get; set; }
        [DisplayName("Postal Code")]
        public string PostalCode { get; set; }
        public string Note { get; set; }
    }

}
