using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
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

        public int GameId { get; set; }
        public virtual Game Game { get; private set; }

        public Character(){}
    }
}
