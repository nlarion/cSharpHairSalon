using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace HairSalon
{
  public class ClientTest : IDisposable
  {
    public ClientTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=hair_salon_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_Empty_DBIsEmpty()
    {
      //Arrange//Act
      int result = Client.GetAll().Count;
      //Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Equal_ReturnsTrueForSameName()
    {
      //Arrange, Act
      Client firstClient = new Client("Johnny", new DateTime(1984, 9, 3),"555-555-5555","john@johnny.com",1);
      Client secondClient = new Client("Johnny", new DateTime(1984, 9, 3),"555-555-5555","john@johnny.com",1);
      //Assert
      Assert.Equal(firstClient, secondClient);
    }

    [Fact]
    public void Test_Save_SavesClientToDatabase()
    {
      //Arrange
      Client testClient = new Client("Johnny", new DateTime(1984, 9, 3),"555-555-5555","john@johnny.com",1);
      testClient.Save();
      //Act
      List<Client> result = Client.GetAll();
      List<Client> testList = new List<Client>{testClient};
      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_GetAll_GetAllClientsInDatabase()
    {
      //Arrange
      Client testClient1 = new Client("Johnny", new DateTime(1984, 9, 3),"555-555-5555","john@johnny.com",1);
      testClient1.Save();
      Client testClient2 = new Client("Matty", new DateTime(1984, 9, 3),"555-555-5555","john@johnny.com",1);
      testClient2.Save();
      //Act
      List<Client> result = Client.GetAll();
      List<Client> testList = new List<Client>{testClient1,testClient2};
      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_DeleteAll_DeleteAllClientsInDatabase()
    {
      //Arrange
      Client testClient = new Client("Johnny", new DateTime(1984, 9, 3),"555-555-5555","john@johnny.com",1);
      testClient.Save();
      //Act
      Client.DeleteAll();
      int result = Client.GetAll().Count;
      //Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Find_FindsClientInDatabase()
    {
      //Arrange
      Client testClient = new Client("Johnny", new DateTime(1984, 9, 3),"555-555-5555","john@johnny.com",1);
      testClient.Save();
      //Act
      Client foundClient = Client.Find(testClient.GetId());
      //Assert
      Assert.Equal(testClient, foundClient);
    }

    [Fact]
    public void Test_Update_UpdateClientInDatabase()
    {
      //Arrange
      Client testClient = new Client("Johnny", new DateTime(1984, 9, 3),"555-555-5555","john@johnny.com",1);
      testClient.Save();
      //Act
      testClient.Update("Scotty", new DateTime(1984, 9, 3),"555-555-5555","john@johnny.com",1);
      //Assert
      Assert.Equal("Scotty", testClient.GetName());
    }

    [Fact]
    public void Test_Update_UpdateClientInDatabaseAndMatchAllAttributes()
    {
      //Arrange
      Client testClient = new Client("Johnny", new DateTime(1984, 9, 3),"555-555-5555","john@johnny.com",1);

      //Act
      testClient.Update("Scotty", new DateTime(1988, 9, 3),"KLF-555-5555","scotty@scotty.com",1);
      Client anotherTestClient = new Client("Scotty", new DateTime(1988, 9, 3),"KLF-555-5555","scotty@scotty.com",1);
      Console.WriteLine(anotherTestClient.GetDateTime());
      Console.WriteLine(testClient.GetDateTime());
      //Assert
      Assert.Equal(testClient, anotherTestClient);
    }

    [Fact]
    public void Test_Update_UpdateClientInDatabaseAndMatchAllAttributesWithGettersAndSetters()
    {
      //Arrange
      Client testClient = new Client("Johnny", new DateTime(1984, 9, 3),"555-555-5555","john@johnny.com",1);
      testClient.Save();
      //Act
      testClient.Update("Scotty", new DateTime(1988, 9, 3),"KLF-555-5555","scotty@scotty.com",1);

      //Assert
      Assert.Equal(true, (testClient.GetName()=="Scotty" && testClient.GetDateTime()==new DateTime(1988, 9, 3) && testClient.GetPhone()=="KLF-555-5555" && testClient.GetEmail()=="scotty@scotty.com"));
    }

    [Fact]
    public void Test_Delete_DeleteSingleClientsInDatabase()
    {
      //Arrange
      Client testClient1 = new Client("Johnny", new DateTime(1984, 9, 3),"555-555-5555","john@johnny.com",1);
      testClient1.Save();
      Client testClient2 = new Client("Johnny", new DateTime(1984, 9, 3),"555-555-5555","john@johnny.com",1);
      testClient2.Save();
      //Act
      testClient1.Delete();
      int result = Client.GetAll().Count;
      //Assert
      Assert.Equal(1, result);
    }

    public void Dispose()
    {
      Client.DeleteAll();
      //Client.DeleteAll();
    }
  }
}
