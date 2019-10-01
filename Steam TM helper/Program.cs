using System;
using System.Threading;
using System.Xml;
using System.Net;
using System.Collections;
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
                // Writeline
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


                // write
                case "RedN":
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.Write("[ОШИБКА] " + Text);
                    break;

                case "GreenN":
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.Write(Text);

                    break;

                case "BlueN":
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.Write(Text);
                    break;

                case "DarkRedN":
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    Console.Write("[ОШИБКА] " + Text);
                    break;

                case "DarkMagentaN":
                    Console.BackgroundColor = ConsoleColor.DarkMagenta;
                    Console.Write(Text);
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
                Console.WriteLine("Steam TM Helper | Update v7.1.1 | by Wichid\nhttps://git.io/fjxVq");
                
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
                                                if (Math.Round(Balance, 2) < Item_Price & Balance != 0) Console.Write(", Остаток: " + Math.Round(Balance, 2) + " " + CurrencyIsNow);
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

                                        else if (Sell_Price != 0 & Sell_Price > 0) {
                                            Sell_Parce = false;
                                            Sell_Price *= Sell_Count;
                                        }
                                        else Sell_case = Sell_Count_While = Sell_Parce = false; Break_Massage = "[КУПИТЬ] Отмена"; // если 0 или мельше то выход!

                                    }
                                    

                                    if (Sell_Parce == false & Sell_Count_While == false & Sell_case == true) {

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
                        Console.WriteLine("[ONLINE] Выбирите что вы хотите сделать:\n \t1. Получить данные \n \t2. Дерево друзей\n \t3. -\n\n\t0. Назад");

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
                                        Console.Beep();
                                        sError = true;
                                    }
                                     // рефактоинг двух if
                                    if (XmlTreeDoc.Name == "customURL" & ( Properties.Settings.Default.SteamID == tSpaceRemover(XmlTreeDoc.InnerText) ) ) sCustomID = true;// проверка на совпадение введённого steam id с тем который в xml файле

                                    if (XmlTreeDoc.Name == "steamID64" & (Properties.Settings.Default.SteamID == XmlTreeDoc.InnerText) ) sSteam64ID = true; // если steamid == тому котрый в xml документе то ок!
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
                                                Text_Color("DarkRed", tSpaceRemover(XmlTreeDoc64.InnerText));
                                                Console.Beep();
                                                sError = true;
                                                break;
                                            }

                                            if (Properties.Settings.Default.SteamID == XmlTreeDoc64.InnerText) {
                                                sSteam64ID = true;
                                                sError = false;

                                            }  // если steamid == тому котрый в xml документе то ок!

                                        }

                                    }


                                    if ((sCustomID == true | sSteam64ID == true) & sError == false) // Если хоть одна ссылка заработала то норм
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
                                        Console.WriteLine("\n[ДАННЫЕ] Полученные данные из Community Data xml");
                                        Console.WriteLine("STEAM64ID: " + data[0] + " | Ник:" + data[1]);
                                        Console.WriteLine("Имя: "               + data[14] + "\t\t| Локация: " + data[13]);
                                        Console.WriteLine("Онлайн статус: "     + data[2] + "\t| Статус: " + data[3]);
                                        Console.WriteLine("В стим с: " + data[10] );

                                        Console.WriteLine("\nПриватность:\t"+ data[4]);
                                        Console.WriteLine("Видимость:\t"    + data[5]);
                                        Console.WriteLine("VAC:\t\t"        + data[6]);
                                        Console.WriteLine("Бан трейда:\t"   + data[7]);
                                        Console.WriteLine("Лимит на аккаунте: " + data[8]);
                                        Console.WriteLine("\nURL:https://steamcommunity.com/profiles/" + data[0] + "/" + " | URL:https://steamcommunity.com/id/" + data[9]+"/");

                                        // Отправка запроса серверу и получение ответа
                                        // вдруг сервер ответит что ключ не валид
                                        // Отправка запроса
                                        WebRequest sApiRequest = WebRequest.Create(@"http://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key=" +Properties.Settings.Default.SteamApiKey + "&steamids=" + data[0] + "&format=xml");
                                        HttpWebResponse sApiResponse = null;

                                        try
                                        {
                                            sApiResponse = (HttpWebResponse)sApiRequest.GetResponse();
                                        }
                                        catch (WebException sAns) {
                                            sApiResponse = (HttpWebResponse)sAns.Response;
                                        }

                                        if (sApiResponse.StatusCode == HttpStatusCode.OK) // если сервер вернул код 200 (ОК) то значит ключ подошёл
                                        {

                                            XmlDocument sApiInfo = new XmlDocument();
                                            Console.WriteLine("\n[SteamApiKey] Загрузка дополнительных данных.");
                                            Text_Color("Blue", "http://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key=*скрыто*&steamids=" + data[0] + "&format=xml");
                                            sApiInfo.Load(@"http://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key=" + Properties.Settings.Default.SteamApiKey + "&steamids=" + data[0] + "&format=xml");
                                            XmlElement sApiSummaries = sApiInfo.DocumentElement;

                                            string[] sSummaries = new string[17]; 

                                            foreach (XmlNode sApiRoot in sApiSummaries)
                                            {
                                                if (sApiRoot.Name == "players")
                                                {
                                                    foreach (XmlNode sApiDataTree in sApiRoot) {
                                                        if (sApiDataTree.Name == "player") {
                                                            foreach (XmlNode sApiData in sApiDataTree) {
                                                                switch (sApiData.Name)
                                                                {
                                                                    // public data
                                                                    case "steamid": // 64id
                                                                        sSummaries[0] = sApiData.InnerText;
                                                                        break;

                                                                    case "personaname": // personaname - Username
                                                                        sSummaries[1] = sApiData.InnerText;
                                                                        break;

                                                                    case "profileurl": // community url
                                                                        sSummaries[2] = sApiData.InnerText;
                                                                        break;

                                                                    case "personastate": // 0 - Offline, 1 - Online, 2 - Busy, 3 - Away, 4 - Snooze, 5 - looking to trade, 6 - looking to play.
                                                                        sSummaries[3] = sApiData.InnerText;
                                                                        break;

                                                                    case "communityvisibilitystate": // приватность 1 == ЗКАРЫТО
                                                                        sSummaries[4] = sApiData.InnerText;
                                                                        break;

                                                                    case "profilestate": // Был ли настроин профиль
                                                                        sSummaries[5] = sApiData.InnerText;
                                                                        break;

                                                                    case "lastlogoff": // был в сети в UNIX time
                                                                        sSummaries[6] = sApiData.InnerText;
                                                                        break;

                                                                    case "commentpermission": // Открыты ли коментарии
                                                                        sSummaries[7] = sApiData.InnerText;
                                                                        break;

                                                                        // private
                                                                        
                                                                    case "realname": // типо Максим
                                                                        sSummaries[8] = sApiData.InnerText;
                                                                        break;

                                                                    case "primaryclanid": // "Главная" группа
                                                                        sSummaries[9] = sApiData.InnerText;
                                                                        break;

                                                                    case "timecreated": // Unix Дата создания
                                                                        sSummaries[10] = sApiData.InnerText;
                                                                        break;

                                                                    case "gameid": // если in-game то id игры
                                                                        sSummaries[11] = sApiData.InnerText;
                                                                        break;

                                                                    case "gameserverip": //
                                                                        sSummaries[12] = sApiData.InnerText;
                                                                        break;

                                                                    case "gameextrainfo": // название игры, если нон-стим то имя приложения
                                                                        sSummaries[13] = sApiData.InnerText;
                                                                        break;

                                                                    case "loccountrycode": //
                                                                        sSummaries[14] = sApiData.InnerText;
                                                                        break;

                                                                    case "locstatecode": //
                                                                        sSummaries[15] = sApiData.InnerText;
                                                                        break;

                                                                    case "loccityid": //
                                                                        sSummaries[16] = sApiData.InnerText;
                                                                        break;


                                                                }


                                                            }


                                                        }


                                                    }
                                                }

                                            }
                                            // загрузка списка банов
                                            string[] sDataBans = new string[6];

                                            XmlDocument DataBans = new XmlDocument();
                                            Console.WriteLine("\n[SteamApiKey] Загрузка банов аккаунта.");
                                            Text_Color("Blue", "http://api.steampowered.com/ISteamUser/GetPlayerBans/v1/?key=*Скрыто*&steamids=" + data[0] + "&format=xml");
                                            
                                            DataBans.Load   ( @"http://api.steampowered.com/ISteamUser/GetPlayerBans/v1/?key=" + Properties.Settings.Default.SteamApiKey + "&steamids=" + data[0] + "&format=xml");
                                            XmlElement DataBansEl = DataBans.DocumentElement;

                                            foreach (XmlNode BanTree in DataBansEl) {
                                                if (BanTree.Name == "players") {
                                                    foreach (XmlNode sBansPlayer in BanTree) {
                                                        if (sBansPlayer.Name == "player") {
                                                            foreach (XmlNode sBans in sBansPlayer) {
                                                                switch (sBans.Name)
                                                                {
                                                                    case "CommunityBanned": // Ну тут всё ясно XD
                                                                        sDataBans[0] = sBans.InnerText;
                                                                        break;

                                                                    case "VACBanned": // и тут
                                                                        sDataBans[1] = sBans.InnerText;
                                                                        break;

                                                                    case "NumberOfVACBans": //и тут
                                                                        sDataBans[2] = sBans.InnerText;
                                                                        break;

                                                                    case "DaysSinceLastBan": //и тут
                                                                        sDataBans[3] = sBans.InnerText;
                                                                        break;

                                                                    case "EconomyBan": //и тут
                                                                        sDataBans[5] = sBans.InnerText;
                                                                        break;

                                                                }

                                                            }
                                                            

                                                        }
                                                        

                                                    }
                                                }

                                            }


                                            /*
                                             ВЫВОД ВСЕХ ДАННЫХ ИЗ 2-х МАССИВОВ
                                            sSummaries  - ОБЫЧНЫЕ ДАННЫЕ
                                            sDataBans   - БАНЫ
                                            */
                                            Text_Color("DarkMagenta", "\n[!] В целях экономии места буду писать коротко, иногда на eng.");
                                            Console.WriteLine("[ДАННЫЕ] Публичные данные.");
                                            Console.WriteLine("Steam64ID: "+sSummaries[0]+ " | Nick: " + sSummaries[1]);
                                            Console.WriteLine("Настройка профиля: " + sSummaries[5] + " | Статус профиля: " +sSummaries[3] );
                                            Console.WriteLine("Видимость профиля: " + sSummaries[4] + " | Доступ к комментариям: " + sSummaries[7]);
                                            Console.WriteLine("Last off: " + sSummaries[6]+" | URL: " + sSummaries[2]);
                                            // private
                                            if      (sSummaries[4] == "1")  Console.WriteLine("Профиль закрыт!");
                                            else if (sSummaries[4] == "2")  Console.WriteLine("Профиль закрыт! Но открыт для друзей.");
                                            else {
                                                Console.WriteLine("\n[ДАННЫЕ] Приватные данные");
                                                if (!String.IsNullOrEmpty(sSummaries[8])) Console.Write("Имя: " + sSummaries[8]); if (!String.IsNullOrEmpty(sSummaries[14])) Console.Write(" | Страна: " + sSummaries[14] + "\n");
                                                if (!String.IsNullOrEmpty(sSummaries[9])) Console.WriteLine("ClanId: " + sSummaries[9]);
                                                Console.WriteLine("Acc created UNIX: " + sSummaries[10]);
                                                if (!String.IsNullOrEmpty(sSummaries[11])) Console.WriteLine("ID игры: " + sSummaries[11] + " | ip сервера: " + sSummaries[12]);
                                                if (!String.IsNullOrEmpty(sSummaries[13])) Console.WriteLine("non-steam игры: " + sSummaries[13]);
                                                if (!String.IsNullOrEmpty(sSummaries[15])) Console.WriteLine("Штат/город: " + sSummaries[15]);
                                                if (!String.IsNullOrEmpty(sSummaries[16])) Console.WriteLine("Область: " + sSummaries[16]);

                                            }
                                            Console.WriteLine("\n[ДАННЫЕ] Баны");

                                            Console.WriteLine("Кол-во VAC баннов: " + sDataBans[2]+" | VAC Бан: " + sDataBans[1]);
                                            Console.WriteLine("Дней с VAC бана: " + sDataBans[3] + " | Бан сообщества: " + sDataBans[0]);
                                            Console.WriteLine("Кол-во игровых банов: " + sDataBans[4] + " | 5$ лимит: " + sDataBans[5]);

                                            //


                                        }
                                        else if ( string.IsNullOrEmpty(Properties.Settings.Default.SteamApiKey) ) Text_Color("DarkRed", "[APIKEY] Что бы получить доп. данные введите SteamApiKey в настройках");

                                        else if (sApiResponse.StatusCode == HttpStatusCode.Forbidden) // если сервер запрещяет доступ то
                                        {
                                            if (string.IsNullOrEmpty(Properties.Settings.Default.SteamApiKey))// проверка на ноль или пустоту
                                            {
                                                Text_Color("DarkMagenta", "Для получения доп. данных введите SteamApiKey");// Если ApiKey не задан то написать
                                            }
                                            else {
                                                Text_Color("DarkRed", "[APIKEY] Ошибка 403, Вы ввели неправельный SteamApikey");// если доступ запрещён то значит что неправельный ключ
                                            }
                                        }
                                        else {
                                            Text_Color("DarkRed", "[APIKEY] Неизвестная ошибка!"); // другая любая ошибка типо 404 и т. д.
                                        }

                                        sApiResponse.Close();// закрытие 

                                        sCustomID = sSteam64ID = sError = false;
                                    }
                                    else if (sCustomID == false & sSteam64ID == false & sError == true)
                                    { // если ошибка то вывод текста
                                        Break_Massage = "Похоже что вы ввели не правельный SteamID[64/Custom], измените его в настройках!";
                                        Text_Color("DarkRed", "Похоже что вы ввели не правельный SteamID[64/Custom], измените его в настройках!");
                                        Console.WriteLine("sCustomID: {0}, sSteamID64: {1}, sError: {2}", sCustomID, sSteam64ID, sError);
                                        sCustomID = sSteam64ID = sError = false;
                                        break;
                                    }

                                }
                                ////
                                Console.WriteLine("\nНажмите любую клавищу для продолжения...");
                                Console.ReadKey();

                                break;


                            case ConsoleKey.D2:
                                // friend tree
                                Console.WriteLine("Вы выбрали дерево друзей.\n");
                                Console.WriteLine("[FriendTree] Выбирите способ. \n\t1. Используя ваш SteamID \n\t2. Используя SteamID друга \n\n\t0. Назад");

                                ConsoleKey fTreeKey;
                                fTreeKey = Console.ReadKey(true).Key;

                                switch (fTreeKey) {
                                    case ConsoleKey.D1:
                                        // Добавить ввод и проверку steamId 
                                        Console.WriteLine("");


                                        XmlDocument fTreeFriendSelf = new XmlDocument();
                                        // try catch NET !!!!!!!

                                        
                                        fTreeFriendSelf.Load(@"http://api.steampowered.com/ISteamUser/GetFriendList/v0001/?key=" + Properties.Settings.Default.SteamApiKey + "&steamid=76561198135861777&format=xml&relationship=friend");
                                        XmlElement fTreefriendSelfElement = fTreeFriendSelf.DocumentElement;
                                        int i = 0;
                                        int d = 0;
                                        int c = 0;
                                        foreach (XmlNode FriendsTree in fTreefriendSelfElement) {
                                            Console.WriteLine(c + fTreefriendSelfElement.Name);
                                            c++;
                                            foreach (XmlNode FriendTree in FriendsTree) {// friend
                                                Console.WriteLine(i + FriendTree.Name);
                                                i++;
                                                foreach (XmlNode FriendTreeDetails in FriendTree) {// iside friend XD
                                                    if (FriendTreeDetails.Name == "steamid") {
                                                        Console.WriteLine(d + " " + FriendTreeDetails.InnerText);
                                                        d++;
                                                    } 
                                                    
                                                }
                                                
                                            }

                                        }


                                        Console.ReadKey();

                                        break;

                                    case ConsoleKey.D2:
                                        Console.WriteLine("2");
                                        Console.ReadKey();

                                        break;
                                }




                                break;

                            case ConsoleKey.D3:



                                break;

                        }
                        // В будующем

                        break;



                    case ConsoleKey.D3: // настройки
                        Console.WriteLine("\nВы выбрали настройки\n");
                        Console.WriteLine("[НАСТРОЙКИ] Что вы хотите поменять?");
                        Console.Write("\n\t1. Вид денег \t"); Text_Color("BlueN", Properties.Settings.Default.Currency_Type);
                        Console.Write("\n\t2. SteamID \t"); Text_Color("BlueN", Properties.Settings.Default.SteamID);
                        Console.Write("\n\t3. Steam Api Key");if (!String.IsNullOrEmpty(Properties.Settings.Default.SteamApiKey)) Console.Write(": *Скрыт*");
                        Console.WriteLine("\n\n\t0. Назад");
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
                                    Console.WriteLine("\n[APIKEY] Текущий Api Key не задан.");
                                    Text_Color("Blue","Вы можете получить его тут: https://steamcommunity.com/dev/apikey");
                                    Console.WriteLine("\nХотите ли вы его задать? [Y\\N]\n");

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
