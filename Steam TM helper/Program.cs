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
                    break;

                case "Green":
                    Console.BackgroundColor = ConsoleColor.Green;
                    break;

                case "Blue":
                    Console.BackgroundColor = ConsoleColor.Blue;
                    break;

                case "DarkRed":
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    break;

                case "DarkYellow":
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    break;


                default:
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine("ОШИБКА СМЕНЫ ТЕКСТА");
                    Console.ResetColor();
                    break;
            }

            
            Console.WriteLine(Text);
            Console.ResetColor();
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Steam TM helper Update v2\n");            

             // buy
            bool buy = true;
            double ItemPrice = 0, balance = 0;
            int ItemCount = 0;
            bool bal, IteP, IteC;

            bal = IteP = IteC = true;
            // buy \


            Console.WriteLine("Выберите режим:\n \t1. Оффлайн режим \n \t2. Онлайн режим");

            ConsoleKey KeyChoice = Console.ReadKey(true).Key;
            
            switch (KeyChoice){ // Выбор режима Онлайн или Офлайн

                case ConsoleKey.D1: // Оффлайн часть!
                    Console.WriteLine("\nВы выбрали offline режим\n");
                    Console.WriteLine("[OFFLINE] Выбирите что вы хотите сделать:\n \t1. Купить \n \t2. Расчет стоимости предметов\n \t3. Продать");
                    switch (KeyChoice = Console.ReadKey(true).Key) { // Выбор Купить, Расчёт стоимости предмета, Продажа

                        case ConsoleKey.D1:
                            Console.WriteLine("[КУПИТЬ] "); // ОФОРМЛЕНИЕ!!!

                            while (buy == true) // цикл покупки
                            {

                                while (IteP == true) // пррверка
                                {
                                    Console.Write("Введите цену за один предмет: ");

                                    if (!Double.TryParse(Console.ReadLine(), out ItemPrice))
                                        Text_Color("DarkRed", "Введите число в формате 1,23");

                                    else if (ItemPrice != 0 & ItemPrice > 0) IteP = false;
                                    else Text_Color("DarkRed", "Введите число > 0");
                                }


                                while (IteC == true) // пррверка
                                {
                                    Console.Write("Введите кол-во предметов: ");

                                    if (!Int32.TryParse(Console.ReadLine(), out ItemCount))
                                        Text_Color("Red", "Введите целое число");

                                    else if (ItemCount != 0 & ItemCount > 0) IteC = false;
                                    else Text_Color("Red", "Введите число > 0");
                                }
                                double PlusResult;

                                Console.WriteLine("\nВведенные данные: Стоимость 1-го предмета: "+ ItemPrice +" Кол-во предметов: " + ItemCount);
                                Console.WriteLine("Цена 1-го предмета: " + ItemPrice + " Кол-во предметов: " + ItemCount + " Итог: " + ItemPrice * ItemCount);
                                Console.WriteLine("Для выхода в плюс лучше продать за "+ Math.Round(PlusResult = ItemPrice * 0.15 + ItemPrice, 2));

                                ConsoleKey ContinueSell;
                                Console.WriteLine("Хотите ли вы повторить? [Y\\N]");
                                switch (ContinueSell = Console.ReadKey(true).Key){

                                    case ConsoleKey.Y:
                                        IteP = IteC = true;
                                        break;

                                    case ConsoleKey.N:
                                        buy = false;
                                        break;

                                }

                            }
                            break;


                           

                        case ConsoleKey.D2:
                            Console.WriteLine("[РАСЧЕТ] ");
                            Console.WriteLine("Хотите ли вы расщитать количество предметов которое сможите купить за количемтво денег?");


                            /*
                            * Console.Write("Ваше число: ");
                            * var (x, y) = (Console.CursorLeft,Console.CursorTop);
                            * for (int i = 0 ; i <= MaxBalance; i++){
                            *     Console.SetCursorPosition(x, y);
                            *     Console.Write(i);
                            * }
                            */

                            break;

                        case ConsoleKey.D3:
                            Console.WriteLine("[ПРОДАТЬ] ");



                            break;

                    }

                    break;

                case ConsoleKey.D2: // ОНЛАЙН ЧАСТЬ!
                    Console.WriteLine("Вы выбрали online режим");
                    Console.WriteLine("Тут неччего нет :(");
                    // В будующем

                    break;

            }


            Console.ReadKey();
        }
    }
}
