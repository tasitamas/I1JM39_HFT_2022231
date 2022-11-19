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
    [Table("Characters")]
    public class Character
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CharacterId { get; set; }

        [Required]
        [StringLength(50)]
        public string CharacterName { get; set; }

        [Required]
        [Range(1,3)]
        public int Priority { get; set; } //1 - Main, 2 - Boss, 3 - NPC

        [ForeignKey(nameof(Game))]
        public int GameId { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual Game Game { get; set; }

        public Character() { }
        public Character(string line)
        {
            string[] split = line.Split('#');
            CharacterId = int.Parse(split[0]);
            CharacterName = split[1];
            Priority = int.Parse(split[2]);
            GameId = int.Parse(split[3]);
        }

        public override string ToString()
        {
            string priority = "";
            if (this.Priority == 1)
                priority = "Main Character";
            else if (this.Priority == 2)
                priority = "Boss";
            else if (this.Priority == 3)
                priority = "NPC";

            return  $"CharacterID: {CharacterId}" +
                    $"\nName: {CharacterName}" +
                    $"\nPriority: {priority}\n";
        }
    }
}