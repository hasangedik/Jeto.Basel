using System;

namespace Jeto.Basel.Common.Options
{
    [Serializable]
    public class MailServerOptions
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool UseSsl { get; set; }
        public string FromEmail { get; set; }
        public string FromName { get; set; }
        public string IsBlocked { get; set; }
    }
}