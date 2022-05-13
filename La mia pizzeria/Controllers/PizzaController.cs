using Microsoft.AspNetCore.Mvc;
using La_mia_pizzeria.Models;
using La_mia_pizzeria.Utils;


namespace La_mia_pizzeria.Controllers
{
    public class PizzaController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            List<Pizza> pizzas = PizzaData.GetPizzas();
            return View(pizzas);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            Pizza pizza = GetPizzaById(id);

            if (pizza == null)
                return NotFound("La pizza numero "+ id + " non è stata trovata");
            else
                return View(pizza);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View("CreateEntry");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Pizza nuovaPizza)
        {
            if (!ModelState.IsValid)
            {
                return View("CreateEntry",nuovaPizza);
            }

            Pizza pizzaIndicizzata = new Pizza(PizzaData.GetPizzas().Count,nuovaPizza.Name,nuovaPizza.Descrizione,nuovaPizza.Prezzo,nuovaPizza.Image);
            //Modello è corretto perciò aggiungo la pizza
            PizzaData.GetPizzas().Add(pizzaIndicizzata);

            return RedirectToAction("Index","Pizza");
        }

        private Pizza GetPizzaById(int id)
        {
            Pizza foundPizza = null;

            foreach (Pizza pizza in PizzaData.GetPizzas())
            {
                if (pizza.Id == id)
                {
                    foundPizza = pizza;
                    break;
                }
            }
            return foundPizza;
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            Pizza pizzaToEdit = GetPizzaById(id);

            if(pizzaToEdit != null)
                return View("Update",pizzaToEdit);
            else
                return NotFound();
        }

        [HttpPost]
        public IActionResult Update(int id, Pizza model)
        {
            if(!ModelState.IsValid)
                return View(model);

            Pizza pizzaToUpdate = GetPizzaById(id);

            if (pizzaToUpdate != null)
            {
                pizzaToUpdate.Name = model.Name;
                pizzaToUpdate.Descrizione = model.Descrizione;
                pizzaToUpdate.Image = model.Image;

                return RedirectToAction("Index");
            }
            else
                return NotFound();
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            int indexPizzaToDelete = -1;

            for (int i = 0; i < PizzaData.GetPizzas().Count; i++)
            {
                if (PizzaData.GetPizzas()[i].Id == id)
                    indexPizzaToDelete = i;
            }

            if (indexPizzaToDelete > -1)
            {
                PizzaData.GetPizzas().RemoveAt(indexPizzaToDelete);
                return RedirectToAction("Index");
            }
            else
            {
                return NotFound();
            }
        }
    }
}
