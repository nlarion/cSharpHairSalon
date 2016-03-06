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
      //Stylist Views
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
        List<Client> clientList = newStylist.GetClients();
        Dictionary<string,object> myDictionary = new Dictionary<string,object>{};
        myDictionary.Add("stylist",newStylist);
        myDictionary.Add("clients",clientList);
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
      //Client Views
      Get["/Client"] = _ => {
        return View["client.cshtml", Client.GetAll()];
      };
      Post["/Client"] =_=> {
        DateTime newDateTime = Convert.ToDateTime((string)Request.Form["date"]);
        Client newClient = new Client(Request.Form["name"], newDateTime, Request.Form["phone"], Request.Form["email"], Request.Form["stylist"]);
        newClient.Save();
        return View["client.cshtml",Client.GetAll()];
      };
      Get["/Client/{id}"]  = parameters => {
        List<Stylist> stylistList = Stylist.GetAll();
        Client newClient = Client.Find(parameters.id);
        Dictionary<string,object> myDictionary = new Dictionary<string,object>{};
        myDictionary.Add("stylist",stylistList);
        myDictionary.Add("client",newClient);
        return View["clientView.cshtml",myDictionary];
      };
      Post["/Client/Update/{id}"]  = parameters => {
        Client newClient = Client.Find(parameters.id);
        DateTime newDateTime = Convert.ToDateTime((string) Request.Form["date"]);
        newClient.Update(Request.Form["name"], newDateTime, Request.Form["phone"], Request.Form["email"], Request.Form["stylist"]);
        return View["client.cshtml",Client.GetAll()];
      };
      Get["/Client/Delete/{id}"]  = parameters => {
        Client newClient = Client.Find(parameters.id);
        newClient.Delete();
        return View["client.cshtml",Client.GetAll()];
      };
      Get["/Client/Create"] = _ => {
        return View["clientCreate.cshtml", Stylist.GetAll()];
      };
      Get["/Client/Delete"] = _ => {
        Client.DeleteAll();
        return View["client.cshtml",  "delete"];
      };
    }
  }
}