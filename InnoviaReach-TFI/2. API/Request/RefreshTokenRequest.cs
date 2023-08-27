namespace Api.Request
{
    public class RefreshTokenRequest
    {
        public RefreshTokenRequest(string bearerToken, string refreshToken)
        {
            BearerToken = bearerToken;
            RefreshToken = refreshToken;
        }
        public string BearerToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
