using AuthService.Application.Abstractions.Cipher;

namespace AuthService.Persistence.Utils.Cipher
{
    public class SampleEncoder : IEncoder
    {
        public string Decode(string encodedText)
        {
            return encodedText;
        }

        public string Encode(string plainText)
        {
            return plainText;
        }
    }
}
