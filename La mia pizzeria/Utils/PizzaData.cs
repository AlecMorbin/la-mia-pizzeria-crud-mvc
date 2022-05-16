using La_mia_pizzeria.Models;

namespace La_mia_pizzeria.Utils
{
    public static class PizzaData
    {
        private static List<Pizza> pizzas=null;

        public static List<Pizza> GetPizzas()
        {
            if (pizzas != null)
            {
                return pizzas;
            }

            List<Pizza> newPizzas = new List<Pizza>();

            for (int i = 0; i < 6; i++)
            {
                Pizza pizza = new Pizza( "Pizza n-" + (i + 1), "Lorem Ipsum is simply dummy text of the printing and typesetting... ",5.5, "/img/pizza-margherita.png");
                newPizzas.Add(pizza);
            }

            pizzas = newPizzas;

            return pizzas;
        }
    }
}
