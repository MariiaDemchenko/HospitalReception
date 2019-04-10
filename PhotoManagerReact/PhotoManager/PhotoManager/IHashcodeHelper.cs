namespace PhotoManager
{
    public interface IHashcodeHelper
    {
        bool CheckHashMatch(string plainText, string salt, string hash);              

        string GenerateHash(string plainText, out string salt);        
    }
}
