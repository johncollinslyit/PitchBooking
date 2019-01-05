using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBLibrary;
using Xunit;

namespace DBLibrary.Tests
{
    public class DBWriteTests
    {
       [Fact]
      public void ValidateLogWrite()
        {
                    
       //Arrange
        DBWrite dBwrite = new DBWrite();
        bool expected = true;

       //Act
            bool actual = dBwrite.CreateLogEntry("test","test",1,"jcollins");

       //Assert
            Assert.Equal(expected,actual);        
       }

    }
}
