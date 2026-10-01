using System;

namespace Task1
{
    public class RobustnessInBits
    {
        public int GetAlphabetSize(string password)
        {
            if (string.IsNullOrEmpty(password)) return 0;

            bool hasLowerLatin = false;
            bool hasUpperLatin = false;
            bool hasDigits = false;
            bool hasOtherAscii = false;
            bool hasNonAscii = false;

            foreach (char ch in password)
            {
                if (ch <= 127)
                {
                    if (ch >= 'a' && ch <= 'z') hasLowerLatin = true;
                    else if (ch >= 'A' && ch <= 'Z') hasUpperLatin = true;
                    else if (ch >= '0' && ch <= '9') hasDigits = true;
                    else hasOtherAscii = true;
                }
                else
                {
                    hasNonAscii = true;
                }
            }

            int size = 0;
            if (hasLowerLatin) size += 26;
            if (hasUpperLatin) size += 26;
            if (hasDigits) size += 10;
            if (hasOtherAscii) size += 33;
            if (hasNonAscii) size += 66;

            return size;
        }
        
        public double GetBits(string password)
        {
            int alphabetSize = GetAlphabetSize(password);
            if (alphabetSize == 0) return 0;
            return password.Length * Math.Log2(alphabetSize);
        }
        
        public string GetVerdict(double bits)
        {
            if (bits < 28)  return "У вас очень слабый пароль, херня короче";
            if (bits < 36)  return "У вас все еще слабый пароль, и вас докснут";
            if (bits < 60)  return "Уже лучше, но вас могут сломать";
            if (bits < 128) return "Ахуенно, брат";
            return "Вооооообще красота";
        }
    }
}