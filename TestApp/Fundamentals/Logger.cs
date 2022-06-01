using System;
using System.Collections.Generic;
using System.Text;

namespace TestApp
{
    public class Logger
    {
        public string LastMessage { get; private set; }

        public event EventHandler<DateTime> MessageLogged;

        public event EventHandler Started;

        public void Log(string message)
        {
            Started?.Invoke(this, EventArgs.Empty);

            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentNullException();

            SetLastMessage(message);

            // Write the log to a storage
            // ...

            MessageLogged?.Invoke(this, DateTime.UtcNow);
        }

       
        private void SetLastMessage(string message)
        {
            LastMessage = message;
        }
    }
}
