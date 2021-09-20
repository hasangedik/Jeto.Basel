using System;
using System.Collections.Generic;

namespace Jeto.Basel.Domain.Contracts
{
    [Serializable]
    public class EmailContract
    {
        public List<string> To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}