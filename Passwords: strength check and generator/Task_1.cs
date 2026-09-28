using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
class Task_1
{
    const double SpeedFast = 10_000_000_000.0;             
    const double SpeedSlow = SpeedFast / 1_000_000.0;      
    const double SecondsPerYear = 365.25 * 24 * 3600;
    const double UniverseAgeYears = 13_800_000_000.0;
    const string Lower = "abcdefghijklmnopqrstuvwxyz";
    const string Upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    const string Digits = "0123456789";
    const string Symbols = " !\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~"; 
    
    static readonly string[] Popular =
    {
        "123456", "password", "123456789", "12345678", "12345", "qwerty", "1234567",
        "111111", "123123", "abc123", "qwerty123", "1q2w3e4r", "admin", "letmein",
        "welcome", "monkey", "dragon", "iloveyou", "sunshine", "princess",
        "football", "master", "login", "passw0rd", "qazwsx", "zaq12wsx", "000000",
        "654321", "superman", "trustno1", "1234", "qwertyuiop", "password1"
    };
    static readonly string[] Words = (
        "лес река гора озеро поле луг ручей туман дождь снег ветер гроза радуга солнце луна звезда " +
        "облако камень песок глина трава цветок ромашка василёк тюльпан роза дуб берёза сосна ель клён " +
        "липа рябина ясень ольха ива яблоко груша вишня слива малина клубника черника грибы орех мёд " +
        "хлеб молоко сыр каша суп борщ пирог блин чай кофе волк лиса медведь заяц ёж белка олень лось " +
        "барсук бобр выдра сова орёл ворон сокол журавль аист лебедь утка гусь щука карась окунь сом " +
        "лодка мост дорога тропа костёр палатка рюкзак компас карта фонарь велосипед поезд самолёт " +
        "корабль книга письмо часы окно дверь камин лампа зеркало ключ"
    ).Split(' ', StringSplitOptions.RemoveEmptyEntries).Distinct().ToArray();
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("=== Парольная лаборатория ===");
            Console.WriteLine("1 - проверить пароль");
            Console.WriteLine("2 - сгенерировать пароль");
            Console.WriteLine("3 - сгенерировать парольную фразу");
            Console.WriteLine("4 - выход");
            Console.WriteLine("5 - самопроверка генератора (10 паролей)");
            Console.Write("Ваш выбор: ");
            string choice = (Console.ReadLine() ?? "").Trim();

