using System;
using System.Net;

namespace WebApplication3.Web.Middlewares;

public class WhiteIPAdressControlMiddleware
{
    private const string WhiteIPAddress="::1";
    private readonly RequestDelegate _requestDelegate;

    public WhiteIPAdressControlMiddleware(RequestDelegate requestDelegate)
    {
        _requestDelegate = requestDelegate;
    }

    public async Task InvokeAsync(HttpContext context){
        //IPv4= 127.0.0.1 =localhost
        //IPv6= ::1 = localhost

        //KULLANICINDA GELEN IP ADRESİNİ AL
        var RequestIPAddress=context.Connection.RemoteIpAddress;

        //ADMİNİN IP ADRESİYLE KULLANICININKİNİ KARŞILAŞTIR
        bool AnyWhiteIPAddress=IPAddress.Parse(WhiteIPAddress).Equals(RequestIPAddress);


        //DOĞRUYSA NEXT();
        if(AnyWhiteIPAddress){
            await _requestDelegate(context);
        }

        //DEĞİLSE 403 FORBIDDEN GÖNDER
        else{
            context.Response.StatusCode=HttpStatusCode.Forbidden.GetHashCode();
            await context.Response.WriteAsync("FORBIDDEN");
        }
    }
}
