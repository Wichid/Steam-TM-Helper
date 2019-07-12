using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam_TM_helper
{
    class Program
    {
        static void Text_Color(string Color, string Text) {

            switch (Color) {

                case "Red":
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine("[ОШИБКА] " + Text);
                    break;

                case "Green":
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.WriteLine(Text);

                    break;

                case "Blue":
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.WriteLine(Text);
                    break;

                case "DarkRed":
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("[ОШИБКА] " + Text);
                    break;

                case "DarkYellow":
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine(Text);

                    break;

                default:
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine("[ОШИБКА] НЕПРАВЕЛЬНЫЙ ЦВЕТ");
                    Console.ResetColor();
                    break;
            }
                        
            Console.ResetColor();
        }

        static void Main(string[] args)
        {

            bool Main_Loop = true;
            while (Main_Loop == true) {
                Console.Clear();                
                
                Console.WriteLine("Steam TM helper Update v3\n");
                Console.WriteLine("Выберите режим:\n \t1. Оффлайн режим \n \t2. Онлайн режим \n \t3. Настройки \n\n \t0. Выход");

                ConsoleKey KeyChoice = Console.ReadKey(true).Key;
                switch (KeyChoice){ // Выбор режима Онлайн или Офлайн

                    case ConsoleKey.D1: // Оффлайн часть!
                        Console.WriteLine("\nВы выбрали offline режим\n");
                        Console.WriteLine("[OFFLINE] Выбирите что вы хотите сделать:\n \t1. Купить \n \t2. Расчет стоимости предметов\n \t3. Продать");
                        switch (KeyChoice = Console.ReadKey(true).Key)
                        { // Выбор Купить, Расчёт стоимости предмета, Продажа

                            case ConsoleKey.D1:

                                bool buy = true; // цикл для покупки
                                double ItemPrice = 0;
                                int ItemCount = 0;
                                bool IteP = true, IteC = true; // циклы

                                while (buy == true) // цикл покупки
                                {

                                    while (IteP == true) // пррверка
                                    {
                                        Console.Write("\n[КУПИТЬ] Введите цену за один предмет: ");

                                        if (!Double.TryParse(Console.ReadLine(), out ItemPrice))
                                            Text_Color("DarkRed", "Введите число в формате 1,23");

                                        else if (ItemPrice != 0 & ItemPrice > 0) IteP = false;
                                        else Text_Color("DarkRed", "Введите число > 0");
                                    }

                                    while (IteC == true) // пррверка
                                    {
                                        Console.Write("[КУПИТЬ] Введите кол-во предметов: ");

                                        if (!Int32.TryParse(Console.ReadLine(), out ItemCount))
                                            Text_Color("Red", "Введите целое число");

                                        else if (ItemCount != 0 & ItemCount > 0) IteC = false;
                                        else Text_Color("Red", "Введите число > 0");
                                    }
                                    double PlusResult;

                                    Console.WriteLine("\nЦена 1-го предмета: " + ItemPrice + " Кол-во предметов: " + ItemCount + " Итог: " + ItemPrice * ItemCount);
                                    Console.WriteLine("Продать в ноль за: " + Math.Round(PlusResult = ItemPrice * 0.15 + ItemPrice, 2));

                                    ConsoleKey ContinueSell;
                                    Console.WriteLine("Хотите ли вы повторить? [Y\\N]\n");
                                    switch (ContinueSell = Console.ReadKey(true).Key)
                                    {

                                        case ConsoleKey.Y:
                                            IteP = IteC = true;
                                            break;

                                        case ConsoleKey.N:
                                            buy = false;
                                            break;
                                    }

                                }
                                break; // конец цикл покупки

                            case ConsoleKey.D2:
                                Console.WriteLine("\n[РАСЧЕТ] Расчет кол-ва предметов на сколько вам хватит");

                                bool Count_Buy = true; // цикл для расчета

                                while (Count_Buy == true) // цикл расчёта
                                {

                                    double Balance = 0, Item_Price = 0, Sec_Result = 0;
                                    int Item_Count_All = 0;
                                    bool Sec_Balance = true, Sec_Price = true, Sec_Func = true; // циклы

                                    while (Sec_Price == true) // пррверка цены
                                    {
                                        Console.Write("[РАСЧЕТ] Введите цену за один предмет: ");

                                        if (!Double.TryParse(Console.ReadLine(), out Item_Price))
                                            Text_Color("DarkRed", "Введите число в формате 1,23");

                                        else if (Item_Price != 0 & Item_Price > 0) Sec_Price = false;
                                        else Text_Color("DarkRed", "Введите число > 0");
                                    }

                                    while (Sec_Balance == true) // пррверка баланса
                                    {
                                        Console.Write("[РАСЧЕТ] Введите ваш бюджет: ");

                                        if (!Double.TryParse(Console.ReadLine(), out Balance))
                                            Text_Color("DarkRed", "Введите число в формате 1,23");

                                        else if (Balance != 0 & Balance > 0) Sec_Balance = false;
                                        else Text_Color("Red", "Введите число > 0");
                                    }

                                    Console.Write("[РАСЧЕТ] Вы сможите купить себе: ");
                                    var (x, y) = (Console.CursorLeft, Console.CursorTop);
                                    while (Sec_Func == true)
                                    {

                                        if (Balance >= Item_Price)
                                        {
                                            Item_Count_All++;
                                            Balance -= Item_Price;
                                            Console.SetCursorPosition(x, y);
                                            Console.Write(Item_Count_All + " предметов");
                                            if (Balance < Item_Price)
                                            {
                                                if (Balance != 0)
                                                    Console.Write(", Остаток: " + Math.Round(Balance, 2));
                                            }
                                        }
                                        else Sec_Func = false;

                                    }

                                    ConsoleKey ContinueSell;
                                    Console.WriteLine("\nХотите ли вы повторить? [Y\\N]\n");
                                    switch (ContinueSell = Console.ReadKey(true).Key)
                                    {

                                        case ConsoleKey.Y:
                                            Sec_Price = Sec_Balance = true;
                                            break;

                                        case ConsoleKey.N:
                                            Count_Buy = false; // выход из цикла
                                            break;

                                    }
                                }
                                break; // конец расчет

                            case ConsoleKey.D3:
                                Console.WriteLine("[ПРОДАТЬ] ");

                                break;

                        }

                        break; // конец оффлайн часть

                    case ConsoleKey.D2: // ОНЛАЙН ЧАСТЬ!
                        Console.WriteLine("Вы выбрали online режим");
                        Console.WriteLine("Тут неччего нет :(");
                        // В будующем

                        break;

                    case ConsoleKey.D3:
                        // Насьройки
                        Console.WriteLine("[НАСТРОЙКИ]");
                        // Properties
                        break;

                    case ConsoleKey.D0:
                        Console.WriteLine("Закрытие bb...");
                        Main_Loop = false;
                        break;

                }

            }


            


        }
    }
}
