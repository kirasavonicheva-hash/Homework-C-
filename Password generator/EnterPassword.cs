using System;
using System.Text;

namespace Task1
{
    public class EnterPassword
    {
        public readonly RobustnessInBits _robustness = new RobustnessInBits();

        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            var program = new EnterPassword();
            program.Run();
        }

        public void Run()
        {
            Console.Write("Введи пароль и не будь лапшой: ");
            string password = Console.ReadLine() ?? "";

            if (string.IsNullOrEmpty(password))
            {
                Console.WriteLine("Пароль пустой, нечего анализировать.");
                return;
            }

            int alphabetSize = _robustness.GetAlphabetSize(password);
            double bits = _robustness.GetBits(password);
            string verdict = _robustness.GetVerdict(bits);

            Console.WriteLine($"Длина пароля: {password.Length}");
            Console.WriteLine($"Размер алфавита: {alphabetSize}");
            Console.WriteLine($"Стойкость: {bits:F2} бит");
            Console.WriteLine($"Вердикт: {verdict}");
        }
    }
}