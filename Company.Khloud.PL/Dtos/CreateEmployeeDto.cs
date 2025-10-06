using Company.Khloud.DAL.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Company.Khloud.PL.Dtos
{
    public class CreateEmployeeDto
    {

        public int Id { get; set; }

        [Required (ErrorMessage ="Name Is Required")]
        public string EmpName { get; set; }
        [Range(22,60 , ErrorMessage ="Age Must be Between 22  and 60") ]
        public int? Age { get; set; }
        [DataType (DataType.EmailAddress,ErrorMessage ="Email Is not Valid !!")]
        public string Email { get; set; }
       // [RegularExpression (" @[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$" 
           // , ErrorMessage ="Address Must be like 123-street-city-country")]
        public string Address { get; set; }
        [Phone]
        public string Phone { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        [DisplayName ("Hiring Date")]
        public DateTime HiringDate { get; set; }

        [DisplayName("Date Of Creation")]
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;

        [DisplayName("department")]

        public int? DepartmentId { get; set; } //fk

        public Department? Department { get; set; }

        public string? ImageName { get; set; }
        public IFormFile? Image { get; set; }

    }
}
