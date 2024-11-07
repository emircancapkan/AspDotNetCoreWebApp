using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApplication3.Web.Filters;

public class LogFilter:ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        Debug.WriteLine("Action method executing");
    }

    public override void OnActionExecuted(ActionExecutedContext context)
    {
        Debug.WriteLine("Action method executed");
    }

    public override void OnResultExecuting(ResultExecutingContext context)
    {
        Debug.WriteLine("Action method working on to give the result");
    }

    public override void OnResultExecuted(ResultExecutedContext context)
    {
        Debug.WriteLine("Action method gave the result");
    }
}

