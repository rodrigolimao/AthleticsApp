namespace AtleticaEcoUff.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Athlete
    {
        [Key]
        public int athlete_id { get; set; }

        [Required]
        [StringLength(50)]
        public string athlete_name { get; set; }

        public int athlete_age { get; set; }

        public int sport_id { get; set; }

        public virtual Sport Sport { get; set; }
    }
}
