using System.ComponentModel.DataAnnotations;

namespace Company.Khloud.PL.Dtos
{
    public class ResetPasswordDto
    {
        [Required(ErrorMessage = "Password Is Required !!")]
        [DataType(DataType.Password)] 
                                     
        public string NewPassword { get; set; }


        [DataType(DataType.Password)]
        [Required(ErrorMessage = "ConfirmPassword Is Required !!")]
        [Compare(nameof(NewPassword), ErrorMessage = "Confirm Password doesn't match the Password !!")]
        public string ConfirmPassword { get; set; }
    }
}
