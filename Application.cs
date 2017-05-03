using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TelegramClient.Core;
using TelegramClient.Entities.TL;
using TelegramClient.Entities.TL.Contacts;

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
    }

    //класс приложения
    public class ApplicationFixic : IApplicationFixic
    {
        //клиент для подключения к сессии
        public ITelegramClient Client { set; get; }
        //текущий пользователь
        public IUser I { set; get; }
        //друг для обмена данными
        public IUser Friend { set; get; }

        //точка входа
        public async Task StartApplication()
        {
            var S = new SimpleIoC();
            //регистрация пользователя
            S.Register<IUser, User>();
            I = S.Resolve<IUser>();

            Client = ClientFactory.BuildClient(196116, "f49bc1763398d6d1e935e30bf2cfe28f", "149.154.167.50", 443);

            await Client.ConnectAsync();
            bool flag = Client.IsUserAuthorized();
            if (flag == false)
            {
                Console.WriteLine("\nПользователь не авторизирован!\n");
                await Auth_User();
            }
            else
            {
                Console.WriteLine("\nПользователь авторизирован\n");
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
           // PrintContact();

            // Console.Write("\nВведите номер телефона, на который необходимо отправить сообщение (например, 79231315459): ");
            //String NewPhone = Console.ReadLine();
            String NewPhone= "";
            String NewName = "";
            Console.Write("\nВведите имя пользователя или номер телефона через 7: ");

            String temp = Console.ReadLine();

            if (temp[0] == '8' || temp[0] == '7')
            {
                 NewPhone = temp;
            }
            else
            {
                 NewName = temp;
            }

            Console.Write("\nВведите текст сообщения: ");
            String NewText = Console.ReadLine();
            //отправка сообщения по номеру телефона
            var Contacts = await Client.GetContactsAsync();
            TlUser Friend;
            if (NewName != "")
            { 
                Friend = Contacts.Users.Lists.Where(x => x.GetType() == typeof(TlUser)).Cast<TlUser>().FirstOrDefault(x => x.FirstName == NewName);
            }
            else
            {
                Friend = Contacts.Users.Lists.Where(x => x.GetType() == typeof(TlUser)).Cast<TlUser>().FirstOrDefault(x => x.Phone == NewPhone);
            }
            if (Friend == null) Console.WriteLine("\nТакого пользователя не существует");
            else await Client.SendMessageAsync(new TlInputPeerUser() { UserId = Friend.Id }, NewText);
            var Dialogs = await Client.GetUserDialogsAsync();
            //Where - фильтрация по заданному предикату
            //var Friend = Contacts.Users.Lists.Where(x => x.GetType() == typeof(TlUser)).Cast<TlUser>().FirstOrDefault(x => x.Phone == NewPhone);
            Console.ReadKey();
            var req = new TlRequestDeleteContact();
            req.Id = 
        }

        public async void PrintContact()
        {
            var Contacts = await Client.GetContactsAsync();
            var one = Contacts.Users.Lists.OfType<TlUser>();
            
            foreach (var item in one)
            {
                //var type = item.Status.GetType();
                if (item.Status.GetType() == null)
                {
                    Console.WriteLine(item.FirstName + " "+item.LastName + "\t\tну оооочень давно не был в сети");
                }
                if (item.Status.GetType() == typeof(TlUserStatusRecently))
                {
                    Console.WriteLine(item.FirstName + " " + item.LastName + "\t\tбыл в сети недавно");
                }
                if (item.Status.GetType() == typeof(TlUserStatusOffline))
                {
                    Console.WriteLine(item.FirstName + " " + item.LastName + "\t\tНе в сети");
                }
                if (item.Status.GetType() == typeof(TlUserStatusLastWeek))
                {
                    Console.WriteLine(item.FirstName + " " + item.LastName + "\t\tБыл в сети на прошлой недели");
                }
            }
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

        }
    }
}
