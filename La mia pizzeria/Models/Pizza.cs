using System.ComponentModel.DataAnnotations;
using La_mia_pizzeria.Utils.Validation;

namespace La_mia_pizzeria.Models
{
    public class Pizza
    {
        public int Id { get; set; }

        [Required(ErrorMessage="Il nome è obbligatorio")]
        [MoreThanOneWordValidationAttribute]
        public string Name { get; set; }

        [Required(ErrorMessage = "La descrizione è obbligatorio")]
        public string Descrizione { get; set; }

        [Required(ErrorMessage ="Un'immagine è obbligatoria")]
        public string Image { get; set; }

        public Pizza() { }

        public Pizza(int id,string name, string descrizione, string image)
        {
            this.Id = id;
            this.Name = name;
            this.Descrizione = descrizione;
            this.Image = image;
        }

    }
}
