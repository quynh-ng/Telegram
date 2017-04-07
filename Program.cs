using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using TLSharp.Core;

namespace Telegram
{

    interface IClient
    {
        ISend Send { get; set; }
        IRecv Recv { get; set; }
        IConnect Connect { get; set; }
        IPrintOfContact PrintOfContact { get; set; }
        IAddConatact AddConatact { get; set; }

    }

    //отправляю сообщение, все пользователю или кому-то конкретно?
    interface ISend
    {
        
    }
    
    //отправить сообщение всем
    interface ISendAllUsers : ISend
    {

    }

    //отправить сообщение пользователю
    interface ISendUser : ISend
    {

    }

    //принимаю сообщение, автоматически или обновление по кнопки?
    interface IRecv
    {

    }

    
    //вопрос, проверка пароля, зачем? если у нас соединение усстановлено, то ок, иначе нет

    //устанавливаю соединение для работы с текущим API - телеграммом
    interface IConnect  //IPassAuthentication
    {

    }

    ////проверка пароля
    //interface IPassAuthentication
    //{
        
    //}
    
    //онлайн или всех, или вообще по фильтру, сделать как базовый класс 
    interface IPrintOfContact
    {
        
    }

    //вывести все конктаты
    interface IPrintOfAllContact: IPrintOfContact
    {

    }
    //вывести все контакты, которые онлайн
    interface IPrintOfOnlineContact : IPrintOfContact, IIsUserOnline
    {

    }

    //проверка онлайн ли пользователь?
    interface IIsUserOnline
    {
        
    }

    interface IAddConatact
    {
        
    }



    class Program
    {
        static void Main(string[] args)
        {
     //       var store = new FileSessionStore();
    //        var client = new TelegramClient(Convert.ToInt32(store), "session");
     //       client.ConnectAsync();
            
       ///     var userByPhoneId = await client.ImportContactByPhoneNumber("79130168187"); // импорт по номеру телефона
        //    var userByUserNameId = await await client.ImportByUserName("userName"); // импорт по юзернейму


        }
    }
}
