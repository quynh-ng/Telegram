using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TelegramClient.Entities;
using TelegramClient.Core;
using TelegramClient;
using LightInject;
using TelegramClient.Entities.TL;
using TelegramClient.Entities.TL.Account;
using TelegramClient.Core.Settings;

namespace Telegram_NEW
{
    //интерфейс пользователя
    public interface IUser
    {
        //свойства пользователя
        string MyPhone { get; set; }                //номер моб.телефона
        string FirstName { get; set; }              //имя пользователя
        string LastName  { get; set; }              //фамилия пользователя
        Int32 ID { get; set; }                      //уникальный ID: long || String
        List<IContact> ListContacts { get; set; }   //список контактов
    }

    //интерфейс отправки сообщений пользователю (поиск по номеру телефона) и группе пользователей (поиск по названию беседы)
    public interface ISendMessage
    {
        bool SendMessageUser (string Text, string Phone);
        bool SendMessageGroup(string Text, string Name);
    }

    //интерфейс обновления состояния приложения
    public interface IUpdateMessager
    {
        bool Update();
    }

    //интерфейс удаления и добавления контактов
    public interface IOperationWithContact
    {
        bool NewContact(string Name, string Phone);
        bool DeleteContact(string Name, string Phone);
    }

    //интерфейс для поиска контактов
    public interface IFindContact
    {
        List<IContact> FindContacts(string Name);  //найти группу пользователей (беседа)
        IContact FindContact(string Phone);        //поиск контанкта по номеру телефона
    }

    //интерфейс контакта
    public interface IContact
    {
        string Name { get; set; }
        bool Status { get; set; }
        string PhoneNumber { get; set; }
        List<IMessage> Message { get; set; }
    }

    //интерфейс сообщения
    public interface IMessage
    {
        string Text { get; set; }
        string NameFrom { get; set; }
    }

    //текущий пользователь
    public class User : IUser
    {
        public string MyPhone { get; set; }                //номер моб.телефона
        public string FirstName { get; set; }              //имя пользователя
        public string LastName { get; set; }               //фамилия пользователя
        public Int32 ID { get; set; }                      //уникальный ID
        public List<IContact> ListContacts { get; set; }   //список контактов
    }

    class Program
    {
        static void Main()
        {
            var S = new SimpleIoC();
            //регистрация приложения
            S.Register<IApplicationFixic, ApplicationFixic>();
            var App = S.Resolve<IApplicationFixic>();
            Task T = App.StartApplication();
            T.Wait();
        }
    }
}
