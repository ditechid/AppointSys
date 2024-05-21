using AppointSys.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Configuration;
using System.Text;

namespace AppointSys.Controllers
{
    public class AppointmentController : Controller
    {
        Uri baseAddress;
        private readonly HttpClient _client;
        private readonly IConfiguration _config;

        public AppointmentController(IConfiguration configuration)
        {
            _config = configuration;
            baseAddress = new Uri(_config["BaseUrl"]);
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<AppointmentViewModel> AppointmentList = new List<AppointmentViewModel>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Appointment/GetAppointments").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                AppointmentList = JsonConvert.DeserializeObject<List<AppointmentViewModel>>(data);
            }
            return View(AppointmentList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            List<CustomerViewModel> customerList = new List<CustomerViewModel>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Customer/GetCustomers").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                customerList = JsonConvert.DeserializeObject<List<CustomerViewModel>>(data);
            }
            ViewData["Customers"] = new SelectList(customerList, "Id", "FullName");
            return View();
        }

        [HttpPost]
        public IActionResult Create(AppointmentViewModel model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Customer/CreateCustomer", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Customer added.";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return View();
        }

        public async Task<IActionResult> Detail(string code)
        {

            AppointmentViewModel appointment = new AppointmentViewModel();
            HttpResponseMessage response =  _client.GetAsync(_client.BaseAddress + "/Appointment/GetAppointment/" + code).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                appointment = JsonConvert.DeserializeObject<AppointmentViewModel>(data);
            }
            return View(appointment);
        }

    }
}
