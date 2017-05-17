using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TelegramClient.Core;
using TelegramClient.Entities.TL;

namespace Telegram_NEW
{
    //интерфейс сервиса контактов 
    public interface IContactService
    {
        Task Service();                                //предоставляемый сервис
        bool NewContact(string Name, string Phone);    //добавить новый контакт
        bool DeleteContact(string Name, string Phone); //удалить контакт
        Task PrintContacts();                          //распечатать все доступные контакты
        List<IContact> FindContacts(string Name);      //найти группу пользователей (беседа)
        IContact FindContact(string Phone);            //поиск контанкта по номеру телефона
        ITelegramClient Client { set; get; }           //методы для обработки контактов
    }

    class ContactService : IContactService
    {
        public ITelegramClient Client { set; get; }

        //предоставляет сервис по работе с контактами
        public async Task Service()
        {
            Console.Clear();
            Int32 CASE = 0;

            //доступный сервис
            while (CASE != 4)
            {
                Console.Clear();
                Console.WriteLine("1.Показать контакты");
                Console.WriteLine("2.Удалить контакт");
                Console.WriteLine("3.Добавить контакт");
                Console.WriteLine("4.Вернуться");
                Console.Write("\nДействие: ");
                CASE = Convert.ToInt32(Console.ReadLine());

                switch (CASE)
                {
                    case 1: { await PrintContacts(); break; }
                    case 2: { break; }
                    case 3: { break; }
                    case 4: { break; }
                }

            }
        }

        //добавить новый контакт
        public bool NewContact(string Name, string Phone)
        {
            return true;
        }

        //удалить контакт
        public bool DeleteContact(string Name, string Phone)
        {
            return true;
        }

        //распечатать все доступные контакты
        public async Task PrintContacts()
        {
            //получение доступных контактов
            var Contacts = await Client.GetContactsAsync();
            if (Contacts != null)
            {
                Console.WriteLine("\nТелефон \t Имя \t \t Фамилия");
                foreach (var item in Contacts.Users.Lists)
                {
                    var a = item as TlUser;
                    Console.WriteLine("{0} \t {1} \t {2}", a.Phone, a.FirstName, a.LastName);
                }
            }
            else Console.WriteLine("\nНет доступных контактов");

            Console.Write("\nДля продолжения нажмите клавишу Enter\n");

            Console.ReadLine();
        }

        //найти группу пользователей (беседа)
        public List<IContact> FindContacts(string Name)
        {
            List<IContact> LC = null;
            return LC;
        }

        //поиск контанкта по номеру телефона
        public IContact FindContact(string Phone)
        {
            IContact Cont = null;
            return Cont;
        }
    }
}
