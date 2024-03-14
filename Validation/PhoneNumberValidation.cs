using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Ass2_PizzaStore_VanTuan.Validation
{
    public class PhoneNumberValidation : ValidationAttribute
    {
        public PhoneNumberValidation()
        {
            ErrorMessage = "Unrecognizable Phone Numbers";
        }
        public override bool IsValid(object? value)
        {
            if (value == null) return false;
            const string phonePattern = @"^(\+\d{1,2}\s?)?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$";
            return Regex.IsMatch(value.ToString(), phonePattern);
        }

    }
}
