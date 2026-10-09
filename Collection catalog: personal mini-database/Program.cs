namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, bool> books = new Dictionary<string, bool>();
            books.Add("\"The Taming of the Shrew,\" or The Night That Changed a Life", false);
            books.Add("Hedgehogs in the Fog", false);
            books.Add("A whale in the ocean", false);
            
            const int Pages1 = 69;
            const int Pages2 = 52;
            const int Pages3 = 67;

            const double Rating1 = 4.7;
            const double Rating2 = 3.2;
            const double Rating3 = 4.3;
            
            bool isRead = true;
            
            var bookNamesList = books.Keys.ToList();

            if (isRead)
            {
                Console.WriteLine($"Книга 1: {bookNamesList[0]}, Страниц: {Pages1}, Рейтинг: {Rating1}");
                Console.WriteLine($"Книга 2: {bookNamesList[1]}, Страниц: {Pages2}, Рейтинг: {Rating2}");
                Console.WriteLine($"Книга 3: {bookNamesList[2]}, Страниц: {Pages3}, Рейтинг: {Rating3}");
            }
            
            while (true)
            { 
               Console.WriteLine();
               Console.WriteLine("Меню:");
               Console.WriteLine("1.Выставить на панель все книги.");
               Console.WriteLine("2.Найти книгу с самым лучшим рейтингом.");
               Console.WriteLine("3.Показать заюзанные книги.");
               Console.WriteLine("4.Отметить как прочитанное/непрочитанное.");
               Console.WriteLine("5.Добавить свою книгу");
               Console.WriteLine("6.Ливнуть");
               Console.WriteLine();
               Console.Write("Выбери пункт:");
               
               string menuChoice = Console.ReadLine();

               switch(menuChoice)
               {
                   case "1": 
                       Console.WriteLine("Все книги в библэотеке:");
                       foreach (var book in books.Keys)
                       {
                           Console.WriteLine($"- {book}");
                       }
                       break;
                   
                   case "2":
                       Console.WriteLine($"Книга с самым лучшим рейтингом: {Rating1}, Название: {bookNamesList[0]}");
                       break;
                   
                   case "3":
                       Console.WriteLine("Заюзанные книги:");
                       bool anyRead = false;
                       
                       foreach (var book in books)
                       {
                           if (book.Value) 
                           {
                               Console.WriteLine($"- {book.Key}");
                               anyRead = true;
                           }
                       }
                       if (!anyRead)
                       {
                           Console.WriteLine("Сорян, у вас пока нет прочитанных книг.");
                       }
                       break;
                       
                   case "4":
                       Console.WriteLine("Выбери книги, которые отметить как прочитанные/непрочитанные:");
                       
                       var currentBooks = books.Keys.ToList();
                       
                       for (int i = 0; i < currentBooks.Count; i++)
                       {
                           string status = books[currentBooks[i]] ? "[Прочитана]" : "[Не прочитана]";
                           Console.WriteLine($"{i + 1}. {currentBooks[i]} {status}");
                       }
                       Console.Write("\nВведите номер книги для переключения блатного статуса: ");
                       if (int.TryParse(Console.ReadLine(), out int bookChoice) && bookChoice > 0 && bookChoice <= currentBooks.Count)
                       {
                           string selectedBook = currentBooks[bookChoice - 1];
                           
                           books[selectedBook] = !books[selectedBook]; 
        
                           string newStatus = books[selectedBook] ? "прочитанной" : "непрочитанной";
                           Console.WriteLine($"Статус книги \"{selectedBook}\" изменен на: {newStatus}!");
                       }
                       else
                       {
                           Console.WriteLine("Неверный ввод. Переключение отменено.");
                       }
                       break;
                   
                   case "5":
                       Console.WriteLine("Рождение новой книги!");
                       Console.Write("Введите название книги:");
                       string title = Console.ReadLine();
                       
                       string fullBookName = $"\"{title}\""; 
                       
                       books.Add(fullBookName, false); 
    
                       Console.WriteLine("Книга успешно добавлена!");
                       break;

                   case "6":
                       Console.WriteLine("Пока!");
                       return; 

                   default:
                       Console.WriteLine("Косой,выбран неверный пункт меню, попробуй еще раз.");
                       break;
               }
            } 
        }
    }
}