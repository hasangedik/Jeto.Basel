using System;

namespace Jeto.Basel.Domain.Contracts
{
    [Serializable]
    public class UserContract
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}