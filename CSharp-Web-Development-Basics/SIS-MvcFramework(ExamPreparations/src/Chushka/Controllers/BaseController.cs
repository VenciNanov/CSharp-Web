using Chushka.Data;
using SIS.MvcFramework;

namespace Chushka.Controllers
{
    public abstract class BaseController : Controller
    {
        public BaseController()
        {
            Db = new ApplicationDbContext();
        }
        public ApplicationDbContext Db { get; set; }
    }
}
