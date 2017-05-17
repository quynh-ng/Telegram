using System.Threading.Tasks;

namespace Telegram_NEW
{
    class Program
    {
        static void Main()
        {
            var S = new SimpleIoC();
            //регистрация приложения
            S.Register<IApplicationFixic, ApplicationFixic>();
            //регистрация пользователя
            S.Register<IUser, User>();
            //регистрация сервиса для обмена сообщениями
            S.Register<IMessageService, MessageService>();
            //регистрация сервиса для работы с контактами
            S.Register<IContactService, ContactService>();
            //разрешаем типы внутри приложения
            var App = S.Resolve<IApplicationFixic>();
            App.I   = S.Resolve<IUser>();
            App.MessServ = S.Resolve<IMessageService>();
            App.ContServ = S.Resolve<IContactService>();
            //запуск приложения
            Task T = App.StartApplication();
            T.Wait();
        }
    }
}
