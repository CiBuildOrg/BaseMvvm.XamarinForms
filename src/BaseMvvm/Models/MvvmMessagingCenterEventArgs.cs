using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseMvvm.XamarinForms.Models
{
    /// <summary>
    /// custom event implementation of MvvmMessagingCenter 
    /// </summary>
    public class MvvmMessagingCenterEventArgs : EventArgs
    {
        /// <summary>
        /// ctor 
        /// </summary>
        /// <param name="messageId">
        /// type of MessagingCenterMessage class 
        /// </param>
        /// <param name="data">
        /// any object 
        /// </param>
        public MvvmMessagingCenterEventArgs(string messageId, object data)
        {
            this.MessageId = messageId;
            this.Event = data;
        }

        /// <summary>
        /// object which sets from user 
        /// </summary>
        public object Event { get; }

        /// <summary>
        /// type of MessagingCenterMessage class 
        /// </summary>
        public string MessageId { get; }
    }
}