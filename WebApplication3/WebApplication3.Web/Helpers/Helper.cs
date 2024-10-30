using System;

namespace WebApplication3.Web.Helpers;
 
public class Helper : IHelper
{
    public string Upper(string text){
        return text.ToUpper();
    }

}
