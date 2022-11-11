using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I1JM39_HFT_2022231.Models
{
    public class Game
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GameId { get; set; }

        [Required]
        [StringLength(250)]
        public string GameName { get; set; }

        [Range(0, 20000)]
        public double Income { get; set; }

        [Range(0, 10)]
        public double Rating { get; set; }

        public DateTime Release { get; set; }

        [ForeignKey(nameof(Developer))]
        public int DeveloperId { get; set; }

        [NotMapped]
        public virtual Developer Developer { get; set; }
        
        [NotMapped]
        public virtual ICollection<Character> Characters { get; set; }

        public Game() { }
        public Game(string line)
        {
            string[] split = line.Split('#');
            GameId = int.Parse(split[0]);
            GameName = split[1];
            Income = double.Parse(split[2]);
            Rating = double.Parse(split[3]);
            Release = DateTime.Parse(split[4].Replace('*','.'));
            DeveloperId = int.Parse(split[5]);
        }
    }
}
