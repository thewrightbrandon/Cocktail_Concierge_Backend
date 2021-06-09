using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Encodings.Web;
using DrinksAPI.Models;

namespace DrinksAPI.Controllers
{
    [Route("drinks")] //handles routes for /drinks
    public class DrinksController : Controller
    {
        // declare property to hold Database Context
        private readonly DrinkContext drinks;

        // define constructor to receive database context via Dependency Injection
        public DrinksController(DrinkContext drinksCtx)
        {
            drinks = drinksCtx;
        }

        [HttpGet] // get request to "/drinks"
        public IEnumerable<Drink> Index()
        {
            // return all the drinks
            return drinks.Drinks.ToList();
        }

        [HttpPost] // post request to "/drinks"
        public IEnumerable<Drink> Post([FromBody] Drink Drink)
        {
            // add a drink
            drinks.Drinks.Add(Drink);

            drinks.SaveChanges();

            return drinks.Drinks.ToList();
        }

        [HttpGet("{id}")] // get request to "drinks/{id}"
        public Drink Show(long id)
        {
            // return the specified Drink matched based on id
            return drinks.Drinks.FirstOrDefault(x => x.Id == id);
        }

        [HttpPut("{id}")] // put request to "drinks/{id}"
        public IEnumerable<Drink> Update([FromBody] Drink Drink, long id)
        {
            // retrieve Drink to be updated
            Drink oldDrink = drinks.Drinks.FirstOrDefault(x => x.Id == id);
            //update their properties
            oldDrink.Name = Drink.Name;
            oldDrink.Alcohol = Drink.Alcohol;
            oldDrink.Profile = Drink.Profile;
            oldDrink.Image = Drink.Image;

            drinks.SaveChanges();

            return drinks.Drinks.ToList();
        }

        [HttpDelete("{id}")] // delete request to "drinks/{id}"
        public IEnumerable<Drink> Destroy(long id)
        {
            //retrieve existing drink
            Drink oldDrink= drinks.Drinks.FirstOrDefault(x => x.Id == id);
            //remove them
            drinks.Drinks.Remove(oldDrink);

            drinks.SaveChanges();

            return drinks.Drinks.ToList();
        }
    }
}
