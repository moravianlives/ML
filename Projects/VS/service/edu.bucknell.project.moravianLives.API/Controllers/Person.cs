using Microsoft.AspNetCore.Mvc;
using Zen.Web.Data.Controller;

namespace edu.bucknell.project.moravianLives.API.Controllers
{
    [Route("data/person")]
    public class Person : DataController<model.Person> {
    }
}
