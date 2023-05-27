using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aspti.Infra.CrossCutting.Filters
{
	public class InternationalizationAttribute : ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			string language = (string)filterContext.RouteData.Values["language"] ?? "pt";
			string culture = (string)filterContext.RouteData.Values["culture"] ?? "BR";

			CultureInfo cultureInfo = CultureInfo.CreateSpecificCulture(string.Format("{0}-{1}", language, culture));

			Thread.CurrentThread.CurrentCulture = cultureInfo;
			Thread.CurrentThread.CurrentUICulture = cultureInfo;
			CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
			CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
		}
	}
}
