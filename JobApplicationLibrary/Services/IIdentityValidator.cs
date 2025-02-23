namespace JobApplicationLibrary.Services
{
    public interface IIdentityValidator
    {
        bool IsValid(string identityNumber);
        bool CheckConnectionToRemoteServer();

        public string Country { get; }
    }
}