using System;

namespace Jeto.Basel.Domain.Contracts
{
    [Serializable]
    public class SmtpContract
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string EmailAddress { get; set; }
        public bool UseSsl { get; set; }
    }
}