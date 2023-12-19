//using Api.Helpers;
//using IdentityModel.Client;
//using Microsoft.Extensions.Options;

//namespace Api.Sevices
//{
//    public class TokenService : ITokenService
//    {
//        private readonly IOptions<IdentityServerSettings> identityServerSettings;
//        private readonly DiscoveryDocumentResponse discoveryDocument;
//        private readonly HttpClient httpClient;

//        public TokenService(IOptions<IdentityServerSettings> identityServerSettings)
//        {
//            this.identityServerSettings = identityServerSettings;
//            httpClient = new HttpClient();
//            discoveryDocument = httpClient.GetDiscoveryDocumentAsync
//                (this.identityServerSettings.Value.DiscoveryUrl).Result;
//            if (discoveryDocument.IsError)
//            {
//                throw new Exception("Unable to get discovery document", discoveryDocument.Exception);
//            }
//        }

//        public async Task<TokenResponse> GetToken(string scope)
//        {
//            var tokenResponse = await httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
//            {
//                Address = discoveryDocument.TokenEndpoint,
//                ClientId = identityServerSettings.Value.ClinentName,
//                ClientSecret = identityServerSettings.Value.ClinentPassword,
//                Scope = scope
//            });
//            if (tokenResponse.IsError)
//            {
//                throw new Exception("Unable to get token", tokenResponse.Exception);
//            }
//            return tokenResponse;
//        }
//    }
//}
