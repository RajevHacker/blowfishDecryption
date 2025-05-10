using BlowFishDecryption;
using Newtonsoft.Json.Linq;
public class JsonDecryptorService
    {
        private readonly string _secretKey;

        public JsonDecryptorService(string secretKey)
        {
            _secretKey = secretKey;
        }

        public string DecryptJson(string jsonContent)
        {
            var jToken = JToken.Parse(jsonContent);
            DecryptToken(jToken);
            return jToken.ToString();
        }

        private void DecryptToken(JToken token)
        {
            if (token.Type == JTokenType.Object)
            {
                foreach (var property in token.Children<JProperty>())
                    DecryptToken(property.Value);
            }
            else if (token.Type == JTokenType.Array)
            {
                foreach (var item in token.Children())
                    DecryptToken(item);
            }
            else if (token.Type == JTokenType.String)
            {
                var value = token.ToString();
                if (value.StartsWith("![") && value.EndsWith("]"))
                {
                    var encryptedValue = value[2..^1];
                    var decryptedValue = BlowfishDecryptor.Decrypt(encryptedValue, _secretKey);
                    token.Replace(decryptedValue);
                }
            }
        }
    }