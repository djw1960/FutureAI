using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EF.Common.DI
{
    using Spring.Context;
    /// <summary>
    /// 对象工厂：负责根据配置文件，通过反射的方式实例化对象，内部使用由Spring.net来完成
    /// </summary>
    public static class ObjectFactory
    {
        //1.0声明并创建一个Spring.net上下文容器对象（对象工厂）
        static IApplicationContext ctx = null;
        static ObjectFactory()
        {
            ctx = Spring.Context.Support.ContextRegistry.GetContext();
        }
        /// <summary>
        /// 根据传入的对象键，反射创建一个对象返回
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objKey"></param>
        /// <returns></returns>
        public static T GetObj<T>(string objKey)
            where T : class
        {
            //2.0调用Spring.net的上下文容器对象（对象工厂）来读取配置文件，并创建对象返回
            return (T)ctx.GetObject(objKey);
        }
    }
}
