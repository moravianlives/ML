using Microsoft.AspNetCore.Mvc;
using Zen.Web.Data.Controller;

namespace edu.bucknell.project.moravianLives.API.Controllers
{
    [Route("data/source")]
    public class Source : DataController<model.Source> { }
}
