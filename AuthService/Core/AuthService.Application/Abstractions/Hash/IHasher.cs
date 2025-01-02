namespace AuthService.Application.Abstractions.Hash
{
    public interface IHasher
    {
        public string Hash(string plainText);
    }
}
