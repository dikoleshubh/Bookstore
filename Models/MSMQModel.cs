using Experimental.System.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class MSMQModel
    {
        public string JwtToken { get; set; }
        public string Email { get; set; }
    }


    
    public class Sender
    {
        public void SendMessage()
        {
            var url = "Click on following link to reset your credentials for Fundoonotes App: https://localhost:44387/api/User/ResetPassword";
            MessageQueue msmqQueue = new MessageQueue();
            if (MessageQueue.Exists(@".\Private$\MyQueue"))
            {
                msmqQueue = new MessageQueue(@".\Private$\MyQueue");
            }
            else
            {
                msmqQueue = MessageQueue.Create(@".\Private$\MyQueue");
            }
            Message message = new Message();
            message.Formatter = new BinaryMessageFormatter();
            message.Body = url;
            msmqQueue.Label = "url link";
            msmqQueue.Send(message);
        }
    }

    public class Receiver
    {
        public string receiverMessage()
        {
            MessageQueue reciever = new MessageQueue(@".\Private$\MyQueue");
            var recieving = reciever.Receive();
            recieving.Formatter = new BinaryMessageFormatter();
            string linkToBeSend = recieving.Body.ToString();
            return linkToBeSend;
        }
    }
   


}
