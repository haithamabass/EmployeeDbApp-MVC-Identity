using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace EmployeeDbApp.Models.Content
{
    public class City
    {


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "City ID")]
        public int CityId { get; set; }



        [Required]
        [Column(TypeName = "varchar(150)")]
        [Display(Name = "City Name")]
        public string CityName { get; set; }
    }
}
