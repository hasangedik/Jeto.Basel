using System;
using System.Collections.Generic;

namespace Jeto.Basel.Domain.Messages.Responses
{
    [Serializable]
    public record BaseResponse
    {
        /// <summary>
        /// For response contains error
        /// </summary>
        public bool HasError { get; init; }
        
        /// <summary>
        /// Error code for service owners. We do not need any error code for response, but
        /// it is easy to determine where the error occures when there is a code for it.
        /// </summary> 
        public string ErrorCode { get; init; }

        /// <summary>
        /// Error messages
        /// </summary>
        public List<string> ErrorMessages { get; init; } = new();

        /// <summary>
        /// Error message for the return value. If UserFriendly value is true then we print
        /// this message to user else we print a default error message.
        /// </summary>
        public string Message { get; init; }
        
        public object Result { get; init; }
    }
}