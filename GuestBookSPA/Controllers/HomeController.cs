using Microsoft.AspNetCore.Mvc;
using GuestBookSPA.Models;
using GuestBookSPA.Repository;

namespace GuestBookSPA.Controllers
{
    public class HomeController : Controller
    {
        /*private readonly UserContext _context;
        public HomeController(UserContext context)
        {
            _context = context;
        }*/

        IRepository repo;

        public HomeController(IRepository r)
        {
            repo = r;
        }

        // GET: Messages
        public async Task<IActionResult> Index()
        {
            //var userContext = await _context.Messages.Include(p => p.User);
            var model = await repo.GetMessageList();
            return View(model);
        }

        // GET: Messages/Create
        public IActionResult Create()
        {
            //ViewData["Id_User"] = new SelectList(repo.GetMessageList(), "Id","Message","MessageDate","User.Name" );
            return View();
        }

        // POST: Messages/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Message")] Messages mes)
        {
            try
            {               
                mes.MessageDateTime = DateTime.Now;

                mes.UserId = int.Parse(HttpContext.Session.GetString("Id"));

                await repo.Create(mes);

                await repo.Save();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}