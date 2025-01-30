using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Inwentarz.Models
{
    public class ApplicationUser : IdentityUser
    {

        public string Imie {  get; set; }
        public string Nazwisko { get; set; }

    }
}
