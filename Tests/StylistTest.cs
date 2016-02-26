using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace HairSalon
{
  public class StylistTest : IDisposable
  {
    public StylistTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=hair_salon_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_Empty_DBIsEmpty()
    {
      //Arrange//Act
      int result = Stylist.GetAll().Count;
      //Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Equal_ReturnsTrueForSameName()
    {
      //Arrange, Act
      Stylist firstStylist = new Stylist("Johnny");
      Stylist secondStylist = new Stylist("Johnny");

      //Assert
      Assert.Equal(firstStylist, secondStylist);
    }

    [Fact]
    public void Test_Save_SavesStylistToDatabase()
    {
      //Arrange
      Stylist testStylist = new Stylist("Johnny");
      testStylist.Save();

      //Act
      List<Stylist> result = Stylist.GetAll();
      List<Stylist> testList = new List<Stylist>{testStylist};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_GetAll_GetAllStylistsInDatabase()
    {
      //Arrange
      Stylist testStylist1 = new Stylist("Johnny");
      testStylist1.Save();
      Stylist testStylist2 = new Stylist("Matty");
      testStylist2.Save();
      //Act
      List<Stylist> result = Stylist.GetAll();
      List<Stylist> testList = new List<Stylist>{testStylist1,testStylist2};
      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_DeleteAll_DeleteAllStylistsInDatabase()
    {
      //Arrange
      Stylist testStylist = new Stylist("Johnny");
      testStylist.Save();
      //Act
      Stylist.DeleteAll();
      int result = Stylist.GetAll().Count;
      //Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Find_FindsStylistInDatabase()
    {
      //Arrange
      Stylist testStylist = new Stylist("Johnny");
      testStylist.Save();
      //Act
      Stylist foundStylist = Stylist.Find(testStylist.GetId());
      //Assert
      Assert.Equal(testStylist, foundStylist);
    }

    [Fact]
    public void Test_Update_UpdateStylistInDatabase()
    {
      //Arrange
      string name = "Johnny";
      Stylist testStylist = new Stylist(name);
      testStylist.Save();
      //Act
      testStylist.Update("Mark");
      //Assert
      Assert.Equal("Mark", testStylist.GetName());
    }

    [Fact]
    public void Test_Delete_DeleteSingleStylistsInDatabase()
    {
      //Arrange
      Stylist testStylist1 = new Stylist("Johnny");
      testStylist1.Save();
      Stylist testStylist2 = new Stylist("Matty");
      testStylist2.Save();
      //Act
      testStylist1.Delete();
      int result = Stylist.GetAll().Count;
      //Assert
      Assert.Equal(1, result);
    }

    public void Dispose()
    {
      Stylist.DeleteAll();
      //Client.DeleteAll();
    }
  }
}
