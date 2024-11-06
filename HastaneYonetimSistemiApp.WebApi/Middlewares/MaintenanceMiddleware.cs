using HastaneYonetimSistemiApp.Business.Operations.Setting;

namespace HastaneYonetimSistemiApp.WebApi.Middlewares
{
    public class MaintenanceMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        

        public MaintenanceMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
            
        }

        public async Task Invoke(HttpContext context)
        {
            var _settingService = context.RequestServices.GetRequiredService<ISettingService>();
            bool maintenanceMode = _settingService.GetMaintenanceState();

            if(context.Request.Path.StartsWithSegments("/api/auth/login") || context.Request.Path.StartsWithSegments("/api/setting"))
            {
                await _requestDelegate(context);
                return;
            }

            if (maintenanceMode)
            {
                await context.Response.WriteAsync("Şuanda hizmet verememekteyiz.");
            }
            else
            {
                await _requestDelegate(context);
            }
        }
    }
}
