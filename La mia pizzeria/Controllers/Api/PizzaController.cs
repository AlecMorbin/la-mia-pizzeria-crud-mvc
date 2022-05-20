using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using La_mia_pizzeria.Models;
using La_mia_pizzeria.Data;
using Microsoft.EntityFrameworkCore;

namespace La_mia_pizzeria.Controllers.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PizzaController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get(string? search)
        {
            List<Pizza> listPizzas = new List<Pizza>();

            using (PizzaContext db = new PizzaContext()) 
            {
                if (!String.IsNullOrEmpty(search))
                {
                    listPizzas = db.Pizzas.Where(pizza => pizza.Name.Contains(search) || pizza.Descrizione.Contains(search)).ToList();
                }
                else
                {
                    listPizzas = db.Pizzas.Include(pizza => pizza.Category).ToList();
                }
            }

            return Ok(listPizzas);
        }

    }
}