            switch (choice)
            {
                case "1": CheckPasswordMenu(); break;
                case "2": GeneratePasswordMenu(); break;
                case "3": GeneratePassphraseMenu(); break;
                case "4": return;
                case "5": SelfTest(); break;
                default: Console.WriteLine("Нет такого пункта, введите число от 1 до 5."); break;
            }
        }
    }
    static int GetAlphabetSize(string password)
    {
        bool lower = false, upper = false, digit = false, other = false, nonAscii = false;
        foreach (char c in password)
        {
            if (c > 127) nonAscii = true;
            else if (c >= 'a' && c <= 'z') lower = true;
            else if (c >= 'A' && c <= 'Z') upper = true;
            else if (c >= '0' && c <= '9') digit = true;
            else other = true;
        }
        int size = 0;
        if (lower) size += 26;
        if (upper) size += 26;
        if (digit) size += 10;
        if (other) size += 33;
        if (nonAscii) size += 66;
        return size;
    }
    static double GetStrengthBits(int length, int alphabetSize)
    {
        if (length == 0 || alphabetSize == 0) return 0;
        return length * Math.Log2(alphabetSize);
    }
    static string GetRating(double bits)
    {
        if (bits < 28) return "очень слабый";
        if (bits < 36) return "слабый";
        if (bits < 60) return "средний";
        if (bits < 128) return "сильный";
        return "очень сильный";
    }
    static double GetAverageSeconds(double bits, double speed)
    {
        return Math.Pow(2, bits - 1) / speed;
    }
    static string Plural(long n, string one, string few, string many)
    {
        long n100 = n % 100, n10 = n % 10;
        string word;
        if (n100 >= 11 && n100 <= 14) word = many;
        else if (n10 == 1) word = one;
        else if (n10 >= 2 && n10 <= 4) word = few;
        else word = many;
        return n + " " + word;
    }
    static string FormatTime(double seconds)
    {
        double years = seconds / SecondsPerYear;
        if (double.IsInfinity(seconds) || years > UniverseAgeYears)
            return "дольше возраста Вселенной (13,8 млрд лет)";
        if (seconds < 1) return "меньше секунды";
        if (seconds < 60) return Plural((long)Math.Round(seconds), "секунда", "секунды", "секунд");
        double minutes = seconds / 60;
        if (minutes < 60) return Plural((long)Math.Round(minutes), "минута", "минуты", "минут");
        double hours = minutes / 60;
        if (hours < 24) return Plural((long)Math.Round(hours), "час", "часа", "часов");
        double days = hours / 24;
        if (days < 365.25) return Plural((long)Math.Round(days), "день", "дня", "дней");
        if (years < 1000) return Plural((long)Math.Round(years), "год", "года", "лет");
        if (years < 1e6)
            return Plural((long)Math.Round(years / 1e3), "тысяча", "тысячи", "тысяч") + " лет";
        if (years < 1e9)
            return Plural((long)Math.Round(years / 1e6), "миллион", "миллиона", "миллионов") + " лет";
        return Plural((long)Math.Round(years / 1e9), "миллиард", "миллиарда", "миллиардов") + " лет";
    }
    static bool IsPopular(string password)
    {
        return Popular.Any(p => string.Equals(p, password, StringComparison.OrdinalIgnoreCase));
    }
    static bool AllSame(string s)
    {
        return s.Length > 1 && s.All(c => c == s[0]);
    }
    static bool HasSequence(string s)
    {
        s = s.ToLowerInvariant();
        for (int i = 0; i + 3 < s.Length; i++)
        {
            bool up = true, down = true;
            for (int j = 1; j < 4; j++)
            {
                if (s[i + j] - s[i + j - 1] != 1) up = false;
                if (s[i + j] - s[i + j - 1] != -1) down = false;
            }
            if (up || down) return true;
        }
        return false;
    }
    static double AnalyzePatterns(string password, double bits, out string note)
    {
        note = "";
        int alphabet = GetAlphabetSize(password);

        if (AllSame(password))
        {
            note = "пароль состоит из одного повторяющегося символа";
            return Math.Min(bits, Math.Log2(alphabet) + Math.Log2(password.Length));
        }
        string letters = password.TrimEnd('0', '1', '2', '3', '4', '5', '6', '7', '8', '9');
        int tail = password.Length - letters.Length;
        if (tail > 0 && letters.Length > 0 && IsPopular(letters))
        {
            note = "популярный пароль с цифрами в конце";
            double patternBits = Math.Log2(Popular.Length) + tail * Math.Log2(10) + 1;
            return Math.Min(bits, patternBits);
        }
        if (HasSequence(password))
        {
            note = "в пароле есть последовательность подряд идущих символов";
            return bits / 2;
        }

        return bits;
    }
    static string GeneratePassword(int length, bool useLower, bool useUpper, bool useDigits, bool useSymbols)
    {
        var groups = new System.Collections.Generic.List<string>();
        if (useLower) groups.Add(Lower);
        if (useUpper) groups.Add(Upper);
        if (useDigits) groups.Add(Digits);
        if (useSymbols) groups.Add(Symbols);

        if (groups.Count == 0)
            throw new ArgumentException("Нужно выбрать хотя бы одну группу символов.");
        if (length < 8 || length > 64)
            throw new ArgumentException("Длина пароля должна быть от 8 до 64.");

        string all = string.Concat(groups);
        char[] result = new char[length];

        int pos = 0;
        foreach (string g in groups)
            result[pos++] = g[RandomNumberGenerator.GetInt32(g.Length)];
       
        while (pos < length)
            result[pos++] = all[RandomNumberGenerator.GetInt32(all.Length)];
        
        for (int i = result.Length - 1; i > 0; i--)
        {
            int j = RandomNumberGenerator.GetInt32(i + 1);
            (result[i], result[j]) = (result[j], result[i]);
        }
        return new string(result);
    }
    static string GeneratePassphrase(int wordCount)
    {
        string[] parts = new string[wordCount];
        for (int i = 0; i < wordCount; i++)
            parts[i] = Words[RandomNumberGenerator.GetInt32(Words.Length)];
        return string.Join("-", parts);
    }

    static void CheckPasswordMenu()
    {
        Console.Write("Введите пароль: ");
        string password = ReadMasked();
        if (password.Length == 0)
        {
            Console.WriteLine("Пустой пароль проверять нельзя.");
            return;
        }

        int alphabet = GetAlphabetSize(password);
        Console.WriteLine($"Длина: {password.Length}, размер алфавита: {alphabet}");

        if (IsPopular(password))
        {
            PrintColored("Пароль есть в списке популярных - он подбирается мгновенно.", ConsoleColor.Red);
            return;
        }

        double bits = GetStrengthBits(password.Length, alphabet);
        double adjusted = AnalyzePatterns(password, bits, out string note);
        if (note != "")
        {
            Console.WriteLine($"По формуле: {bits:F1} бит, но {note}.");
            Console.WriteLine($"Скорректированная стойкость: {adjusted:F1} бит.");
        }
        ShowReport(adjusted);
    }

    static void ShowReport(double bits)
    {
        string rating = GetRating(bits);
        Console.Write($"Стойкость: {bits:F1} бит, оценка: ");
        PrintColored(rating, RatingColor(bits));
        Console.WriteLine($"Перебор на скорости 10 млрд/с: {FormatTime(GetAverageSeconds(bits, SpeedFast))}");
        Console.WriteLine($"Перебор при медленном хешировании (10 тыс./с): {FormatTime(GetAverageSeconds(bits, SpeedSlow))}");
    }

    static void GeneratePasswordMenu()
    {
        int length = ReadInt("Длина пароля (8-64): ", 8, 64);
        bool lower = AskYesNo("Строчные латинские? (д/н): ");
        bool upper = AskYesNo("Заглавные латинские? (д/н): ");
        bool digits = AskYesNo("Цифры? (д/н): ");
        bool symbols = AskYesNo("Спецсимволы? (д/н): ");

        if (!(lower || upper || digits || symbols))
        {
            Console.WriteLine("Нужно выбрать хотя бы одну группу символов.");
            return;
        }
        int count = ReadInt("Сколько вариантов сгенерировать для выбора лучшего (1-20): ", 1, 20);
        string best = null;
        double bestBits = -1;
        for (int i = 0; i < count; i++)
        {
            string p = GeneratePassword(length, lower, upper, digits, symbols);
            double b = AnalyzePatterns(p, GetStrengthBits(p.Length, GetAlphabetSize(p)), out _);
            if (b > bestBits) { bestBits = b; best = p; }
        }
        Console.WriteLine($"Пароль: {best}");
        ShowReport(bestBits);
    }
    static void GeneratePassphraseMenu()
    {
        int n = ReadInt("Сколько слов (2-12): ", 2, 12);
        string phrase = GeneratePassphrase(n);
        double perWord = Math.Log2(Words.Length);
        double bits = n * perWord;

        Console.WriteLine($"Фраза: {phrase}");
        Console.WriteLine($"Словарь: {Words.Length} слов, {perWord:F2} бит на слово");
        ShowReport(bits);

        const double target = 71.5; // 12 случайных символов
        int mine = (int)Math.Ceiling(target / perWord);
        int diceware = (int)Math.Ceiling(target / Math.Log2(7776));
        Console.WriteLine($"Чтобы догнать {target} бита, нужно {Plural(mine, "слово", "слова", "слов")} из моего словаря " +
                          $"или {Plural(diceware, "слово", "слова", "слов")} по Diceware (7776 слов).");
    }
    static void SelfTest()
    {
        int ok = 0;
        for (int i = 1; i <= 10; i++)
        {
            string p = GeneratePassword(12, true, true, true, true);
            bool good = p.Any(c => Lower.Contains(c)) && p.Any(c => Upper.Contains(c))
                     && p.Any(c => Digits.Contains(c)) && p.Any(c => Symbols.Contains(c));
            if (good) ok++;
            Console.WriteLine($"{i,2}. {p}  {(good ? "все группы есть" : "ОШИБКА")}");
        }
        Console.WriteLine($"Успешно: {ok} из 10");
    }
    static string ReadMasked()
    {
        if (Console.IsInputRedirected) return Console.ReadLine() ?? "";
        var sb = new StringBuilder();
        while (true)
        {
            ConsoleKeyInfo key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.Enter) { Console.WriteLine(); break; }
            if (key.Key == ConsoleKey.Backspace)
            {
                if (sb.Length > 0) { sb.Length--; Console.Write("\b \b"); }
            }
            else if (!char.IsControl(key.KeyChar))
            {
                sb.Append(key.KeyChar);
                Console.Write('*');
            }
        }
        return sb.ToString();
    }
    static int ReadInt(string prompt, int min, int max)
    {
        while (true)
        {
            Console.Write(prompt);
            string s = Console.ReadLine();
            if (int.TryParse(s, out int v) && v >= min && v <= max) return v;
            Console.WriteLine($"Введите целое число от {min} до {max}.");
        }
    }
    static bool AskYesNo(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string s = (Console.ReadLine() ?? "").Trim().ToLowerInvariant();
            if (s == "д" || s == "да" || s == "y") return true;
            if (s == "н" || s == "нет" || s == "n") return false;
            Console.WriteLine("Ответьте «д» или «н».");
        }
    }
    static ConsoleColor RatingColor(double bits)
    {
        if (bits < 36) return ConsoleColor.Red;
        if (bits < 60) return ConsoleColor.Yellow;
        return ConsoleColor.Green;
    }

    static void PrintColored(string text, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(text);
        Console.ResetColor();
    }
}