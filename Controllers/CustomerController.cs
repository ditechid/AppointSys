using AppointSys.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;

namespace AppointSys.Controllers
{
    public class CustomerController : Controller
    {
        Uri baseAddress;
        private readonly HttpClient _client;
        private readonly IConfiguration _config;

        public CustomerController(IConfiguration configuration)
        {
            _config = configuration;
            baseAddress = new Uri(_config["BaseUrl"]);
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<CustomerViewModel> customerList = new List<CustomerViewModel>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Customer/GetCustomers").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                customerList= JsonConvert.DeserializeObject<List<CustomerViewModel>>(data);
            }
            return View(customerList);
        }

        public async Task<IActionResult> Detail(string id)
        {
          
            CustomerViewModel customer = new CustomerViewModel();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Customer/GetCustomer/" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                customer = JsonConvert.DeserializeObject<CustomerViewModel>(data);                
            }
            return View(customer);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CustomerViewModel model)
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
            catch(Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return View();
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            try
            {
                CustomerViewModel customer = new CustomerViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Customer/GetCustomer/" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    customer = JsonConvert.DeserializeObject<CustomerViewModel>(data);
                }
                return View(customer);
            }
            catch(Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        [HttpPost]
        public IActionResult Edit(CustomerViewModel model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + "/Customer/UpdateCustomer/" + model.Id,content).Result;
            if (response.IsSuccessStatusCode)
            {
                TempData["successMessage"] = "Customer updated.";
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult Delete(string id)
        {
            HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress + "/Customer/Delete/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

    }
}
