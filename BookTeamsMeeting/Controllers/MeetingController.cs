using Azure.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph.Models;
using Microsoft.Graph;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Identity.Web;
using BookTeamsMeeting.Services;
using BookTeamsMeeting.Models;

namespace BookTeamsMeeting.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MeetingController : Controller
    {
        private readonly IOnlineMeetingService _service;

        public MeetingController(IOnlineMeetingService service)
        {
            this._service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            await CreateOnlineMeeting("Test");
            return View();
        }

        public async Task<IActionResult> CreateOnlineMeeting(string subject)
        {
            //method parameters ("shubham@indpro.se", "Shubham singh") to be fetched from logged in user
            var result = await _service.CreateOnlineMeeting("shubham@indpro.se", "Shubham singh");
            return Ok(result);
        }
        //[HttpPost]
        //public async Task<IActionResult> CreateOnlineMeeting([FromBody] OnlineMeetingModel request)
        //{
        //    var result = await _service.CreateOnlineMeetingService();
        //    return Ok(result);
        //}

    }
}
