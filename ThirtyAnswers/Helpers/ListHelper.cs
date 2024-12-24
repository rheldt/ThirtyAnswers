using System.Collections.Generic;
using System.Security.Cryptography;

namespace ThirtyAnswers.Helpers
{
    public static class ListHelper
    {
        /// <summary>
        /// Shuffles the specified list of items.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputList"></param>
        /// <returns></returns>
        public static List<T> ShuffleList<T>(List<T> inputList)
        {
            // Adapted from codesnippets.fesslersoft.de/shuffle-a-list
            var cryptoServiceProvider = new RNGCryptoServiceProvider();
            var count = inputList.Count;
            while (count > 1)
            {
                var bytes = new byte[1];
                do cryptoServiceProvider.GetBytes(bytes);
                while (!(bytes[0] < count * (byte.MaxValue / count)));

                var index = (bytes[0] % count);
                count--;

                var input = inputList[index];
                inputList[index] = inputList[count];
                inputList[count] = input;
            }
            return inputList;
        }
    }
}