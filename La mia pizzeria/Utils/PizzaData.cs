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
                Pizza pizza = new Pizza(i, "Pizza n-" + (i + 1), "Lorem Ipsum is simply dummy text of the printing and typesetting... ",5.5, "/img/pizza-margherita.png");
                newPizzas.Add(pizza);
            }

            pizzas = ReadCatalogFile();

            return pizzas;
        }

        private static List<Pizza> ReadCatalogFile()
        {
            List<Pizza> PizzasFromFile = new List<Pizza>();

            StreamReader file = null;
            string path = "wwwroot\\Catalog.csv";

            try
            {
                file = File.OpenText(path);
                while (!file.EndOfStream)
                {
                    string line = file.ReadLine();
                    string[] parts = line.Split(',');
                    if (parts.Length == 5)
                        PizzasFromFile.Add(new Pizza(int.Parse(parts[0]), parts[1], parts[2], Double.Parse(parts[3]) ,parts[4]));
                }
                file.Close();
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }



            return PizzasFromFile;
        }
    }
}
