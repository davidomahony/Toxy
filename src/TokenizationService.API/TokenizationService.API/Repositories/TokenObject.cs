namespace TokenizationService.API.Repositories
{
    public class TokenObject
    {
        public TokenObject(string token, string value)
        {
            Token = token;
            Value = value;
        }

        public string Token { get; set; }

        public string Value { get; set; }
    }
}
