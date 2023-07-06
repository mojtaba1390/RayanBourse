using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;

namespace RayanBourse.Domain.Entities
{
    public class Product
    {
        

        [MaxLength(100)]
        [Required]
        public string Name { get; set; }


        //assume that it is mobile number
        [StringLength(11, MinimumLength = 11, ErrorMessage = "mobile number must be 11 charector")]
        [Required]
        public string ManufacturePhone { get; set; }


        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Required]
        [Key]
        [Column(Order = 1)]
        [MaxLength(100)]
        [DataType(DataType.EmailAddress)]
        public string ManufactureEmail { get; set; }


        [Required]
        [Key]
        [Column(Order = 2)]
        public DateTime ProduceDate { get; set; }

        [Required]
        public EnumYesNo IsAvailable { get; set; }

        public string UserId { get; set; }

        public  ApplicationUser User { get; set; }

    }
}
