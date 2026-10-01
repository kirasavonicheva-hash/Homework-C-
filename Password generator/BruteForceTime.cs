using System;

public class BruteForceTime
{
    static BruteForceTime()
    {
        double speedFast = 10000000000;      
        double speedSlow = speedFast / 1000000;  

        double secondsInYear = 365.25 * 24 * 60 * 60;
        double universeAge = 13800000000 * secondsInYear; 

        double variants8 = 100000000;            

        double variants16 = 1;
        for (int i = 0; i < 16; i++)
        {
            variants16 = variants16 * 95;
        }

        Console.WriteLine("Быстрый компьютер (10 млрд попыток/сек):");
        ShowTime("8 цифр", variants8, speedFast, universeAge);
        ShowTime("16 символов", variants16, speedFast, universeAge);

        Console.WriteLine();

        Console.WriteLine("Медленный компьютер (10 тыс. попыток/сек):");
        ShowTime("8 цифр", variants8, speedSlow, universeAge);
        ShowTime("16 символов", variants16, speedSlow, universeAge);
    }

    static void ShowTime(string name, double variants, double speed, double universeAge)
    {
        double seconds = variants / 2 / speed;

        Console.Write(name + ": ");

        if (seconds >= universeAge)
        {
            Console.WriteLine("Больше возраста Вселенной (13.8 млрд лет)");
            return;
        }

        double secondsInYear = 365.25 * 24 * 60 * 60;
        double secondsInDay = 24 * 60 * 60;
        double secondsInHour = 60 * 60;

        if (seconds >= secondsInYear * 1000000000)
        {
            Console.WriteLine((seconds / (secondsInYear * 1000000000)).ToString("F2") + "млрд лет");
        }
        else if (seconds >= secondsInYear * 1000000)
        {
            Console.WriteLine((seconds / (secondsInYear * 1000000)).ToString("F2") + "млн лет");
        }
        else if (seconds >= secondsInYear * 1000)
        {
            Console.WriteLine((seconds / (secondsInYear * 1000)).ToString("F2") + "тыс. лет");
        }
        else if (seconds >= secondsInYear)
        {
            Console.WriteLine((seconds / secondsInYear).ToString("F2") + "лет");
        }
        else if (seconds >= secondsInDay)
        {
            Console.WriteLine((seconds / secondsInDay).ToString("F2") + "дней");
        }
        else if (seconds >= secondsInHour)
        {
            Console.WriteLine((seconds / secondsInHour).ToString("F2") + "часов");
        }
        else if (seconds >= 60)
        {
            Console.WriteLine((seconds / 60).ToString("F2") + "минут");
        }
        else
        {
            Console.WriteLine(seconds.ToString("F4") + "секунд");
        }
    }
}