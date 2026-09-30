using System;
using System.Security.Cryptography;
using System.Text;
class Task_1
{
    const double SpeedFast = 10000000000.0;
    const double SpeedSlow = SpeedFast / 1000000.0;
    const double SecondsPerYear = 365.25 * 24 * 3600;
    const double UniverseAgeYears = 13800000000.0;
    const string Lower = "abcdefghijklmnopqrstuvwxyz";
    const string Upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    const string Digits = "0123456789";
    const string Symbols = " !\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~";
    static string[] Popular = {
        "123456", "password", "123456789", "12345678", "12345", "qwerty", "1234567",
        "111111", "123123", "abc123", "qwerty123", "1q2w3e4r", "admin", "letmein",
        "welcome", "monkey", "dragon", "iloveyou", "sunshine", "princess",
        "football", "master", "login", "passw0rd", "qazwsx", "zaq12wsx", "000000",
        "654321", "superman", "trustno1", "1234", "qwertyuiop", "password1"
    };
    static string[] Words = ("лес река гора озеро поле луг ручей туман дождь снег ветер гроза радуга солнце луна звезда " +
        "облако камень песок глина трава цветок ромашка василёк тюльпан роза дуб берёза сосна ель клён " +
        "липа рябина ясень ольха ива яблоко груша вишня слива малина клубника черника грибы орех мёд " +
        "хлеб молоко сыр каша суп борщ пирог блин чай кофе волк лиса медведь заяц ёж белка олень лось " +
        "барсук бобр выдра сова орёл ворон сокол журавль аист лебедь утка гусь щука карась окунь сом " +
        "лодка мост дорога тропа костёр палатка рюкзак компас карта фонарь велосипед поезд самолёт " +
        "корабль книга письмо часы окно дверь камин лампа зеркало ключ").Split(' ');
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("Парольная лаборатория");
            Console.WriteLine("1.проверить пароль");
            Console.WriteLine("2.сгенерировать пароль");
            Console.WriteLine("3.сгенерировать парольную фразу");
            Console.WriteLine("4.выход");
            Console.WriteLine("5.самопроверка генератора");
            Console.Write("Ваш выбор: ");
            string choice = Console.ReadLine();
            if (choice == null) choice = "";
            choice = choice.Trim();
            if (choice == "1")
            {
                CheckPasswordMenu();
            }
            else if (choice == "2")
            {
                GeneratePasswordMenu();
            }
            else if (choice == "3")
            {
                GeneratePassphraseMenu();
            }
            else if (choice == "4")
            {
                return;
            }
            else if (choice == "5")
            {
                SelfTest();
            }
            else
            {
                Console.WriteLine("Нет такого пункта, введите число от 1 до 5.");
            }
        }
    }
    static int GetAlphabetSize(string password)
    {
        bool lower = false;
        bool upper = false;
        bool digit = false;
        bool other = false;
        bool nonAscii = false;
        for (int i = 0; i < password.Length; i++)
        {
            char c = password[i];
            if (c > 127) nonAscii = true;
            else if (c >= 'a' && c <= 'z') lower = true;
            else if (c >= 'A' && c <= 'Z') upper = true;
            else if (c >= '0' && c <= '9') digit = true;
            else other = true;
        }
        int size = 0;
        if (lower) size = size + 26;
        if (upper) size = size + 26;
        if (digit) size = size + 10;
        if (other) size = size + 33;
        if (nonAscii) size = size + 66;
        return size;
    }
    static double GetStrengthBits(int length, int alphabetSize)
    {
        if (length == 0 || alphabetSize == 0) return 0;
        return length * (Math.Log(alphabetSize) / Math.Log(2));
    }
    static string GetRating(double bits)
    {
        if (bits < 28) return "очень слабый лошпед переделывай.";
        if (bits < 36) return "слабый ты все еще нищий, тупой и т.д.";
        if (bits < 60) return "средний, ну уже лучше но все равно на дне.";
        if (bits < 128) return "сильный, ну ты прям луксмексер";
        return "очень сильный, НО тебя все равно МОГГНУТ!";
    }
    static double GetAverageSeconds(double bits, double speed)
    {
        return Math.Pow(2, bits - 1) / speed;
    }
    static string Plural(long n, string one, string few, string many)
    {
        long n100 = n % 100;
        long n10 = n % 10;
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
            return "дольше возраста ВСЕЛЕННОЙ (13,8 млрд лет)";
        if (seconds < 1) return "меньше секунды";
        if (seconds < 60) return Plural((long)Math.Round(seconds), "секунда", "секунды", "секунд");
        double minutes = seconds / 60;
        if (minutes < 60) return Plural((long)Math.Round(minutes), "минута", "минуты", "минут");
        double hours = minutes / 60;
        if (hours < 24) return Plural((long)Math.Round(hours), "час", "часа", "часов");
        double days = hours / 24;
        if (days < 365.25) return Plural((long)Math.Round(days), "день", "дня", "дней");
        if (years < 1000) return Plural((long)Math.Round(years), "год", "года", "лет");
        if (years < 1000000)
            return Plural((long)Math.Round(years / 1000), "тысяча", "тысячи", "тысяч") + " лет";
        if (years < 1000000000)
            return Plural((long)Math.Round(years / 1000000), "миллион", "миллиона", "миллионов") + " лет";
        return Plural((long)Math.Round(years / 1000000000), "миллиард", "миллиарда", "миллиардов") + " лет";
    }
    static bool IsPopular(string password)
    {
        for (int i = 0; i < Popular.Length; i++)
        {
            if (string.Equals(Popular[i], password, StringComparison.OrdinalIgnoreCase))
                return true;
        }
        return false;
    }
    static bool AllSame(string s)
    {
        if (s.Length <= 1) return false;
        char first = s[0];
        for (int i = 1; i < s.Length; i++)
        {
            if (s[i] != first) return false;
        }
        return true;
    }
    static bool HasSequence(string s)
    {
        s = s.ToLower();
        for (int i = 0; i + 3 < s.Length; i++)
        {
            bool up = true;
            bool down = true;
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
            double newBits = Math.Log(alphabet) / Math.Log(2) + Math.Log(password.Length) / Math.Log(2);
            if (newBits < bits) return newBits;
            else return bits;
        }
        string letters = password.TrimEnd('0', '1', '2', '3', '4', '5', '6', '7', '8', '9');
        int tail = password.Length - letters.Length;
        if (tail > 0 && letters.Length > 0 && IsPopular(letters))
        {
            note = "популярный пароль с цифрами в конце";
            double patternBits = Math.Log(Popular.Length) / Math.Log(2) + tail * (Math.Log(10) / Math.Log(2)) + 1;
            if (patternBits < bits) return patternBits;
            else return bits;
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
        string[] groups = new string[4];
        int groupCount = 0;
        if (useLower) { groups[groupCount] = Lower; groupCount++; }
        if (useUpper) { groups[groupCount] = Upper; groupCount++; }
        if (useDigits) { groups[groupCount] = Digits; groupCount++; }
        if (useSymbols) { groups[groupCount] = Symbols; groupCount++; }
        if (groupCount == 0)
            throw new Exception("Нужно выбрать хотя бы одну группу символов.");
        if (length < 8 || length > 64)
            throw new Exception("Длина пароля должна быть от 8 до 64.");
        string all = "";
        for (int i = 0; i < groupCount; i++)
        {
            all = all + groups[i];
        }
        char[] result = new char[length];
        int pos = 0;
        for (int i = 0; i < groupCount; i++)
        {
            string g = groups[i];
            int index = RandomNumberGenerator.GetInt32(g.Length);
            result[pos] = g[index];
            pos++;
        }
        while (pos < length)
        {
            int index = RandomNumberGenerator.GetInt32(all.Length);
            result[pos] = all[index];
            pos++;
        }
        for (int i = result.Length - 1; i > 0; i--)
        {
            int j = RandomNumberGenerator.GetInt32(i + 1);
            char temp = result[i]; 
            result[i] = result[j]; 
            result[j] = temp;
        }
        return new string(result);
    }
    static string GeneratePassphrase(int wordCount)
    {
        string[] parts = new string[wordCount];
        for (int i = 0; i < wordCount; i++)
        {
            int index = RandomNumberGenerator.GetInt32(Words.Length);
            parts[i] = Words[index];
        }
        return string.Join("-", parts);
    }
    static void CheckPasswordMenu()
    {
        Console.Write("Введи пароль бестолочь: ");
        string password = ReadMasked();
        if (password.Length == 0)
        {
            Console.WriteLine("Пустой пароль проверять нельзя, понял? Имбицил.");
            return;
        }
        int alphabet = GetAlphabetSize(password);
        Console.WriteLine("Длина: " + password.Length + ", размер алфавита: " + alphabet);
        if (IsPopular(password))
        {
            PrintColored("Пароль есть в списке популярных - он подбирается мгновенно.", ConsoleColor.Red);
            return;
        }
        double bits = GetStrengthBits(password.Length, alphabet);
        string note;
        double adjusted = AnalyzePatterns(password, bits, out note);
        if (note != "")
        {
            Console.WriteLine("По формуле: " + bits.ToString("F1") + " бит, но " + note + ".");
            Console.WriteLine("Скорректированная стойкость: " + adjusted.ToString("F1") + " бит.");
        }

        ShowReport(adjusted);
    }
    static void ShowReport(double bits)
    {
        string rating = GetRating(bits);
        Console.Write("Стойкость: " + bits.ToString("F1") + " бит, оценка: ");
        PrintColored(rating, RatingColor(bits));
        Console.WriteLine("Перебор на скорости 10 млрд/с: " + FormatTime(GetAverageSeconds(bits, SpeedFast)));
        Console.WriteLine("Перебор при медленном хешировании (10 тыс./с): " + FormatTime(GetAverageSeconds(bits, SpeedSlow)));
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
            string note;
            double b = AnalyzePatterns(p, GetStrengthBits(p.Length, GetAlphabetSize(p)), out note);
            if (b > bestBits)
            {
                bestBits = b;
                best = p;
            }
        }

        Console.WriteLine("Пароль_67: " + best);
        ShowReport(bestBits);
    }
    static void GeneratePassphraseMenu()
    {
        int n = ReadInt("Сколько слов (2-12): ", 2, 12);
        string phrase = GeneratePassphrase(n);
        double perWord = Math.Log(Words.Length) / Math.Log(2);
        double bits = n * perWord;

        Console.WriteLine("Фраза: " + phrase);
        Console.WriteLine("Словарь: " + Words.Length + " слов, " + perWord.ToString("F2") + " бит на слово");
        ShowReport(bits);

        double target = 71.5;
        int mine = (int)Math.Ceiling(target / perWord);
        int diceware = (int)Math.Ceiling(target / (Math.Log(7776) / Math.Log(2)));
        Console.WriteLine("Чтобы догнать " + target + " бита, нужно " + Plural(mine, "слово", "слова", "слов") + " из моего словаря " +
                          "или " + Plural(diceware, "слово", "слова", "слов") + " по Diceware (7776 слов).");
    }

    static void SelfTest()
    {
        int ok = 0;
        for (int i = 1; i <= 10; i++)
        {
            string p = GeneratePassword(12, true, true, true, true);
            bool hasLower = false;
            bool hasUpper = false;
            bool hasDigit = false;
            bool hasSymbol = false;

            for (int j = 0; j < p.Length; j++)
            {
                char c = p[j];
                if (Lower.IndexOf(c) >= 0) hasLower = true;
                if (Upper.IndexOf(c) >= 0) hasUpper = true;
                if (Digits.IndexOf(c) >= 0) hasDigit = true;
                if (Symbols.IndexOf(c) >= 0) hasSymbol = true;
            }

            bool good = hasLower && hasUpper && hasDigit && hasSymbol;
            if (good) ok++;
            Console.WriteLine(i + ". " + p + "  " + (good ? "все группы есть" : "ОШИБКАААААААА"));
        }
        Console.WriteLine("Успешно: " + ok + " из 10");
    }
    static string ReadMasked()
    {
        if (Console.IsInputRedirected) return Console.ReadLine() ?? "";

        StringBuilder sb = new StringBuilder();
        while (true)
        {
            ConsoleKeyInfo key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.Enter)
            {
                Console.WriteLine();
                break;
            }
            if (key.Key == ConsoleKey.Backspace)
            {
                if (sb.Length > 0)
                {
                    sb.Length--;
                    Console.Write("\b \b");
                }
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
            int v;
            if (int.TryParse(s, out v) && v >= min && v <= max) return v;
            Console.WriteLine("Введите целое число от " + min + " до " + max + ".");
        }
    }
    static bool AskYesNo(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string s = Console.ReadLine();
            if (s == null) s = "";
            s = s.Trim().ToLower();
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