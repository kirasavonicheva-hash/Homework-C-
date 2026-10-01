using System;
using System.Linq;

namespace Task1;

public class PopularPasswords
{
    
    private static readonly string[] WeakPasswords =
    {
        "123456", "password", "123456789", "12345",
        "12345678", "qwerty", "abc123", "111111",
        "123123", "iloveyou", "1234567890", "qwerty123",
        "000000", "1q2w3e", "aa12345678", "password1",
        "1234", "qwertyuiop" , "Password123"
    };
    
    public int Passwords(string[] passwords)
    {
        int weakCount = 0;

        foreach (string password in passwords)
        {
            if (WeakPasswords.Contains(password, StringComparer.OrdinalIgnoreCase))
            {
                Console.WriteLine($"Пароль ТУПОРЫЛЫЙ усложни его по братски.");
                weakCount++;
            }
        }

        return weakCount;
    }
}