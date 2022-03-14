using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace EmployeeDbApp.Models.Content
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Employee ID")]
        public int EmployeeId { get; set; }

        [Required]
        [Column(TypeName ="varchar(150)")]
        [MaxLength(100)]
        [Display(Name ="Employee Name")]
        public string EmployeeName { get; set; }



        [Required]
        [DataType(DataType.Date)]
        [Display(Name ="Date Of Birth")]
        [DisplayFormat(DataFormatString ="{0:dd-MMM-yyyy}")]
        public DateTime DOB { get; set; }


        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Hiring Date")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime HiringDate { get; set; }


        [Required]
        [Column(TypeName = "decimal(12)")]
        [Display(Name = "Gross Salary")]
        public decimal GrossSalary { get; set; }


        [Required]
        [Column(TypeName = "decimal(12)")]
        [Display(Name = "Net Salary")]
        public decimal NetSalary { get; set; }




        [ForeignKey("City")]
        [Required]
        public int CityId { get; set; }


        [Display(Name = "City")]
        [NotMapped]
        public string CityName { get; set; }

        public virtual City City { get; set; }




        [ForeignKey ("Department")]
        [Required]
        public int DepartmentId { get; set; }


        [Display(Name = "Department")]
        [NotMapped]
        public string DepartmentName { get; set; }

        public virtual Department Department { get; set; }


        [Column(TypeName ="varchar(150)")]
        [Display(Name = "Job Title")]
        public string JobTitle { get; set; }



        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        public int ? PhoneNumber { get; set; }



















    }
}
