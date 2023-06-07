using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WEBAPI.Models;

namespace MVC.Controllers
{
    public class VideoMvcController : Controller
    {
        private HttpClient _client = new HttpClient();
        private string _apiUrl = "http://localhost:7122/api/Video"; // Replace with your Web API URL

        public async Task<IActionResult> Index()
        {
            var responseString = await _client.GetStringAsync(_apiUrl);
            var videoList = JsonConvert.DeserializeObject<List<Video>>(responseString);
            return View(videoList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Video video)
        {
            var content = new StringContent(JsonConvert.SerializeObject(video), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(_apiUrl, content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(video);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var responseString = await _client.GetStringAsync($"{_apiUrl}/{id}");
            var video = JsonConvert.DeserializeObject<Video>(responseString);
            return View(video);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Video video)
        {
            var content = new StringContent(JsonConvert.SerializeObject(video), Encoding.UTF8, "application/json");
            var response = await _client.PutAsync($"{_apiUrl}/{id}", content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(video);
        }

        public async Task<IActionResult> Details(int id)
        {
            var responseString = await _client.GetStringAsync($"{_apiUrl}/{id}");
            var video = JsonConvert.DeserializeObject<Video>(responseString);
            return View(video);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var responseString = await _client.GetStringAsync($"{_apiUrl}/{id}");
            var video = JsonConvert.DeserializeObject<Video>(responseString);
            return View(video);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _client.DeleteAsync($"{_apiUrl}/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
    }
}
