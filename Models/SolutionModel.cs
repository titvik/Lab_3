using System.ComponentModel.DataAnnotations;
using lab3.Utilities;
namespace lab3.Models
{
    public class SolutionModel
    {   


        [Required(ErrorMessage = "Вы ничего не ввели!")]
        [ValidAnswerDomain(ErrorMessage = "Число должно быть >= -20 и <= 20!")]
        public string solution {get; set;}

    }
}