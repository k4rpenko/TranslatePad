using System.Text.RegularExpressions;

namespace Client.pass
{
    public class EmailValidation
    {
        public class ValidationResult
        {
            public bool IsValid { get; set; }
            public string Message { get; set; }
        }

        public ValidationResult ValidateEmail(string email)
        {
            var result = new ValidationResult();
            var input = email;

            if (string.IsNullOrWhiteSpace(input))
            {
                result.IsValid = false;
                result.Message = "The email field is empty";
                return result;
            }

            var emailRegex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.com$");

            if (!emailRegex.IsMatch(input))
            {
                result.IsValid = false;
                result.Message = "Invalid email format";
                return result;
            }
            else
            {
                result.IsValid = true;
                result.Message = "Email is valid";
                return result;
            }
        }
    }
}
