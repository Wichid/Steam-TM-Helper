using System;
using System.Threading;
using System.Xml;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam_TM_helper
{
    class Program
    {

        static string tSpaceRemover(string Text)
        {

            if (Text.IndexOf(' ') >= 0)
            {
                Text = Text.Remove(Text.IndexOf(' '), 1);
            }

            if (Text.LastIndexOf(' ') >= 0)
            {
                Text = Text.Remove(Text.LastIndexOf(' '), 1);
            }


            return Text;

        }


        static void Text_Color(string Color, string Text)
        {

            switch (Color)
            {

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

                case "DarkMagenta":
                    Console.BackgroundColor = ConsoleColor.DarkMagenta;
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
            string Break_Massage = null;
            while (Main_Loop == true)
            {
                string CurrencyIsNow = Properties.Settings.Default.Currency_Type;
                Console.Clear();

                Console.WriteLine("Steam TM helper Update v6");
                if (!string.IsNullOrEmpty(Break_Massage)) // если что то отменить то можно будет увидить причину
                {
                    Text_Color("DarkMagenta", ("Причина выхода: " + Break_Massage));

                    Break_Massage = null;
                }
                Console.WriteLine("\nВыберите режим:\n \t1. Оффлайн режим \n \t2. Онлайн режим \n \t3. Настройки \n\n \t0. Выход");

                ConsoleKey KeyChoice = Console.ReadKey(true).Key;
                switch (KeyChoice)
                { // Выбор режима Онлайн или Офлайн

                    case ConsoleKey.D1: // Оффлайн часть!
                        Console.WriteLine("\nВы выбрали offline режим\n");
                        Console.WriteLine("[OFFLINE] Выбирите что вы хотите сделать:\n \t1. Купить \n \t2. Расчет стоимости предметов\n \t3. Продать\n\n\t0. Назад");
                        switch (KeyChoice = Console.ReadKey(true).Key)
                        { // Выбор Купить, Расчёт стоимости предмета, Продажа

                            case ConsoleKey.D1:
                                Console.WriteLine("\n[КУПИТЬ] Купить предметы");
                                bool buy = true; // цикл для покупки                                
                                while (buy == true) // цикл покупки
                                {
                                    double ItemPrice = 0;
                                    int ItemCount = 0;
                                    bool IteP = true, IteC = true; // циклы

                                    while (IteP == true) // проверка
                                    {
                                        Console.Write("[КУПИТЬ] Введите цену за один предмет: ");

                                        if (!Double.TryParse(Console.ReadLine(), out ItemPrice))

                                            Text_Color("DarkRed", "Введите число в формате 1,23");

                                        else if (ItemPrice != 0 & ItemPrice > 0) IteP = false; // продолжить!
                                        else buy = IteC = IteP = false; Break_Massage = "[КУПИТЬ] Отмена"; // если 0 или мельше то выход!

                                    }

                                    while (IteC == true) // проверка
                                    {
                                        Console.Write("[КУПИТЬ] Введите кол-во предметов: ");

                                        if (!Int32.TryParse(Console.ReadLine(), out ItemCount))
                                            Text_Color("Red", "Введите целое число");

                                        else if (ItemCount != 0 & ItemCount > 0) IteC = false; // продолжить!
                                        else buy = IteC = IteP = false; Break_Massage = "[КУПИТЬ] Отмена"; // если 0 или мельше то выход!
                                    }

                                    if (IteC == false & IteP == false & buy == true) { // 

                                        Console.WriteLine("\nЦена 1-го предмета: " + ItemPrice + " " + CurrencyIsNow + " Кол-во предметов: " + ItemCount + " Итог: " + ItemPrice * ItemCount + " " + CurrencyIsNow);
                                        Console.WriteLine("Продать в ноль за: " + Math.Round((ItemPrice * 0.15) + ItemPrice, 2) + " " + CurrencyIsNow);

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
                                    

                                }
                                break; // конец цикл покупки

                            case ConsoleKey.D2:
                                Console.WriteLine("\n[РАСЧЕТ] Расчет кол-ва предметов на сколько вам хватит");

                                bool Count_Buy = true; // цикл для расчета

                                while (Count_Buy == true) // цикл расчёта
                                {
                                    double Balance = 0, Item_Price = 0;
                                    int Item_Count_All = 0;
                                    bool Sec_Balance = true, Sec_Price = true, Sec_Func = true; // циклы

                                    while (Sec_Price == true) // проверка цены
                                    {
                                        Console.Write("[РАСЧЕТ] Введите цену за один предмет: ");

                                        if (!Double.TryParse(Console.ReadLine(), out Item_Price))
                                            Text_Color("DarkRed", "Введите число в формате 1,23");

                                        else if (Item_Price != 0 & Item_Price > 0) Sec_Price = false;
                                        else Count_Buy = Sec_Price = Sec_Balance = false; Break_Massage = "[КУПИТЬ] Отмена"; // если 0 или мельше то выход!
                                    }

                                    while (Sec_Balance == true) // проверка баланса
                                    {
                                        Console.Write("[РАСЧЕТ] Введите ваш бюджет: ");

                                        if (!Double.TryParse(Console.ReadLine(), out Balance))
                                            Text_Color("DarkRed", "Введите число в формате 1,23");

                                        else if (Balance != 0 & Balance > 0) Sec_Balance = false;
                                        else Count_Buy = Sec_Price = Sec_Balance = false; Break_Massage = "[КУПИТЬ] Отмена"; // если 0 или мельше то выход!

                                    }

                                    if (Sec_Price == false & Sec_Balance == false & Count_Buy == true){

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
                                                if (Math.Round(Balance, 2) < Item_Price)
                                                {
                                                    if (Balance != 0)
                                                        Console.Write(", Остаток: " + Math.Round(Balance, 2) + " " + CurrencyIsNow);
                                                }
                                            }
                                            else Sec_Func = false;

                                        }

                                        ConsoleKey ContinueBuy;
                                        Console.WriteLine("\nХотите ли вы повторить? [Y\\N]\n");
                                        switch (ContinueBuy = Console.ReadKey(true).Key)
                                        {
                                            case ConsoleKey.Y:
                                                Sec_Price = Sec_Balance = true;
                                                break;

                                            case ConsoleKey.N:
                                                Count_Buy = false; // выход из цикла
                                                break;

                                        }

                                    }
                                    
                                }
                                break; // конец расчет

                            case ConsoleKey.D3: // продать
                                int Sell_Count = 0;
                                double Sell_Price = 0;
                                bool Sell_Parce = true, Sell_case = true, Sell_Count_While = true; // циклы
                                while (Sell_case)
                                {
                                    Console.WriteLine("\n[ПРОДАТЬ] Продажа предмета"); // next

                                    while (Sell_Count_While == true) // проверка
                                    {
                                        Console.Write("[ПРОДАТЬ] Введите кол-во предметов: ");
                                        if (!Int32.TryParse(Console.ReadLine(), out Sell_Count))
                                            Text_Color("Red", "Введите целое число");

                                        else if (Sell_Count != 0 & Sell_Count > 0) Sell_Count_While = false;
                                        else Sell_case = Sell_Count_While = Sell_Parce = false; Break_Massage = "[КУПИТЬ] Отмена"; // если 0 или мельше то выход!
                                    }

                                    while (Sell_Parce == true) // проверка
                                    {
                                        Console.Write("[ПРОДАТЬ] Введите цену за один предмет: ");

                                        if (!Double.TryParse(Console.ReadLine(), out Sell_Price))
                                            Text_Color("DarkRed", "Введите число в формате 1,23");

                                        else if (Sell_Price != 0 & Sell_Price > 0) Sell_Parce = false;
                                        else Sell_case = Sell_Count_While = Sell_Parce = false; Break_Massage = "[КУПИТЬ] Отмена"; // если 0 или мельше то выход!

                                    }

                                    if (Sell_Parce == false & Sell_Count_While == false & Sell_case == true) {

                                        Sell_Price *= Sell_Count;
                                        Console.WriteLine("\nВы получите: " + Sell_Price + " " + CurrencyIsNow + " Покупатель заплатит: " + Math.Round((Sell_Price * 0.15) + Sell_Price, 2) + " " + CurrencyIsNow);

                                        ConsoleKey ContinueSell;
                                        Console.WriteLine("\nХотите ли вы повторить? [Y\\N]");
                                        switch (ContinueSell = Console.ReadKey(true).Key)
                                        {
                                            case ConsoleKey.Y:
                                                Sell_case = Sell_Parce = Sell_Count_While = true;
                                                break;

                                            case ConsoleKey.N:
                                                Sell_case = false; // выход из цикла
                                                break;

                                        }

                                    }

                                }

                                break; // прдажа case

                            default:
                                Console.WriteLine("Назад");
                                break;

                        }

                        break; // конец оффлайн часть

                    case ConsoleKey.D2: // ОНЛАЙН ЧАСТЬ!
                        ConsoleKey oReadKey;
                        Console.WriteLine("\nВы выбрали offline режим\n");
                        Console.WriteLine("[ONLINE] Выбирите что вы хотите сделать:\n \t1. Получить данные \n \t2. -\n \t3. -\n\n\t0. Назад");

                        switch (oReadKey = Console.ReadKey(true).Key)
                        {
                            case ConsoleKey.D1:

                                bool sCustomID = false, sSteam64ID = false, sError = false;

                                Console.WriteLine("\n[ONLINE] Получить данные");

                                XmlDocument xDoc = new XmlDocument(); // новый документ
                                // нужна проверка на 404 и другурие ситуации
                                // удаление всех символов
                                Console.WriteLine("\n[ДАННЫЕ] Загрузка xml документа[1].");
                                xDoc.Load(@"http://steamcommunity.com/id/" + Properties.Settings.Default.SteamID + "/?xml=1"); //Загрузка xml документа по ссылке
                                Text_Color("Blue", "URL: https://steamcommunity.com/id/" + Properties.Settings.Default.SteamID + "/?xml=1");
                                XmlElement xRoot = xDoc.DocumentElement; // получение корня xml документа

                                foreach (XmlNode XmlTreeDoc in xRoot)
                                {

                                    if (XmlTreeDoc.Name == "error")
                                    { // если xml файл содержит тэг error знатит профиль не найден

                                        Text_Color("DarkRed", tSpaceRemover(XmlTreeDoc.InnerText)); // вывод  сообщения xml файла
                                        sError = true;
                                    }

                                    if (XmlTreeDoc.Name == "customURL")
                                    {
                                        if (Properties.Settings.Default.SteamID == tSpaceRemover(XmlTreeDoc.InnerText))// проверка на совпадение введённого steam id с тем который в xml файле
                                        {
                                            Console.WriteLine("CustomURL == SteamID");
                                            sCustomID = true;
                                        }

                                    }

                                    if (XmlTreeDoc.Name == "steamID64")
                                    {

                                        if (Properties.Settings.Default.SteamID == XmlTreeDoc.InnerText)
                                        {
                                            Console.WriteLine("Steam64ID == SteamID");
                                            sSteam64ID = true;
                                        }

                                    }
                                    else if (sError == true)
                                    { // загрузка второго документа
                                        Console.WriteLine("\n[ДАННЫЕ] Загрузка xml документа[2].");
                                        xDoc.Load(@"https://steamcommunity.com/profiles/" + Properties.Settings.Default.SteamID + "/?xml=1"); //Загрузка xml документа по ссылке
                                        Text_Color("Blue", "URL: https://steamcommunity.com/profiles/" + Properties.Settings.Default.SteamID + "/?xml=1");
                                        xRoot = xDoc.DocumentElement; // получение корня xml документа

                                        foreach (XmlNode XmlTreeDoc64 in xRoot)
                                        {
                                            if (XmlTreeDoc64.Name == "error")
                                            {
                                                Text_Color("DarkRed", tSpaceRemover(XmlTreeDoc.InnerText));
                                                sError = true;
                                                break;
                                            }

                                            if (Properties.Settings.Default.SteamID == XmlTreeDoc64.InnerText)
                                            {
                                                Console.WriteLine("Steam64ID == SteamID");
                                                sSteam64ID = true;
                                            }


                                        }

                                    }


                                    if (sCustomID == true | sSteam64ID == true & sError == false) // Если хоть одна ссылка заработала то норм
                                    {
                                        // дальше идут действия связанные с данными.
                                        string[] data = new string[16]; // создание массива на 15 элементов

                                        foreach (XmlNode xnode in xRoot) {

                                            switch (xnode.Name){

                                                case "steamID64":
                                                    data[0] = xnode.InnerText;
                                                    break;                                        

                                                case "steamID":
                                                    data[1] = xnode.InnerText;
                                                    break;

                                                case "onlineState":
                                                    data[2] = xnode.InnerText;
                                                    break;

                                                case "stateMessage":
                                                    data[3] = xnode.InnerText;
                                                    break;

                                                case "privacyState":
                                                    data[4] = xnode.InnerText;
                                                    break;

                                                case "visibilityState":
                                                    data[5] = xnode.InnerText;
                                                    break;

                                                case "vacBanned":
                                                    data[6] = xnode.InnerText;
                                                    break;

                                                case "tradeBanState":
                                                    data[7] = xnode.InnerText;
                                                    break;

                                                case "isLimitedAccount":
                                                    data[8] = xnode.InnerText;
                                                    break;

                                                case "customURL":
                                                    data[9] = xnode.InnerText;
                                                    break;

                                                case "memberSince":
                                                    data[10] = xnode.InnerText;
                                                    break;

                                                case "hoursPlayed2WK":
                                                    data[11] = xnode.InnerText;
                                                    break;

                                                case "headline":
                                                    data[12] = xnode.InnerText;
                                                    break;

                                                case "location":
                                                    data[13] = xnode.InnerText;
                                                    break;

                                                case "realname":
                                                    data[14] = xnode.InnerText;
                                                    break;

                                                case "summary":
                                                    data[15] = xnode.InnerText;
                                                    break;

                                            }




                                        // Console.WriteLine(xnode.Name+" "+ xnode.InnerText);

                                        }
                                        int arrcount = 0;
                                        while (data.Length > arrcount) {
                                            Console.WriteLine(data[arrcount]);
                                            arrcount++;

                                        }

                                        sCustomID = sSteam64ID = sError = false;
                                    }
                                    else if (sCustomID == false & sSteam64ID == false & sError == true)
                                    { // если ошибка то вывод текста

                                        Text_Color("DarkRed", "Похоже что вы ввели не правельный SteamID[64/Custom], измените его в настройках!");
                                        Console.WriteLine("sCustomID: {0}, sSteamID64: {1}, sError: {2}", sCustomID, sSteam64ID, sError);
                                        sCustomID = sSteam64ID = sError = false;
                                        break;
                                    }

                                }
                                ////

                                Console.ReadKey();

                                break;

                            case ConsoleKey.D2:



                                break;

                            case ConsoleKey.D3:



                                break;

                        }
                        // В будующем

                        break;






                    case ConsoleKey.D3: // настройки
                        Console.WriteLine("\nВы выбрали настройки\n");
                        Console.WriteLine("[НАСТРОЙКИ] Что вы хотите поменять? \n\t1. Вид денег\n\t2. SteamID\n\t3. Steam Api Key \n\n\t0. Назад");
                        ConsoleKey SettingKey;
                        switch (SettingKey = Console.ReadKey(true).Key)
                        {
                            case ConsoleKey.D1:
                                string Currency_Type;
                                ConsoleKey SettingsBool;
                                Console.WriteLine("\n[ВАЛЮТА] Текщий вид денег: " + Properties.Settings.Default.Currency_Type);
                                Console.WriteLine("\nХотите ли вы его поменять? [Y\\N]\n");

                                switch (SettingsBool = Console.ReadKey(true).Key)
                                {

                                    case ConsoleKey.Y:
                                        Console.Write("[ВАЛЮТА] Введите новый вид денег: ");
                                        Properties.Settings.Default.Currency_Type = Currency_Type = Console.ReadLine();
                                        Console.WriteLine("[ВАЛЮТА] Настройки успешно сохранены.");
                                        Properties.Settings.Default.Save();
                                        break;

                                    default:
                                        Console.WriteLine("[ВАЛЮТА] Отмена");

                                        break;

                                }
                                Thread.Sleep(250); // костыль


                                break;

                            case ConsoleKey.D2:
                                // Изменение steamid

                                string SteamID;
                                ConsoleKey SteamKeyBool;

                                if (String.IsNullOrEmpty(Properties.Settings.Default.SteamID))
                                { // пуст ли SteamID
                                    Console.WriteLine("\n[STEAMID] Текущий SteamID не задан." + Properties.Settings.Default.SteamID);
                                    Console.WriteLine("Хотите ли вы его задать? [Y\\N]\n");

                                }
                                else // нет
                                {
                                    Console.WriteLine("\n[STEAMID] Текущий SteamID: " + Properties.Settings.Default.SteamID);
                                    Console.WriteLine("\nХотите ли вы его поменять? [Y\\N]\n");
                                }

                                switch (SteamKeyBool = Console.ReadKey(true).Key)
                                {
                                    case ConsoleKey.Y:
                                        Console.Write("[STEAMID] Введите новый SteamID: ");
                                        Properties.Settings.Default.SteamID = SteamID = Console.ReadLine();
                                        Console.WriteLine("[STEAMID] SteamID успешно сохранены.");
                                        Properties.Settings.Default.Save();
                                        break;

                                    default:
                                        Console.WriteLine("[STEAMID] Отмена");

                                        break;

                                }

                                break;

                            case ConsoleKey.D3:
                                // 3-я настройка - SteamApiKey
                                string SteamApiKey;
                                ConsoleKey SteamApiKeyBool;

                                if (String.IsNullOrEmpty(Properties.Settings.Default.SteamApiKey))
                                { // пуст ли STEAMAPIKEY
                                    Console.WriteLine("\n[APIKEY] Текущий Api Key не задан." + Properties.Settings.Default.SteamApiKey);
                                    Console.WriteLine("Хотите ли вы его задать? [Y\\N]\n");

                                }
                                else // нет
                                {
                                    Console.WriteLine("\n[APIKEY] Текущий Api Key: " + Properties.Settings.Default.SteamApiKey);
                                    Console.WriteLine("\nХотите ли вы его поменять? [Y\\N]\n");
                                }

                                switch (SteamApiKeyBool = Console.ReadKey(true).Key)
                                {
                                    case ConsoleKey.Y:
                                        Console.Write("[APIKEY] Введите новый Api Key: ");
                                        Properties.Settings.Default.SteamApiKey = SteamApiKey = Console.ReadLine();
                                        Console.WriteLine("[APIKEY] Api Key успешно сохранены.");
                                        Properties.Settings.Default.Save();
                                        break;

                                    default:
                                        Console.WriteLine("[STEAMID] Отмена");

                                        break;

                                }
                                break;

                            case ConsoleKey.D4:
                                // 4-я настройка

                                break;


                            case ConsoleKey.D0:

                                break;


                        }

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
