namespace AuthService.Application.Abstractions.Cipher
{
    public interface IEncoder
    {
        public string Encode(string plainText);
        public string Decode(string encodedText);
    
    }
}
