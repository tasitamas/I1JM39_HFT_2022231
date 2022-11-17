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
    public class Character
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CharacterId { get; set; }

        [Required]
        [StringLength(200)]
        public string CharacterName { get; set; }
        public int Priority { get; set; }

        [ForeignKey(nameof(Game))]
        public int GameId { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual Game Game { get; private set; }

        public Character() { }
        public Character(string line)
        {
            string[] split = line.Split('#');
            CharacterId = int.Parse(split[0]);
            CharacterName = split[1];
            Priority = int.Parse(split[2]);
            GameId = int.Parse(split[3]);
        }
    }
}