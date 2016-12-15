using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;


namespace Oridashi.CDAAU.Core
{
    /// <summary>
    ///  helpers to access attribute values at runtime
    /// </summary>
    public static class Extensions
    {

        public static TExpected GetAttributeValue<T, TExpected>(this Enum enumeration, Func<T, TExpected> expression ) where T : Attribute
        {
            var attribute = enumeration.GetType().GetMember(enumeration.ToString())[0].GetCustomAttributes(typeof(T), false).Cast<T>().SingleOrDefault();

            return attribute == null ? default(TExpected) : expression(attribute);
        }

        public static TExpected GetAttributeValue<T, TExpected> (this object o, Func<T, TExpected> expression  )
          where T : Attribute
        {
            var attribute = o.GetType().GetCustomAttributes(typeof(T), false).Cast<T>().SingleOrDefault();

            return attribute == null ? default(TExpected) : expression(attribute);
        }

        public static TExpected GetAttributeValue<T, TExpected>(this PropertyInfo o, Func<T, TExpected> expression)
          where T : Attribute
        {
            var attribute = o.GetCustomAttributes(typeof(T), false).Cast<T>().SingleOrDefault();

            return attribute == null ? default(TExpected) : expression(attribute);
        }


        public static D GetAttributeValueII<T, D>(this object o)
            where D : new()
            where T : Attribute
        {
            dynamic item = o.GetType().GetCustomAttributes(typeof(T), false).Cast<T>().SingleOrDefault();

            if (item == null)
                return default(D);

            dynamic e = new D();
            e.root = item.Root;
            e.extension = item.Extension;

            return e;
        }


        public static D[] GetAttributeValuesII<T, D>(this object o) where D : new()
        where T : Attribute
        {
            List<D> output = new List<D>();
            foreach (dynamic item in o.GetType().GetCustomAttributes(typeof(T), false).Cast<T>())
            {
                dynamic e = new D();
                e.root = item.Root;
                e.extension = item.Extension;

                output.Add(e);
            }

            return output.Count==0?null:output.ToArray();
        }

        public static string ToFirstLower(this string o)
        {
            if (string.IsNullOrEmpty(o))
                return o;

            if (o.Length == 1)
                return o.ToLower();

            return o.Substring(0, 1).ToLower() + o.Substring(1);
        }
    }
}
