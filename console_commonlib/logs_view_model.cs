using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using console_dto;

namespace console_commonlib
{
    public class logs_view_model
    {
        public IEnumerable<log_dto> lst_dto { get; set; }
        public log_dto dto { get; set; }
    }
}