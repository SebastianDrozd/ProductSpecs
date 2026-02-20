using System.Security.Cryptography;

namespace ProductSpecs.Util
{
    public class Generators
    {
        public static string CreateSessionId() 
        {
            var bytes = RandomNumberGenerator.GetBytes(32);
            return Convert.ToHexString(bytes).ToLowerInvariant();
        }
    }
}
