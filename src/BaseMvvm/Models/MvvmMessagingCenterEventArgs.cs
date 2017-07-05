using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseMvvm.XamarinForms.Models
{
    public class MvvmMessagingCenterEventArgs : EventArgs
    {
        public MvvmMessagingCenterEventArgs(string messageId, object data)
        {
            this.MessageId = messageId;
            this.Event = data;
        }

        public string MessageId { get; }

        public object Event { get; }
    }
}