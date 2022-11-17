using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace I1JM39_HFT_2022231.Models
{
    [Table("Developers")]
    public class Developer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DeveloperId { get; set; }

        [Required]
        [StringLength(100)]
        public string DeveloperName { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<Game> Games { get; set; }

        public Developer()
        {
            Games = new HashSet<Game>();
        }
        public Developer(string line)
        {
            string[] split = line.Split('#');
            DeveloperId = int.Parse(split[0]);
            DeveloperName = split[1];
            Games = new HashSet<Game>();
        }

        public override string ToString()
        {
            return  $"DeveloperID: {DeveloperId}" +
                    $"\nName: {DeveloperName}\n";
        }
    }
}