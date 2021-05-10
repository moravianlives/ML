using Microsoft.AspNetCore.Mvc;
using Zen.Web.Data.Controller;

namespace edu.bucknell.project.moravianLives.API.Controllers
{
    [Route("data/location")]
    public class Location : DataController<model.Location> { }
}
