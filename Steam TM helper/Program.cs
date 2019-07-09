using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam_TM_helper
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Steam TM helper");

            // = buy
            // + сделать Double.TryParce
            // + Сделать расчет т.е. Ввести цену за предмет и сумму на которую нужно закупится и получить кол-во предметов которые можно купить!


            double price, balance;
            int ItemCount;

            Console.Write("Введите ваш баланс: ");
            balance = Convert.ToDouble(Console.ReadLine());// tryparce
            

            Console.Write("Введите цену за один предмет: ");
            price = Convert.ToDouble(Console.ReadLine()); // tryparce
            
            Console.Write("Введите кол-во предметов: ");
            ItemCount = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Ваш баланс: "+balance+ " Цена за 1 предмет: "+price+" Кол-во предметов: "+ItemCount);

            Console.WriteLine("Цена: "+price+" Кол-во: "+ItemCount+" Цена: "+ price * ItemCount);

            Console.ReadKey();
        }
    }
}
