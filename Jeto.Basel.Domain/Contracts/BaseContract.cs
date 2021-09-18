using System.Collections.Generic;

namespace Jeto.Basel.Domain.Contracts
{
    public class BaseContract
    {
        /// <summary>
        /// For response contains error
        /// </summary>
        public bool HasError { get; set; }
        
        /// <summary>
        /// Error code for service owners. We do not need any error code for response, but
        /// it is easy to determine where the error occures when there is a code for it.
        /// </summary> 
        public string ErrorCode { get; set; }

        /// <summary>
        /// Error messages
        /// </summary>
        public List<string> ErrorMessages { get; set; } = new();

        /// <summary>
        /// Error message for the return value. If UserFriendly value is true then we print
        /// this message to user else we print a default error message.
        /// </summary>
        public string Message { get; set; }
    }
}