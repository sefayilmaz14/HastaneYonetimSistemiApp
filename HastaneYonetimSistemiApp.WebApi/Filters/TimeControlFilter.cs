using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HastaneYonetimSistemiApp.WebApi.Filters
{
    public class TimeControlFilter : ActionFilterAttribute
    {
        public string Startime { get; set; }
        public string EndTime { get; set; }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var now = DateTime.Now.TimeOfDay;

            Startime = "18:00";
            EndTime = "07:00";

            if (now >= TimeSpan.Parse(Startime) && now >= TimeSpan.Parse(EndTime))
            {
                base.OnActionExecuted(context);
            }
            else
            {
                context.Result = new ContentResult
                {
                    Content = "Bu saatler arasında randevu alınamamaktadır.",
                    StatusCode = 403
                    
                };

            }
        }
    }
}
