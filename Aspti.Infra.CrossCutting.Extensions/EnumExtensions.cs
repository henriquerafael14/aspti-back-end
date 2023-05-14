using System.ComponentModel;
using System.Globalization;
using System.Web.Mvc;

namespace Aspti.Infra.CrossCutting.Extensions
{
	public static class EnumExtensions
	{
		public static string GetDescription<T>(this T e) where T : IConvertible
		{
			string description = e.ToString();

			if (e is Enum)
			{
				Type type = e.GetType();
				Array values = Enum.GetValues(type);

				foreach (int val in values)
				{
					if (val == e.ToInt32(CultureInfo.InvariantCulture))
					{
						var memInfo = type.GetMember(type.GetEnumName(val));
						var descriptionAttributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
						if (descriptionAttributes.Length > 0)
						{
							description = ((DescriptionAttribute)descriptionAttributes[0]).Description;
						}

						break;
					}
				}
			}

			return description;
		}

		public static IEnumerable<SelectListItem> EnumToSelectList<T>() where T : IConvertible
		{
			return (Enum.GetValues(typeof(T)).Cast<T>()
				.Select(e => new SelectListItem()
				{
					Text = GetDescription(e),
					Value = (e.ToInt32(CultureInfo.InvariantCulture)).ToString()
				})).ToList();
		}
	}
}
