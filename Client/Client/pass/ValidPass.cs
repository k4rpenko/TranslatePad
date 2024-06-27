using System.Text.RegularExpressions;

namespace Client.pass
{
    public class PassValidation
    {
        public class ValidationResult
        {
            public bool IsValid { get; set; }
            public string Message { get; set; }
        }

        public ValidationResult ValidatePassword(string password)
        {
            var result = new ValidationResult();
            var input = password;

            if (string.IsNullOrWhiteSpace(input))
            {
                result.IsValid = false;
                result.Message = "The password field is empty";
                return result;
            }

            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMiniMaxChars = new Regex(@".{6,20}");
            var hasLowerChar = new Regex(@"[a-z]+");

            if (!hasLowerChar.IsMatch(input))
            {
                result.IsValid = false;
                result.Message = "The password must contain at least one lowercase letter";
                return result;
            }
            else if (!hasMiniMaxChars.IsMatch(input))
            {
                result.IsValid = false;
                result.Message = "The password must be at least 6 characters long";
                return result;
            }
            else if (!hasNumber.IsMatch(input))
            {
                result.IsValid = false;
                result.Message = "The password must contain at least one numeric value";
                return result;
            }
            else
            {
                result.IsValid = true;
                result.Message = "Password is valid";
                return result;
            }
        }
    }
}
