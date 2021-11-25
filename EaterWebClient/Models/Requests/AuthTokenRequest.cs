namespace EaterWebClient.Models.Requests
{
    public static class GrantType
    {
        public const string Password = "password";
        public const string RefreshToken = "refresh_token";
    }

    public class AuthTokenRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string GrantType { get; set; } = Requests.GrantType.Password;
        public string? RefreshToken { get; set; }
        public string Role { get; } = "CUSTOMER";
    }

    public class AuthTokenRequestValidator : AbstractValidator<AuthTokenRequest>
    {
        public AuthTokenRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Email invalid");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<AuthTokenRequest>.CreateWithOptions((AuthTokenRequest) model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
