using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    //пользователь
    public class User : IUser
    {
        public string MyPhone { get; set; }                //номер моб.телефона
        public string FirstName { get; set; }              //имя пользователя
        public string LastName  { get; set; }              //фамилия пользователя
        public Int32 ID { get; set; }                      //уникальный ID
        public List<IContact> ListContacts { get; set; }   //список контактов
    }
}
