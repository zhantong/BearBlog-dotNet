using BearBlog.Models;
using Microsoft.AspNetCore.Mvc;

namespace BearBlog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NavbarController : ControllerBase
    {
        // GET: api/Navbar
        [HttpGet]
        public Navbar Get()
        {
            var navbar = new Navbar();
            Events.OnNavbarInit(new NavbarInitEventArgs() {Navbar = navbar});
            return navbar;
        }
    }
}