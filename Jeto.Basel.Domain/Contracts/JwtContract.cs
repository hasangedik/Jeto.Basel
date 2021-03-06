using System;

namespace Jeto.Basel.Domain.Contracts
{
    public interface IJwtContract
    {
        public int Id { get; set; }
    }
    
    [Serializable]
    public class JwtContract : IJwtContract
    {
        public int Id { get; set; }
    }
}