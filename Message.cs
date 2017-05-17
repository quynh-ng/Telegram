using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telegram_NEW
{
    //интерфейс сообщения
    public interface IMessage
    {
        string Text { get; set; }
        string NameFrom { get; set; }
    }

    class Message : IMessage
    {
        public string Text { get; set; }
        public string NameFrom { get; set; }
    }
}
