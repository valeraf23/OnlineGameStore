using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineGameStore.Common.Errors
{
    public class ArgumentNullError : Error
    {
        private readonly string _msg;
        public string Message => _msg;

        public ArgumentNullError(string msg)
        {
            _msg = msg;
        }
    }
}
