using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Shared.Helpers
{
    public static class RandomStringGenerator
    {
        private static readonly Random _random = new Random();
        private const string _chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        public static string GenerateRandomString(int length = 8) 
        {
            return new string(Enumerable.Repeat(_chars, length).Select(s => s[_random.Next(s.Length)]).ToArray());
        }
    }
}
