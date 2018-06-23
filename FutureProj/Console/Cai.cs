using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Future.Console
{
    /// <summary>
    /// 足彩研究
    /// </summary>
    public class Cai
    {
        public static void G()
        {
            double x = 1.2;
            double y = 5;
            double z = 9;
            double xzhu = y * z, yzhu = x * z, zzhu = x * y;
            double cben = xzhu + yzhu + zzhu;
            System.Console.WriteLine("盘口：X-{0}  Y-{1}  Z-{2}", x, y, z);
            System.Console.WriteLine("投注比例：X-{0}  Y-{1} Z-{2}", xzhu, yzhu, zzhu);
            System.Console.WriteLine("收益：X赢:{0} ", xzhu * x - cben);
            System.Console.WriteLine("收益：Y赢:{0} ", yzhu * y - cben);
            System.Console.WriteLine("收益：Z赢:{0} ", zzhu * z - cben);
            System.Console.WriteLine("----------------------------------------");
            for (double i = 0; i <= xzhu; i = i + 0.1)
            {
                for (double k = 0; k <= yzhu; k = k + 0.1)
                {
                    for (double m = 0; m <= zzhu; m = m + 0.1)
                    {

                    }
                }
            }

            for (int i = 0; i < 100; i++)
            {
                xzhu = xzhu + 0.1;
                System.Console.WriteLine("xzhu:{0}", xzhu);
                cben = xzhu + yzhu + zzhu;
                System.Console.WriteLine("收益：X赢:{0} ", xzhu * 2 * x - cben * 2);
                System.Console.WriteLine("收益：Y赢:{0} ", yzhu * 2 * y - cben * 2);
                System.Console.WriteLine("收益：Z赢:{0} ", zzhu * 2 * z - cben * 2);
                System.Console.WriteLine("------------------------------------");
            }
        }
    }
}
