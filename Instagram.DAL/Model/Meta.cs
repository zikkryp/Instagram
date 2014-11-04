using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.DAL.Model
{
    public class Meta
    {
        public Meta(int code)
        {
            this.Code = code;
        }

        public Meta(int code, string errorType, string errorMessage)
        {
            this.Code = code;
            this.ErrorType = errorType;
            this.ErrorMessage = errorMessage;
        }

        public int Code { get; private set; }
        public string ErrorType { get; private set; }
        public string ErrorMessage { get; private set; }

        public override string ToString()
        {
            return this.Code.ToString();
        }
    }
}
