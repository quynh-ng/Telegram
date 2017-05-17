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
    //интерфейс сервиса отправки сообщений пользователю (поиск по номеру телефона и поиск по имени)
    public interface IMessageService
    {
        Task<bool> SendMessageByPhone();  //отправить сообщение по номеру телефона
        Task<bool> SendMessageByName ();  //отправить сообщение по имени пользователя
        Task<bool> GetMessageByName  ();  //получить список сообщений по имени пользователя  
        Task Service();                   //предоставляет сервис по отправке сообщений
        ITelegramClient Client {set;get;} //методы для отправки сообщений
    }

    //сервис отправки сообщений
    class MessageService : IMessageService
    {
        public ITelegramClient Client { set; get; }

        //предоставляет сервис по отправке сообщений
        public async Task Service()
        {
            Console.Clear();
            Int32 CASE = 0;

            //доступный сервис
            while (CASE != 4)
            {
                Console.Clear();
                Console.WriteLine("1.Отправить сообщение по номеру телефона");
                Console.WriteLine("2.Отправить сообщение по имени пользователя");
                Console.WriteLine("3.Просмотреть сообщения");
                Console.WriteLine("4.Вернуться");
                Console.Write("\nДействие: ");
                CASE = Convert.ToInt32(Console.ReadLine());

                switch (CASE)
                {
                    case 1: { await SendMessageByPhone(); break; }
                    case 2: { await SendMessageByName (); break; }
                    case 3: { await GetMessageByName();   break; }
                    case 4: { break; }
                }

            }
        }

        //отправить сообщение по номеру телефона
        public async Task<bool> SendMessageByPhone()
        {
            Console.Write("\nВведите номер телефона (начинать с 7): ");

            String NewPhone = Console.ReadLine();
                       
            //получение доступных контактов
            var Contacts = await Client.GetContactsAsync();

            //поиск по телефону
            var Friend = Contacts.Users.Lists.Where(x => x.GetType() == typeof(TlUser)).Cast<TlUser>().FirstOrDefault(x => x.Phone == NewPhone);

            if (Friend == null)
            {
                Console.WriteLine("\nТакого пользователя не существует");
                Console.ReadLine();
                return false;
            }

            Console.Write("\nВведите текст сообщения: ");

            String NewText = Console.ReadLine();

            await Client.SendMessageAsync(new TlInputPeerUser() { UserId = Friend.Id }, NewText);

            return true;
        }

        //отправить сообщение по имени
        public async Task<bool> SendMessageByName()
        {
            Console.Write("\nВведите имя пользователя: ");

            String NewName = Console.ReadLine();

            //получение доступных контактов
            var Contacts = await Client.GetContactsAsync();

            //поиск по имени
            var Friend = Contacts.Users.Lists.Where(x => x.GetType() == typeof(TlUser)).Cast<TlUser>().FirstOrDefault(x => x.FirstName == NewName);

            if (Friend == null)
            {
                Console.WriteLine("\nТакого пользователя не существует");
                Console.ReadLine();
                return false;
            }

            Console.Write("\nВведите текст сообщения: ");

            String NewText = Console.ReadLine();

            await Client.SendMessageAsync(new TlInputPeerUser() { UserId = Friend.Id }, NewText);

            return true;
        }

        //получить список сообщений по имени пользователя 
        public async Task<bool> GetMessageByName()
        {
            Console.Write("\nВведите имя пользователя: ");

            String NewName = Console.ReadLine();

            var Contacts = await Client.GetContactsAsync();

            //Where - фильтрация по заданному предикату
            var Friend = Contacts.Users.Lists.Where(x => x.GetType() == typeof(TlUser)).Cast<TlUser>().FirstOrDefault(x => x.FirstName == NewName);

            if (Friend == null)
            {
                Console.WriteLine("\nТакого пользователя не существует");
                Console.ReadLine();
                return false;
            }

            //здесь открывается доступ к истории переписки с каким-либо контактом
            //параметры: целевой пользователь или группа, offset - число пропущенных элементов, MaxId - вернутся те сообщения, ID которых < MaxId, limit - число получаемых элементов
            var HistoryMessages = await Client.GetHistoryAsync(new TlInputPeerUser() { UserId = Friend.Id }, 0, 1000, 100) as TlMessages;
            
            if (HistoryMessages != null)
                foreach (var item in HistoryMessages.Messages.Lists)
                {
                    var a = item as TlMessage;
                    //исходящее сообщение
                    if (a.Out) Console.WriteLine("You:" + " \t" + a.Message);
                    else       Console.WriteLine(Friend.FirstName + ": \t" + a.Message);
                    
                }
            else Console.WriteLine("\nНет сообщений");

            Console.Write("\nДля продолжения нажмите клавишу Enter\n");

            Console.ReadLine();

            return true;
        }

    }
}
