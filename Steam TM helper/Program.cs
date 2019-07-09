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
        static void Error_Text(string text) {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine(text);
            Console.ResetColor();

        }

        static void Main(string[] args)
        {
            Console.Write("Steam TM helper");

            // TASKS

            // + Сделать расчет т.е. Ввести цену за предмет и сумму на которую нужно закупится и получить кол-во предметов которые можно купить!


            double ItemPrice = 0, balance = 0;
            int ItemCount = 0;
            bool bal, IteP, IteC;

            bal = IteP = IteC = true;

            while (bal == true){
                Console.Write("Введите ваш баланс: ");

                if (!Double.TryParse(Console.ReadLine(), out balance))
                    Error_Text("Введите число в формате 1,23");     
                
                else if (balance != 0) bal = false;
                else Error_Text("Введите число > 0");
            }      

            while (IteP == true)
            {
                Console.Write("Введите цену за один предмет: ");

                if (!Double.TryParse(Console.ReadLine(), out ItemPrice))
                    Error_Text("Введите число в формате 1,23");

                else if (ItemPrice != 0) IteP = false;
                else Error_Text("Введите число > 0");
            }


            while (IteC == true)
            {
                Console.Write("Введите кол-во предметов: ");

                if (!Int32.TryParse(Console.ReadLine(), out ItemCount))
                    Error_Text("Введите целое число");

                else if (ItemCount != 0) IteC = false;
                else Error_Text("Введите число > 0");
            }

            Console.WriteLine("Ваш баланс: "+balance+ " Цена за 1 предмет: "+ItemPrice+" Кол-во предметов: "+ItemCount);

            Console.WriteLine("Цена: "+ItemPrice+" Кол-во: "+ItemCount+" Цена: "+ ItemPrice * ItemCount);

            Console.ReadKey();
        }
    }
}
