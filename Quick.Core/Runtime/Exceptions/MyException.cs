using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Quick.Core.Runtime.Exceptions
{
    public class MyException : Exception
    {
        public MyException() { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public MyException(string message) : base(message)
        {
            ErrorMessage = message;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected MyException(SerializationInfo info, StreamingContext context)
        {
            ErrorMessage = info.GetString("ErrorMessage");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("ErrorMessage", ErrorMessage);
        }

        /// <summary>
        /// 异常信息
        /// </summary>
        public string ErrorMessage { get; private set; }
    }
}
