using System;
using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;

namespace HairSalon
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
        return View["index.cshtml"];
      };      
      Get["/Stylist"] = _ => {
        return View["stylist.cshtml",Stylist.GetAll()];
      };
    }
  }
}