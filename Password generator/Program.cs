using System.Security.Cryptography;
using System.Text;

class PasswordApp
{
    private List<string> popularPasswords = new List<string>()
    {
        "123456", "password", "123456789", "12345678", "12345",
        "qwerty", "abc123", "football", "monkey", "letmein",
        "dragon", "111111", "baseball", "iloveyou", "trustno1",
        "sunshine", "master", "welcome", "shadow", "superman",
        "qwerty123", "password1", "admin", "login", "passw0rd"
    };

    private List<string> phraseWords = new List<string>()
    {
        "звезда", "планета", "комета", "галактика", "туманность",
        "ракета", "спутник", "орбита", "космос", "астронавт",
        "луна", "солнце", "марс", "венера", "юпитер",
        "сатурн", "уран", "нептун", "меркурий", "плутон",
        "метеор", "астероид", "телескоп", "обсерватория", "невесомость",
        "скафандр", "модуль", "станция", "шаттл", "зонд",
        "пыль", "лёд", "газ", "ядро", "крона",
        "вспышка", "затмение", "парад", "созвездие", "пояс",
        "млечный", "андромеда", "сириус", "полярная", "вега",
        "альдебаран", "бетельгейзе", "ригель", "капелла", "арктур",
        "фотон", "кварк", "нейтрон", "протон", "электрон",
        "гравитация", "энергия", "масса", "скорость", "свет",
        "тьма", "вакуум", "плазма", "излучение", "спектр",
        "кратер", "каньон", "равнина", "гора", "вулкан",
        "море", "океан", "камень", "металл", "керамика",
        "топливо", "двигатель", "сопло", "антенна", "датчик",
        "камера", "компьютер", "программа", "сигнал", "частота",
        "волна", "импульс", "код", "данные", "файл",
        "сервер", "сеть", "канал", "старт", "финиш",
        "полёт", "стыковка", "шлюз", "отсек", "панель"
    };

    public int GetAlphabetSize(string password)
    {
        bool lower = false, upper = false, digit = false;
        bool otherAscii = false, nonAscii = false;

        foreach (char ch in password)
        {
            if (ch >= 'a' && ch <= 'z') lower = true;
            else if (ch >= 'A' && ch <= 'Z') upper = true;
            else if (ch >= '0' && ch <= '9') digit = true;
            else if (ch < 128) otherAscii = true;
            else nonAscii = true;
        }

        int size = 0;
        if (lower) size += 26;
        if (upper) size += 26;
        if (digit) size += 10;
        if (otherAscii) size += 33;
        if (nonAscii) size += 66;

        return size;
    }

    public double GetStrengthBits(string password)
    {
        int alphabet = GetAlphabetSize(password);
        if (alphabet == 0) return 0;
        return password.Length * Math.Log(alphabet, 2);
    }

    public string GetStrengthText(double bits)
    {
        if (bits < 28) return "очень слабый";
        if (bits < 36) return "слабый";
        if (bits < 60) return "средний";
        if (bits < 128) return "сильный";
        return "очень сильный";
    }

    public double GetCrackTimeSeconds(string password, double speed = 10_000_000_000.0)
    {
        int alphabet = GetAlphabetSize(password);
        if (alphabet == 0) return 0;
        double combinations = Math.Pow(alphabet, password.Length);
        return combinations / (2 * speed);
    }

    public string FormatTime(double seconds)
    {
        if (seconds < 1) return "меньше секунды";
        if (seconds < 60) return $"{seconds:F1} секунд";

        double minutes = seconds / 60;
        if (minutes < 60) return $"{minutes:F1} минут";

        double hours = minutes / 60;
        if (hours < 24) return $"{hours:F1} часов";

        double days = hours / 24;
        if (days < 365) return $"{days:F1} дней";

        double years = days / 365;
        if (years < 1000) return $"{years:F1} лет";
        if (years < 1_000_000) return $"{years / 1000:F1} тысяч лет";
        if (years < 1_000_000_000) return $"{years / 1_000_000:F1} миллионов лет";

        double billions = years / 1_000_000_000;
        if (billions < 13.8) return $"{billions:F1} миллиардов лет";
        return "дольше возраста Вселенной (13,8 млрд лет)";
    }

    public bool IsInDictionary(string password)
    {
        string p = password.ToLower();
        foreach (string word in popularPasswords)
        {
            if (p == word.ToLower()) return true;
        }
        return false;
    }

    public bool IsSimplePattern(string password)
    {
        string p = password.ToLower();

        bool sameChar = true;
        for (int i = 1; i < p.Length; i++)
        {
            if (p[i] != p[0]) { sameChar = false; break; }
        }
        if (sameChar && p.Length > 0) return true;

        foreach (string word in popularPasswords)
        {
            string w = word.ToLower();
            if (p.StartsWith(w))
            {
                string rest = p.Substring(w.Length);
                if (rest.Length > 0)
                {
                    bool allDigits = true;
                    foreach (char c in rest)
                    {
                        if (!char.IsDigit(c)) { allDigits = false; break; }
                    }
                    if (allDigits) return true;
                }
            }
        }

        string[] sequences = { "12345", "23456", "34567", "45678", "56789",
                               "abcde", "bcdef", "cdefg", "defgh", "efghi" };
        foreach (string seq in sequences)
        {
            if (p.Contains(seq)) return true;
        }

        return false;
    }

