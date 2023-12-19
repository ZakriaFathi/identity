namespace Api.Helpers
{
    public class IdentityServerSettings
    {
        public string DiscoveryUrl { get; set; }
        public string ClinentName { get; set; }
        public string ClinentPassword { get; set; }
        public bool UseHttps { get; set; }
    }
}
