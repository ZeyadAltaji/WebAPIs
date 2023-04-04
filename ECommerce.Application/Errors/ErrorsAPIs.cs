using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Errors
{
    public class ErrorsAPIs
    {
        public ErrorsAPIs(int Error_Code, string Error_Messages, string Errors_Details = null)
        {
            this.Errors_Details = Errors_Details;
            this.Error_Code = Error_Code;
            this.Error_Messages = Error_Messages;
            
        }
        public int Error_Code { get; set; }
        public string Error_Messages { get; set; }
        public string Errors_Details { get; set; }
    }
}
