using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Aspti.Infra.CrossCutting.Utils
{
	public static class UtilEnum
	{
        public static string ObtenhaDescricao<T>(this T enumValue)
        where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
                return null;

            var description = enumValue.ToString();
            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

            if (fieldInfo != null)
            {
                var attrs = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (attrs != null && attrs.Length > 0)
                {
                    description = ((DescriptionAttribute)attrs[0]).Description;
                }
            }

            return description;
        }
        public static IEnumerable<T> ObterValores<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }
    }
}
