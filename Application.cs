using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TelegramClient.Core;
using TelegramClient.Entities.TL;
using TelegramClient.Entities.TL.Messages;

namespace Telegram_NEW
{
    //интерфейс приложения Telegram
    public interface IApplicationFixic
    {
        //клиент для подключения к серверу
        ITelegramClient Client { set; get; }
        //текущий пользователь
        IUser I { set; get; }
        //точка входа
        Task StartApplication();
        //авторизация пользователя
        Task Auth_User();
        //обновление состояния
        void UpdateApplication();
        //доступные сервисы приложения
        Task Service();
        //сервис для обмена сообщениями 
        IMessageService MessServ { set; get; }
        //сервис для работы с контактами
        IContactService ContServ { set; get; }
    }

    //класс приложения
    public class ApplicationFixic : IApplicationFixic
    {
        //клиент для подключения к сессии
        public ITelegramClient Client { set; get; }
        //текущий пользователь
        public IUser I { set; get; }
        //сервис для обмена сообщениями 
        public IMessageService MessServ { set; get; }
        //сервис для работы с контактами
        public IContactService ContServ{ set; get; }

        //точка входа
        public async Task StartApplication()
        {
            //здесь происходит подключение к тестовому серверу
            Client = ClientFactory.BuildClient(17349, "344583e45741c457fe1862106095a5eb", "149.154.167.40", 443);

            MessServ.Client = Client;
            ContServ.Client = Client;

            await Client.ConnectAsync();

            bool flag = Client.IsUserAuthorized();

            if (flag == false)
            {
                Console.WriteLine("\nПользователь не авторизирован!\n");
                await Auth_User();
            }
            else
            {
                Console.WriteLine("\nПользователь авторизирован");
                //забираем из файла актуальную информацию: наши ID, имя, фамилия 
                using (StreamReader sr = new StreamReader("inf.txt", Encoding.GetEncoding(1251)))
                {
                    I.ID = Convert.ToInt32(sr.ReadLine());
                    I.FirstName = sr.ReadLine();
                    I.LastName = sr.ReadLine();
                }
            }

            Console.Write("\nДля начала нажмите клавишу Enter\n");

            Console.ReadLine();

            //переходим к сервису доступных команд
            await Service();
        }

        //авторизация пользователя
        public async Task Auth_User()
        {
            Console.Write("\nВведите свой телефон: ");
            string MyPhone = Console.ReadLine();

            //запрос на создание сессии
            string UserHash = await Client.SendCodeRequestAsync(MyPhone);

            //здесь надо ввести код из Telegram
            Console.Write("\nВведите код из Telegram: ");
            string UserCode = Console.ReadLine();

            //авторизация пользователя
            var TLUser = await Client.MakeAuthAsync(MyPhone, UserHash, UserCode);

            I.ID = TLUser.Id;
            I.FirstName = TLUser.FirstName;
            I.LastName  = TLUser.LastName;

            //запись в файл актуальной информации: наши ID, имя, фамилия 
            using (StreamWriter sw = new StreamWriter("inf.txt"))
            {
                sw.WriteLine(I.ID.ToString());
                sw.WriteLine(I.FirstName);
                sw.WriteLine(I.LastName);
            }
        }

        //обновление состояния
        public void UpdateApplication()
        {
            Console.Clear();
            //ЗДЕСЬ БУДЕТ КУСОК КОДА ПО ОБНОВЛЕНИЮ ИНФОРМАЦИИ
        }

        //доступные сервисы приложения
        public async Task Service()
        {
            Int32 CASE = 0;

            //доступный сервис
            while (CASE != 4)
            {
                UpdateApplication();
                Console.WriteLine("1.Контакты");
                Console.WriteLine("2.Сообщения({0})", 0);
                Console.WriteLine("3.Чаты");
                Console.WriteLine("4.Закрыть Telegram_New");
                Console.Write("\nДействие: ");
                CASE = Convert.ToInt32(Console.ReadLine());

                switch(CASE)
                {
                    case 1: { await ContServ.Service();  break; }
                    case 2: { await MessServ.Service();  break; }
                    case 3: { break; }
                    case 4: { break; }
                }
            }
        }
    }
}
