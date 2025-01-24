using GuestBookSPA.Models;
using GuestBookSPA.Repository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GuestBookSPA.Controllers
{
    public class HomeController : Controller
    {
        IRepository repo;

        public HomeController(IRepository r)
        {
            repo = r;
        }

        public async Task<IActionResult> Index()
        {           
            var model = await repo.GetAll();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetMessageList()
        {
            if (repo.GetAll == null)
                return Problem("The message list is empty!");

            List<Messages> list = await repo.GetAll();

            string response = JsonConvert.SerializeObject(list);

            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(Messages mes)
        {
            if (ModelState.IsValid)
            {
                mes.MessageDateTime = DateTime.Now;

                mes.UserId = int.Parse(HttpContext.Session.GetString("Id"));

                await repo.Create(mes);

                await repo.Save();

                string response = "The message was sent successfully!";

                return Json(response);
            }
            return Problem("Problem in message sending!");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateMessage(Messages mes)
        {
            if (ModelState.IsValid)
            {
                repo.Update(mes);

                await repo.Save();

                string response = "The message updated successfully!";

                return Json(response);
            }
            return Problem("Problem in message updating!");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            if (repo.GetAll == null)
            {
                return Problem("The message list is empty!");
            }
            var mes = await repo.GetById(id);

            if (mes != null)
            {
                repo.Delete(id);
            }
            await repo.Save();

            string response = "The message deleted successfully!";

            return Json(response);
        }
    }
}