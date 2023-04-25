using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using NuGet.Common;
using NuGet.Protocol;
using System.Net.Http;
using System.Net.Http.Headers;
using testCore.Models;

namespace testCore.Controllers
{
    public class EmployeeController : Controller
    {

        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<Employee> employees = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7130/");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", HttpContext.Session.GetString("Token"));

                var responseTask = client.GetAsync("api/Employee/all");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    var deserialized = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Employee>>(readTask.Result);
                    readTask.Wait();
                    employees = deserialized;
                }
                else
                {
                    employees = Enumerable.Empty<Employee>();
                    ModelState.AddModelError(string.Empty, "Employees not found.");
                }
            }
            return View(employees);
        }
        [HttpGet]
        public IActionResult AddEmployee()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddEmployee(Employee employee)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7130/");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", HttpContext.Session.GetString("Token"));

                var postTask = client.PostAsJsonAsync<Employee>("api/Employee/add", employee);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError(string.Empty, "Failed Try again.");
            return View(employee);
        }
        [HttpGet]
        public IActionResult GetEmployeeById(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7130/");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", HttpContext.Session.GetString("Token"));

                var postTask = client.GetAsync($"api/Employee/GetEmployeeById/{id}");
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    var deserialized = Newtonsoft.Json.JsonConvert.DeserializeObject<Employee>(readTask.Result);
                    readTask.Wait();
                    
                    return Json(deserialized);
                }
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult UpdateEmployee(Employee employee)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7130/");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", HttpContext.Session.GetString("Token"));

                var postTask = client.PostAsJsonAsync<Employee>("api/Employee/edit", employee);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError(string.Empty, "Failed Try again.");
            return View(employee);
        }
        [HttpGet]
        public IActionResult DeleteEmployee()
        {
            return View();
        }

        public async Task<ActionResult> DeletData(int id)
        {
            try { 
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7130/");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", HttpContext.Session.GetString("Token"));


                    var postTask = await client.PostAsJsonAsync("api/Employee/delete", id);
                var result = await postTask.Content.ReadAsStringAsync();
                if (postTask.IsSuccessStatusCode)
                {
                    return Json(result);
                }
            }
                return Json("\"Failed Try again.\"");
            }
            catch(Exception ex) { throw ex; }
        }
        [HttpGet]
        public IActionResult AddFeedback()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddFeedback(Feedback feedback)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7130/");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", HttpContext.Session.GetString("Token"));

                var postTask = client.PostAsJsonAsync<Feedback>("api/Employee/addFeedback", feedback);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError(string.Empty, "Failed Try again.");
            return View(feedback);
        }
        
        [HttpGet]
        public ActionResult GetFeedbacks(int id)
        {
            IEnumerable<Feedback> feedback = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7130/");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", HttpContext.Session.GetString("Token"));

                var postTask = client.GetAsync($"api/Employee/feedbacks/{id}");
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    var deserialized = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Feedback>>(readTask.Result);
                    readTask.Wait();
                    feedback = deserialized;
                }
            }
            return View(feedback);
        }
    }
}
