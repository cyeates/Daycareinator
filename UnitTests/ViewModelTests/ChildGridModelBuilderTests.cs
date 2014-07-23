using Daycareinator.Data.Entities;
using Daycareinator.Models.Children;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.ViewModelTests
{
    [TestClass]
    public class ChildGridModelBuilderTests
    {
        private List<Record> _records;
        private ChildGridModelBuilder _builder;

        [TestInitialize]
        public void SetUp()
        {
            _records = new List<Record> { new Record { Description = "Test1" }, new Record { Description = "Test2" } };
            _builder = new ChildGridModelBuilder(_records);
        }
        
        [TestMethod]
        public void HasErrorWhenChildDoesntHaveAllRecords()
        {
           
            var child = new Child { ChildId = 1, FirstName = "Bart", LastName = "Simpson", IsActive = true, Records = new List<ChildRecord>() };
            var model = _builder.Build(child);

            Assert.AreEqual("error", model.RowClass);
        }

        [TestMethod]
        public void HasErrorWhenRecordsIsNull()
        {
            var child = new Child { ChildId = 1, FirstName = "Bart", LastName = "Simpson", IsActive = true };
            var model = _builder.Build(child);

            Assert.AreEqual("error", model.RowClass);
        }

        [TestMethod]
        public void DoesNotContainErrorWhenChildHasAllRecords()
        {
            var child = new Child 
                            { 
                                ChildId = 1, 
                                FirstName = "Bart", 
                                LastName = "Simpson", 
                                IsActive = true, 
                                Records = new List<ChildRecord>{ new ChildRecord{RecordId = 1, DisplayFileName = "adf" },new ChildRecord{RecordId = 2, DisplayFileName = "afasdf"}}
                             };

            
            var model = _builder.Build(child);

            StringAssert.Equals(String.Empty, model.RowClass);

            
        }
    }
}
