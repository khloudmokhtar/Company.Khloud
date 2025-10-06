using System.ComponentModel.DataAnnotations;

namespace Company.Khloud.PL.Dtos
{
    public class SignUpDto
    {
        [Required (ErrorMessage = "UserName Is Required !!")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "FirstName Is Required !!")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName Is Required !!")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email Is Required !!")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password Is Required !!")]
        [DataType (DataType.Password)] // ****
       // [RegularExpression("^[A-Za-z0-9@#$%^&*!]{1,6}$", ErrorMessage ="Password Must be Like abc123 ")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "ConfirmPassword Is Required !!")]
        [Compare(nameof(Password) , ErrorMessage = "Confirm Password doesn't match the Password !!")]
        public string ConfirmPassword { get; set; }
        public bool IsAgree { get; set; }
    }
} 
