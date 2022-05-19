using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using La_mia_pizzeria.Models;
using La_mia_pizzeria.Data;

namespace La_mia_pizzeria.Controllers.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PizzaController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            List<Pizza> listPizzas = new List<Pizza>();

            using (PizzaContext db = new PizzaContext()) 
            {
                listPizzas = db.Pizzas.ToList<Pizza>();
            }

            return Ok(listPizzas);
        }

    }
}
