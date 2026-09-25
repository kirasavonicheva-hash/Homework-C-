using System;
using System.Text;
using System.Globalization;
public class Program 
{ 
    public static void Main(string[] args) 
    { 
        
        Console.Write("Введите ваше имя: ");
        string name = Console.ReadLine(); 
        Console.Write($"Привет, {name}!");
        
        Console.Write("Введите размер в байтах: ");
        string input = Console.ReadLine();

        if (!long.TryParse(input, out long sizeBytes) || sizeBytes < 0)
        {
            Console.WriteLine("Ошибка: введено не число или отрицательное значение.");
            return;
        }

        Console.WriteLine("Десятичные (1000):");
            Console.WriteLine($"Килобайты: {sizeBytes / 1000.0:F2}");
            Console.WriteLine($"Мегабайты: {sizeBytes / 1_000_000.0:F2}");
            Console.WriteLine($"Гигабайты: {sizeBytes / 1_000_000_000.0:F2}");

            Console.WriteLine();
            
            Console.WriteLine("Двоичные (1024):");
            Console.WriteLine($"Килобайты: {sizeBytes / 1024.0:F2}");
            Console.WriteLine($"Мегабайты: {sizeBytes / 1_048_576.0:F2}");
            Console.WriteLine($"Гигабайты: {sizeBytes / 1_073_741_824.0:F2}");
            
            Console.WriteLine();
            
            Console.Write("Введите объём накопителя, указанный на коробке (в ГБ): ");
            string inputDisk = Console.ReadLine();
            inputDisk = inputDisk.Replace(',', '.');

            if (!double.TryParse(inputDisk, NumberStyles.Any, CultureInfo.InvariantCulture, out double advertisedGB)
                || advertisedGB <= 0)
            {
                Console.WriteLine("Ошибка: введено не число или не положительное значение.");
                return;
            }

            const double BYTES_PER_GB_DEC = 1_000_000_000.0;
            const double BYTES_PER_GB_BIN = 1_073_741_824.0;

            double totalBytes = advertisedGB * BYTES_PER_GB_DEC;
            double windowsGB = totalBytes / BYTES_PER_GB_BIN;
            double lostGB = advertisedGB - windowsGB;
            double lostPercent = lostGB / advertisedGB * 100.0;
            
            Console.WriteLine($"На коробке: {advertisedGB:F2} ГБ");
            Console.WriteLine($"Windows покажет: {windowsGB:F2} ГБ");
            Console.WriteLine($"Разница: {lostGB:F2} ГБ ({lostPercent:F2} %)");
            Console.WriteLine("Ничего не пропало: 1 ГБ у производителя = 1000 МБ,");
            Console.WriteLine("а Windows считает 1 ГБ = 1024 МБ.");
            
            long photoSizeBytes = 4L * 1024 * 1024;
            long totalBytesLong = (long)totalBytes;

            long photoCount = totalBytesLong / photoSizeBytes;
            long remainderBytes = totalBytesLong % photoSizeBytes;
            double remainderMB  = remainderBytes / (1024.0 * 1024.0);

            Console.WriteLine($"Фото по 4 МБ поместится: {photoCount} шт.");
            Console.WriteLine($"Останется места: {remainderBytes} байт ≈ {remainderMB:F2} МБ");
            
            Console.Write("Введите размер файла в гигабайтах: ");
            string sizeInput = Console.ReadLine().Replace(',', '.');

            if (!double.TryParse(sizeInput, NumberStyles.Any, CultureInfo.InvariantCulture, out double fileGB)
                || fileGB <= 0)
            {
                Console.WriteLine("Ошибка: размер должен быть положительным числом.");
                return;
            }
        
            Console.Write("Введите скорость тарифа в Мбит/с: ");
            string speedInput = Console.ReadLine().Replace(',', '.');

            if (!double.TryParse(speedInput, NumberStyles.Any, CultureInfo.InvariantCulture, out double speedMbit)
                || speedMbit <= 0)
            {
                Console.WriteLine("Ошибка: скорость должна быть положительным числом.");
                return;
            }
            double speedMBps = speedMbit / 8.0;
            double fileMB = fileGB * 1024.0;
            double secondsExact = fileMB / speedMBps;
            long totalSeconds = (long)Math.Ceiling(secondsExact);
            
            long hours = totalSeconds / 3600;
            long minutes = (totalSeconds % 3600) / 60;
            long seconds = totalSeconds % 60;

            Console.WriteLine();
            Console.WriteLine($"Скорость: {speedMBps:F2} МБ/с");
            Console.WriteLine($"Размер файла: {fileMB:F2} МБ ({fileGB:F2} ГБ)");
            Console.WriteLine($"Точное время: {secondsExact:F2} сек");
            Console.WriteLine($"Время скачивания: {hours} ч {minutes} мин {seconds} сек");
            
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;
            
            Console.WriteLine("Введите строку: ");
            string str = Console.ReadLine();
            
            int charCount = str.Length;
            int byteCount = Encoding.UTF8.GetByteCount(input);

            int russianLetters = 0;
            int latinLetters = 0;
            int digits = 0;
            int spaces = 0;
            int punctuation = 0;
            int others = 0;

            foreach (char c in str) 
            { 
                if (char.IsDigit(c)) 
                { 
                    digits++; 
                } 
                else if (char.IsWhiteSpace(c)) 
                { 
                    spaces++; 
                } 
                else if (char.IsPunctuation(c) || char.IsSymbol(c)) 
                { 
                    punctuation++; 
                } 
                else if (char.IsLetter(c)) 
                { 
                    if ((c >= 'А' && c <= 'я') || c == 'Ё' || c == 'ё') 
                    { 
                        russianLetters++; 
                    } 
                    else if ((c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z')) 
                    { 
                        latinLetters++; 
                    } 
                    else 
                    { 
                        others++; 
                    } 
                } 
                else 
                { 
                    others++; 
                } 
            }

            Console.WriteLine($"Количество символов: {charCount}");
            Console.WriteLine($"Количество байт в UTF-8: {byteCount}");
            Console.WriteLine($"Русских букв: {russianLetters}");
            Console.WriteLine($"Латинских букв: {latinLetters}");
            Console.WriteLine($"Цифр: {digits}");
            Console.WriteLine($"Пробелов: {spaces}");
            Console.WriteLine($"Знаков препинания/символов: {punctuation}");
            Console.WriteLine($"Прочих символов: {others}");
            
            Console.Write("(❤ ω ❤)Введите объем накопителя в битах:");
            double storageSizeInBits = double.Parse(Console.ReadLine());
            
            Console.Write("ლ(╹◡╹ლ)Введите битрейт видео (в битах в секунду):");
            double bitrateInBitsPerSecond = double.Parse(Console.ReadLine());
            
            double time = storageSizeInBits / bitrateInBitsPerSecond;
            
            Console.WriteLine($"Время: {time}");
    }
}