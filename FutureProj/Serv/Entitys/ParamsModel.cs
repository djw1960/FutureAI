using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serv.Entitys
{
    public class BindCardModel
    {
        //绑卡四要素-身份证号
        public string IDCard { get; set; }
        //绑卡四要素-姓名
        public string UserName { get; set; }
        //绑卡四要素-银行卡号
        public string BankCard { get; set; }
        //绑卡四要素-手机号
        public string Mobile { get; set; }
    }
}
