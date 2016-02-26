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
      Post["/Stylist"] = _ => {
        Stylist newStylist = new Stylist(Request.Form["name"]);
        newStylist.Save();
        return View["stylist.cshtml", Stylist.GetAll()];
      };
      Get["/Stylist/{id}"]  = parameters => {
        Stylist newStylist = Stylist.Find(parameters.id);
        //List<Client> clientList = Client.FindByStylistId(newStylist.GetId());
        Dictionary<string,object> myDictionary = new Dictionary<string,object>{};
        myDictionary.Add("stylist",newStylist);
        //myDictionary.Add("clients",clientList);
        return View["StylistView.cshtml",myDictionary];
      };
      Post["/Stylist/Update/{id}"]  = parameters => {
        Stylist newStylist = Stylist.Find(parameters.id);
        newStylist.Update(Request.Form["name"]);
        return View["stylist.cshtml",Stylist.GetAll()];
      };
      Get["/Stylist/Delete/{id}"]  = parameters => {
        Stylist newStylist = Stylist.Find(parameters.id);
        newStylist.Delete();
        return View["stylist.cshtml",Stylist.GetAll()];
      };
      Get["/Stylist/Create"]  = _ => {
        return View["StylistCreate.cshtml"];
      };
      Get["/Stylist/Delete"] = _ => {
        Stylist.DeleteAll();
        return View["stylist.cshtml",  "delete"];
      };
    }
  }
}