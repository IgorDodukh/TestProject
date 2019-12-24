using System;
using System.Linq;

namespace TestProject
{
    public class AppUser
    {
        private const string UserName = "testname";
        private const string UserEmail = "test{0}@test.test";
        private const string UserPassword = "Testpass11";
        private const string UserPhone = "+380{0}";

        public string Name { get; }
        public string Login { get; }
        public string Password { get; }
        public string PhoneNumber { get; }
        public string Street { get; }
        public string Place { get; }
        public string Room { get; }

        public AppUser()
        {
            Name = UserName;
            Login = RandomLogin();
            Password = UserPassword;
            PhoneNumber = RandomPhone();
            Street = RandomString(5);
            Place = RandomDigits(1);
            Room = RandomDigits(1);
        }

        private static string RandomLogin()
        {
            return string.Format(UserEmail, RandomDigits(5));
        }

        private static string RandomPhone()
        {
            return string.Format(UserPhone, RandomDigits(9));
        }

        private static string RandomString(int length)
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        
        private static string RandomDigits(int length)
        {
            var random = new Random();
            string s = string.Empty;
            for (int i = 0; i < length; i++)
                s = String.Concat(s, random.Next(10).ToString());
            return s;
        }
    }
}