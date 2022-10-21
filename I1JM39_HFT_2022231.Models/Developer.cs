using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I1JM39_HFT_2022231.Models
{
    internal class Developer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DeveloperId { get; set; }

        [Required]
        [StringLength(150)]
        public string DeveloperName { get; set; }

        public virtual ICollection<Game> Games { get; set; }

        public Developer()
        {
            Games = new HashSet<Game>();
        }
    }
}
