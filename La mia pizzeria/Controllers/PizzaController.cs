using Microsoft.AspNetCore.Mvc;
using La_mia_pizzeria.Models;
using La_mia_pizzeria.Utils;
using La_mia_pizzeria.Data;

namespace La_mia_pizzeria.Controllers
{
    public class PizzaController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            List<Pizza> pizzaList = new List<Pizza>();

            using (PizzaContext db = new PizzaContext())
            {
                pizzaList = db.Pizzas.ToList();
            }

            return View("Index",pizzaList);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            using (PizzaContext db = new PizzaContext())
            {
                Pizza? pizza = db.Pizzas.Where(pizza => pizza.Id == id).FirstOrDefault ();    

                if (pizza == null)
                    return NotFound("La pizza numero " + id + " non è stata trovata");
                else
                    return View(pizza);
            }
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

            using(PizzaContext db = new PizzaContext())
            {
                Pizza pizzaToCreate = new Pizza(nuovaPizza.Name,nuovaPizza.Descrizione,nuovaPizza.Prezzo,nuovaPizza.Image);
                db.Pizzas.Add(pizzaToCreate);
                db.SaveChanges();
            }

            return RedirectToAction("Index","Pizza");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            Pizza? pizzaToEdit = null;

            using (PizzaContext db = new PizzaContext())
            {
                pizzaToEdit = db.Pizzas.Where(pizza => pizza.Id == id).FirstOrDefault();
            }

            if (pizzaToEdit != null)
                return View("Update", pizzaToEdit);
            else
                return NotFound();
        }

        [HttpPost]
        public IActionResult Update(int id, Pizza model)
        {
            if (!ModelState.IsValid)
            {
                return View("Update", model);
            }

            Pizza? pizzaToEdit = null;

            using (PizzaContext db = new PizzaContext())
            {
                pizzaToEdit = db.Pizzas.Where(pizza => pizza.Id == id).FirstOrDefault();

                if (pizzaToEdit != null)
                {
                    pizzaToEdit.Name = model.Name;
                    pizzaToEdit.Descrizione = model.Descrizione;
                    pizzaToEdit.Image = model.Image;
                    pizzaToEdit.Prezzo = model.Prezzo;

                    db.SaveChanges();
                    
                    return RedirectToAction("Index");
                }
                else
                    return NotFound();
            }
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            using (PizzaContext db = new PizzaContext())
            {
                Pizza? pizzaToDelete = db.Pizzas.Where(pizza => pizza.Id == id).FirstOrDefault();
                if (pizzaToDelete != null)
                {
                    db.Pizzas.Remove(pizzaToDelete);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    return NotFound();
                }
            }
        }
    }
}
