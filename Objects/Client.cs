using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace HairSalon
{
  public class Client
  {
    private int _id;
    private string _name;
    private DateTime _dateTime;
    private string _phone;
    private string _email;
    private int _stylistId;

    public Client(string name, DateTime appointment, string phone, string email, int stylistId, int id=0)
    {
      _id = id;
      _name = name;
      _dateTime = appointment;
      _phone = phone;
      _email = email;
      _stylistId = stylistId;
    }
    public override bool Equals(System.Object otherClient)
    {
      if(!(otherClient is Client))
      {
        return false;
      }
      else
      {
        Client newClient = (Client) otherClient;
        bool nameEquals = this.GetName() == newClient.GetName();
        bool idEquals = this.GetId() == newClient.GetId();
        bool stylistIdEquals = this.GetStylistId() == newClient.GetStylistId();
        bool phoneEquals = this.GetPhone() == newClient.GetPhone();
        bool emailEquals = this.GetEmail() == newClient.GetEmail();
        bool dateEquals = this.GetDateTime() == newClient.GetDateTime();
        return (nameEquals && idEquals && stylistIdEquals && phoneEquals && emailEquals && dateEquals);
      }
    }
    public string GetName()
    {
      return _name;
    }
    public void SetName(string name)
    {
      _name = name;
    }
    public DateTime GetDateTime()
    {
      return _dateTime;
    }
    public void SetDateTime(DateTime dateTime)
    {
      _dateTime = dateTime;
    }
    public string GetPhone()
    {
      return _phone;
    }
    public void SetPhone(string phone)
    {
      _phone = phone;
    }
    public string GetEmail()
    {
      return _email;
    }
    public void SetEmail(string email)
    {
      _email = email;
    }
    public int GetStylistId()
    {
      return _stylistId;
    }
    public void SetStylistId(int stylistId)
    {
      _stylistId = stylistId;
    }
    public int GetId()
    {
      return _id;
    }
    public void SetId(int id)
    {
      _id = id;
    }
    public void Save()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("Insert INTO clients (name, appointment, phone, email, stylist_id) OUTPUT INSERTED.id VALUES (@ClientName, @ClientDate, @ClientPhone, @ClientEmail, @ClientStylistId);", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@ClientName";
      nameParameter.Value = this.GetName();
      cmd.Parameters.Add(nameParameter);

      SqlParameter appointmentParameter = new SqlParameter();
      appointmentParameter.ParameterName = "@ClientDate";
      appointmentParameter.Value = this.GetDateTime();
      cmd.Parameters.Add(appointmentParameter);

      SqlParameter phoneParameter = new SqlParameter();
      phoneParameter.ParameterName = "@ClientPhone";
      phoneParameter.Value = this.GetPhone();
      cmd.Parameters.Add(phoneParameter);

      SqlParameter emailParameter = new SqlParameter();
      emailParameter.ParameterName = "@ClientEmail";
      emailParameter.Value = this.GetEmail();
      cmd.Parameters.Add(emailParameter);

      SqlParameter stylistIdParameter = new SqlParameter();
      stylistIdParameter.ParameterName = "@ClientStylistId";
      stylistIdParameter.Value = this.GetStylistId();
      cmd.Parameters.Add(stylistIdParameter);

      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
    }
    public static List<Client> GetAll()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      List<Client> myListClient = new List<Client>{};

      SqlCommand cmd = new SqlCommand("SELECT * FROM clients;", conn);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        DateTime dateTime = rdr.GetDateTime(2);
        string phone = rdr.GetString(3);
        string email = rdr.GetString(4);
        int stylistId = rdr.GetInt32(5);
        Client newClient = new Client(name, dateTime, phone, email, stylistId, id);
        myListClient.Add(newClient);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
      return myListClient;
    }
    public static Client Find(int id)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM clients WHERE id = @ClientId;", conn);
      SqlParameter ClientIdParameter = new SqlParameter();
      ClientIdParameter.ParameterName = "@ClientId";
      ClientIdParameter.Value = id.ToString();
      cmd.Parameters.Add(ClientIdParameter);
      rdr = cmd.ExecuteReader();

      int foundClientId = 0;
      string foundClientName = null;
      DateTime foundDateTime = new DateTime(2016,1,1);
      string foundClientPhone = null;
      string foundClientEmail = null;
      int foundClientSytlistId = 0;

      while(rdr.Read())
      {
        foundClientId = rdr.GetInt32(0);
        foundClientName = rdr.GetString(1);
        foundDateTime = rdr.GetDateTime(2);
        foundClientPhone = rdr.GetString(3);
        foundClientEmail = rdr.GetString(4);
        foundClientSytlistId = rdr.GetInt32(5);
      }
      Client foundClient = new Client(foundClientName, foundDateTime, foundClientPhone, foundClientEmail, foundClientSytlistId, foundClientId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundClient;
    }
    public void Update(string newName, DateTime newAppointment, string newPhone, string newEmail, int newStylistId)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE clients SET name = @ClientName, appointment = @ClientDate, phone = @ClientPhone, email = @ClientEmail, stylist_id = @ClientStylistId OUTPUT INSERTED.name, INSERTED.appointment, INSERTED.phone, INSERTED.email, INSERTED.stylist_id WHERE id = @Id;", conn);
      SqlParameter ClientNameParameter = new SqlParameter();
      ClientNameParameter.ParameterName = "@ClientName";
      ClientNameParameter.Value = newName;
      cmd.Parameters.Add(ClientNameParameter);

      SqlParameter ClientDateParameter = new SqlParameter();
      ClientDateParameter.ParameterName = "@ClientDate";
      ClientDateParameter.Value = newAppointment.ToString("d");
      cmd.Parameters.Add(ClientDateParameter);

      SqlParameter clientPhoneParameter = new SqlParameter();
      clientPhoneParameter.ParameterName = "@ClientPhone";
      clientPhoneParameter.Value = newPhone;
      cmd.Parameters.Add(clientPhoneParameter);

      SqlParameter clientEmailParameter = new SqlParameter();
      clientEmailParameter.ParameterName = "@ClientEmail";
      clientEmailParameter.Value = newEmail;
      cmd.Parameters.Add(clientEmailParameter);

      SqlParameter clientStylistIdParameter = new SqlParameter();
      clientStylistIdParameter.ParameterName = "@ClientStylistId";
      clientStylistIdParameter.Value = newStylistId;
      cmd.Parameters.Add(clientStylistIdParameter);

      SqlParameter stylistIdParameter = new SqlParameter();
      stylistIdParameter.ParameterName = "@Id";
      stylistIdParameter.Value = this.GetId();
      cmd.Parameters.Add(stylistIdParameter);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._name = rdr.GetString(0);
        this._dateTime = rdr.GetDateTime(1);
        this._phone = rdr.GetString(2);
        this._email = rdr.GetString(3);
        this._stylistId = rdr.GetInt32(4);
      }

      if (rdr != null)
      {
        rdr.Close();
      }

      if (conn != null)
      {
        conn.Close();
      }

    }
    public void Delete()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM clients where id = @Id;", conn);

      SqlParameter stylistIdParameter = new SqlParameter();
      stylistIdParameter.ParameterName = "@Id";
      stylistIdParameter.Value = this.GetId();
      cmd.Parameters.Add(stylistIdParameter);
      cmd.ExecuteNonQuery();
    }
    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM clients;", conn);
      cmd.ExecuteNonQuery();
    }
  }
}
