﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaOrder.Models;
using PizzaOrder.ViewModel;

namespace PizzaOrder.Areas.Admin.Controllers
{
    public class PizzaController : Controller
    {
        ApplicationContext db;

        IWebHostEnvironment _appEnvironment;

        public PizzaController(ApplicationContext context, IWebHostEnvironment appEnvironment)
        {
            db = context;
            _appEnvironment = appEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            return View(await db.Pizzas.ToListAsync());
        }

        public IActionResult PizzaAdd()
        {
            List<PizzaComponent> pizzaComponents = db.PizzaComponents.ToList();
            Pizza pizza = null;

            PizzaViewModel pvm = new PizzaViewModel { PizzaComponents = pizzaComponents, Pizza = pizza };

            return View(pvm);
        }

        [HttpPost]
        public async Task<IActionResult> PizzaAdd(Pizza pizza, IList<int> comId, IFormFile uploadedFile)
        {
            if (ModelState.IsValid)
            {
                if (uploadedFile != null)
                {
                    pizza.PhotoName = uploadedFile.FileName;
                    string path = "/images/pizzas/" + uploadedFile.FileName;
                    pizza.PhotoPath = path;
                    using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await uploadedFile.CopyToAsync(fileStream);
                    }
                }

                List<PizzaComponent> pizzaComponents = new List<PizzaComponent>();
                foreach(var item in comId)
                {
                    PizzaComponent component = db
                        .PizzaComponents
                        .FirstOrDefault(x => x.Id == item);
                    pizzaComponents.Add(component);
                }
                pizza.PizzaComponents = pizzaComponents;
                db.Pizzas.Add(pizza);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                return View(pizza);
            }
        }
    }
}
