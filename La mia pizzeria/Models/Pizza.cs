using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using La_mia_pizzeria.Utils.Validation;

namespace La_mia_pizzeria.Models
{
    public class Pizza
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage="Il nome è obbligatorio")]
        [MoreThanOneWordValidationAttribute]
        public string Name { get; set; }

        [Required(ErrorMessage = "La descrizione è obbligatorio")]
        [Column(TypeName = "text")]
        public string Descrizione { get; set; }

        [Required(ErrorMessage ="Un'immagine è obbligatoria")]
        public string Image { get; set; }

        [Required(ErrorMessage = "Un prezzo è obbligatoria")]
        public double Prezzo { get; set; }

        public int? CategoryId { get; set; }
        public Category? Category { get; set; }

        public Pizza() { }

        public Pizza(string name, string descrizione,double prezzo, string image)
        {
            this.Name = name;
            this.Descrizione = descrizione;
            this.Prezzo = prezzo;
            this.Image = image;
        }

    }
}
