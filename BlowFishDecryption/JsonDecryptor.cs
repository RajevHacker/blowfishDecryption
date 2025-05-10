namespace BlowFishDecryption;

public static class JsonDecryptor
{
    public static string DecryptFile(string filePath, string secretKey)
    {
        var content = File.ReadAllText(filePath);
        var service = new JsonDecryptorService(secretKey);
        return service.DecryptJson(content);
    }
}