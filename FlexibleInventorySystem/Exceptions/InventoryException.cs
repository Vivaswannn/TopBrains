using System;

namespace FlexibleInventorySystem.Exceptions
{
    public class InventoryException : Exception
    {
        public string ErrorCode { get; }

        public InventoryException() : base()
        {
        }

        public InventoryException(string message) : base(message)
        {
        }

        public InventoryException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public InventoryException(string message, string errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }

        public override string Message
        {
            get
            {
                if (!string.IsNullOrEmpty(ErrorCode))
                {
                    return $"[{ErrorCode}] {base.Message}";
                }
                return base.Message;
            }
        }
    }
}
