using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telegram_NEW
{
    //интерфейс контакта
    public interface IContact
    {
        string Name { get; set; }
        bool Status { get; set; }
        string PhoneNumber { get; set; }
        List<IMessage> Message { get; set; }
    }

    class Contact : IContact
    {
        public string Name { get; set; }
        public bool Status { get; set; }
        public string PhoneNumber { get; set; }
        public List<IMessage> Message { get; set; }
    }
}
