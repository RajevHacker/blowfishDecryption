using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Paddings;
using Org.BouncyCastle.Crypto.Parameters;
using System.Text;
namespace BlowFishDecryption;
public static class BlowfishDecryptor
{
    public static string Decrypt(string base64CipherText, string secretKey)
    {
        var inputBytes = Convert.FromBase64String(base64CipherText);
        var keyBytes = Encoding.UTF8.GetBytes(secretKey);

        var cipher = new PaddedBufferedBlockCipher(new BlowfishEngine());
        cipher.Init(false, new KeyParameter(keyBytes)); // false = decrypt

        var output = new byte[cipher.GetOutputSize(inputBytes.Length)];
        var length = cipher.ProcessBytes(inputBytes, 0, inputBytes.Length, output, 0);
        length += cipher.DoFinal(output, length);

        return Encoding.UTF8.GetString(output, 0, length);
    }
}