﻿using Microsoft.AspNetCore.Mvc;
using La_mia_pizzeria.Models;
using La_mia_pizzeria.Utils;
using La_mia_pizzeria.Data;
using Microsoft.EntityFrameworkCore;

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
                Pizza? pizza = db.Pizzas.Where(pizza => pizza.Id == id).Include(pizza => pizza.Category).FirstOrDefault();

                if (pizza == null)
                    return NotFound("La pizza numero " + id + " non è stata trovata");
                else
                    return View(pizza);
            }
        }


        [HttpGet]
        public IActionResult Create()
        {
            using(PizzaContext db = new PizzaContext())
            {
                List<Category> categories = db.Category.ToList();

                PizzaCategories model = new PizzaCategories();
                model.Pizza = new Pizza();
                model.Categories = categories;

                return View("CreateEntry",model);

            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PizzaCategories data)
        {
            if (!ModelState.IsValid)
            {
                using (PizzaContext db = new PizzaContext())
                {
                    List<Category> categories = db.Category.ToList ();

                    data.Categories = categories;
                }
                return View("CreateEntry",data);
            }

            using(PizzaContext db = new PizzaContext())
            {
                Pizza pizzaToCreate = new Pizza();
                pizzaToCreate.Name = data.Pizza.Name; 
                pizzaToCreate.Descrizione = data.Pizza.Descrizione;
                pizzaToCreate.Prezzo = data.Pizza.Prezzo;
                pizzaToCreate.Image = data.Pizza.Image; 
                pizzaToCreate.CategoryId = data.Pizza.CategoryId;

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
