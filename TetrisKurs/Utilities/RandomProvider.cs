using System.Security.Cryptography;
using This = TetrisKurs.Utilities.RandomProvider;

namespace TetrisKurs.Utilities
{
    public class RandomProvider
    {
        private static ThreadLocal<Random> RandomWrapper { get; } = new ThreadLocal<Random>(() =>
        {
            //--- PCL で RNGCryptoServiceProvider が使えないので GUID で代用
            //var @byte = Guid.NewGuid().ToByteArray();
            //var seed = BitConverter.ToInt32(@byte, 0);
            //return new Random(seed);

            var @byte = new byte[sizeof(int)];
            using (var crypto = new RNGCryptoServiceProvider())
                crypto.GetBytes(@byte);
            var seed = BitConverter.ToInt32(@byte, 0);
            return new Random(seed);
        });
        public static Random ThreadRandom => This.RandomWrapper.Value;
    }
}