    public string GeneratePassword(int length, bool useLower, bool useUpper, bool useDigits, bool useOther)
    {
        List<string> groups = new List<string>();
        if (useLower) groups.Add("abcdefghijklmnopqrstuvwxyz");
        if (useUpper) groups.Add("ABCDEFGHIJKLMNOPQRSTUVWXYZ");
        if (useDigits) groups.Add("0123456789");
        if (useOther) groups.Add("!@#$%^&*()-_=+[]{};:,.<>?/");

        if (groups.Count == 0) return null;
        if (length < 8 || length > 64) return null;
        
        List<char> chars = new List<char>();
        foreach (string g in groups)
        {
            chars.Add(g[GetRandomInt(g.Length)]);
        }

        string allChars = string.Join("", groups);

        while (chars.Count < length)
        {
            chars.Add(allChars[GetRandomInt(allChars.Length)]);
        }

        for (int i = chars.Count - 1; i > 0; i--)
        {
            int j = GetRandomInt(i + 1);
            char tmp = chars[i];
            chars[i] = chars[j];
            chars[j] = tmp;
        }

        return new string(chars.ToArray());
    }

    private int GetRandomInt(int max)
    {
        byte[] bytes = new byte[4];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(bytes);
        }
        int value = BitConverter.ToInt32(bytes, 0);
        if (value < 0) value = -value;
        return value % max;
    }

    public string GeneratePhrase(int wordCount)
    {
        List<string> words = new List<string>();
        for (int i = 0; i < wordCount; i++)
        {
            words.Add(phraseWords[GetRandomInt(phraseWords.Count)]);
        }
        return string.Join("-", words);
    }

    public double GetPhraseBits(int wordCount)
    {
        return wordCount * Math.Log(phraseWords.Count, 2);
    }

    public void CheckPasswordMenu()
    {
        Console.Write("Введите пароль: ");
        string password = Console.ReadLine();

        if (IsInDictionary(password))
        {
            Console.WriteLine("Пароль найден в словаре популярных паролей — подбирается мгновенно!");
            return;
        }

        double bits = GetStrengthBits(password);
        string text = GetStrengthText(bits);

        if (IsSimplePattern(password))
        {
            Console.WriteLine("Обнаружен простой шаблон — оценка понижена.");
            text = "слабый (шаблон)";
        }

        double seconds = GetCrackTimeSeconds(password);
        Console.WriteLine($"Длина пароля: {password.Length}");
        Console.WriteLine($"Размер алфавита: {GetAlphabetSize(password)}");
        Console.WriteLine($"Стойкость: {bits:F1} бит");
        Console.WriteLine($"Оценка: {text}");
        Console.WriteLine($"Время перебора: {FormatTime(seconds)}");
    }

    public void GeneratePasswordMenu()
    {
        Console.Write("Длина пароля (8–64): ");
        int length;
        if (!int.TryParse(Console.ReadLine(), out length))
        {
            Console.WriteLine("Ошибка: введите число.");
            return;
        }

        if (length < 8 || length > 64)
        {
            Console.WriteLine("Ошибка: длина должна быть от 8 до 64.");
            return;
        }

        Console.Write("Использовать строчные? (y/n): ");
        bool useLower = Console.ReadLine().ToLower() == "y";
        Console.Write("Использовать заглавные? (y/n): ");
        bool useUpper = Console.ReadLine().ToLower() == "y";
        Console.Write("Использовать цифры? (y/n): ");
        bool useDigits = Console.ReadLine().ToLower() == "y";
        Console.Write("Использовать прочие символы? (y/n): ");
        bool useOther = Console.ReadLine().ToLower() == "y";

        string password = GeneratePassword(length, useLower, useUpper, useDigits, useOther);
        if (password == null)
        {
            Console.WriteLine("Не выбрано ни одной группы символов.");
            return;
        }

        Console.WriteLine($"Сгенерированный пароль: {password}");
        double bits = GetStrengthBits(password);
        Console.WriteLine($"Стойкость: {bits:F1} бит ({GetStrengthText(bits)})");
        double seconds = GetCrackTimeSeconds(password);
        Console.WriteLine($"Время перебора: {FormatTime(seconds)}");
    }

    public void GeneratePhraseMenu()
    {
        Console.Write("Сколько слов в фразе? ");
        int count;
        if (!int.TryParse(Console.ReadLine(), out count))
        {
            Console.WriteLine("Ошибка, введите число.");
            return;
        }
        if (count < 1)
        {
            Console.WriteLine("Ошибка: нужно хотя бы одно слово.");
            return;
        }

        string phrase = GeneratePhrase(count);
        double bits = GetPhraseBits(count);
        Console.WriteLine($"Фраза: {phrase}");
        Console.WriteLine($"Стойкость: {bits:F1} бит");
    }

    public void Run()
    {
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("Мэню");
            Console.WriteLine("1.Проверить пароль");
            Console.WriteLine("2.Сгенерировать пароль");
            Console.WriteLine("3.Сгенерировать фразу");
            Console.WriteLine("4.Выход");
            Console.Write("Ваш выбор:");
            string choice = Console.ReadLine();

            if (choice == "1") CheckPasswordMenu();
            else if (choice == "2") GeneratePasswordMenu();
            else if (choice == "3") GeneratePhraseMenu();
            else if (choice == "4")
            {
                Console.WriteLine("Гудбай");
                break;
            }
            else
            {
                Console.WriteLine("Неверный ввод, назад.");
            }
        }
    }

    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        PasswordApp app = new PasswordApp();
        app.Run();
    }
}