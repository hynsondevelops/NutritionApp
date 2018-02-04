using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutritionApp.Entities
{
    public class Food
    {
        public int Id { get; set; }
        
    
        [Required]
        [StringLength(255)]
        public string Title { get; set; }

    }
}
