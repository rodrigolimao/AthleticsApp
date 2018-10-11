namespace AtleticaEcoUff.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sport
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sport()
        {
            Athletes = new HashSet<Athlete>();
        }

        [Key]
        [Display(Name = "Sport ID")]
        public int sport_id { get; set; }

        [Required]
        [StringLength(25)]
        [Display(Name = "Sport Name")]
        public string sport_name { get; set; }

        [Display(Name = "Sport Rating")]
        public int sport_rating { get; set; }

        [Display(Name = "Sport Awards")]
        public int sport_awards { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Athlete> Athletes { get; set; }
    }
}
