using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace La_mia_pizzeria.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Il campo è obbligatori")]
        [StringLength(75, ErrorMessage = "Il titolo della categorian non puo superare i 75 caratteri")]
        public string Name { get; set; }

        [JsonIgnore]
        public List<Pizza> Pizzas { get; set; }

        public Category()
        {

        }
    }
}
