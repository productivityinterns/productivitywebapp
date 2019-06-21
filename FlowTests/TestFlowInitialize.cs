using ProductivityApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ProductivityApp.Tests
{
    public class TestFlowInitialize    
    {

        [Fact]
        public void TestFlowOrdering()
        {
            using(Database db = new Database())
            {
                var flow = db.GetSampleTemplates().Last();
                //perform ordering explicitly, and make sure stuff is in expected order!
                flow = db.OrderFlowItems(flow);   
                var firstCriteria = flow.criteria.FirstOrDefault();
                Assert.Equal("6a",firstCriteria.Category);
                var secondCriteria = flow.criteria.Skip(1).FirstOrDefault();
                Assert.Equal("Transfer Information",secondCriteria.Category);

                //and make sure if we forget to order the same test fails.
                flow = db.GetSampleTemplates().Last();
                secondCriteria = flow.criteria.Skip(1).FirstOrDefault();
                Assert.NotEqual("Transfer Information",secondCriteria.Category);


                //OK! Criteria are showing up correctly. Test fields.
                //get a fresh flow
                flow = db.OrderFlowItems(db.GetSampleTemplates().Last());
                
                //order should be: date, first, last
                var firstField = flow.inputSurvey.fields.FirstOrDefault();
                Assert.Equal("date",firstField.tag);
                var secondField = flow.inputSurvey.fields.Skip(1).FirstOrDefault();
                Assert.Equal("firstname",secondField.tag);
                var thirdField = flow.inputSurvey.fields.Skip(2).FirstOrDefault();
                Assert.Equal("lastname",thirdField.tag);


                //make sure its not true when unsorted.
                flow = db.GetSampleTemplates().Last();
                
                //order should be: date, first, last
                firstField = flow.inputSurvey.fields.FirstOrDefault();
                Assert.NotEqual("date",firstField.tag);
                secondField = flow.inputSurvey.fields.Skip(1).FirstOrDefault();
                Assert.NotEqual("firstname",secondField.tag);
                thirdField = flow.inputSurvey.fields.Skip(2).FirstOrDefault();
                Assert.NotEqual("lastname",thirdField.tag);



                //Now test answer sorting. 
                flow = db.OrderFlowItems(db.GetSampleTemplates().Last());
                
                firstCriteria = flow.criteria.FirstOrDefault();
                Assert.Equal("6a",firstCriteria.Category);
                var firstAnswer = firstCriteria.answers.FirstOrDefault();
                Assert.Equal("no",firstAnswer.value);

                //test inverse when sort is off
                flow = db.GetSampleTemplates().Last();
                firstCriteria = flow.criteria.Where(c=>c.Category == "6a").FirstOrDefault();
                Assert.Equal("6a",firstCriteria.Category);
                firstAnswer = firstCriteria.answers.FirstOrDefault();
                Assert.NotEqual("no",firstAnswer.value);
            }
        }

        [Fact]
        public void TestInitializeCopiesValues()
        {
            using(Database db = new Database())
            {
                var flow = db.GetSampleTemplates().Last();
                //make sure we got a flow
                Assert.NotNull(flow);

                var newFlow = flow.initializeFlow();
                Assert.NotEqual(flow.Id, newFlow.Id);
                Assert.NotEqual(flow.inputSurvey.timeCreated, newFlow.inputSurvey.timeCreated);
                Assert.NotEqual(flow.inputSurvey, newFlow.inputSurvey);
                Assert.NotEqual(flow.inputSurvey.Id, newFlow.inputSurvey.Id);                
                //check all field id's changed
                for(int fieldIndex = 0;fieldIndex < flow.inputSurvey.fields.Count;fieldIndex++)
                {
                    Assert.NotEqual(flow.inputSurvey.fields[fieldIndex].Id, newFlow.inputSurvey.fields[fieldIndex].Id);
                }
                //and assignments
                for (int formIndex = 0; formIndex < flow.forms.Count; formIndex++)
                {
                    Assert.NotEqual(flow.forms[formIndex].Id, newFlow.forms[formIndex].Id);
                }
                //and criteria
                for (int criteriaIndex = 0; criteriaIndex < flow.criteria.Count; criteriaIndex++)
                {
                    Assert.NotEqual(flow.criteria[criteriaIndex].Id, newFlow.criteria[criteriaIndex].Id);
                }
                //finally, destinations
             
                    Assert.NotEqual(flow.destination.Id, newFlow.destination.Id);
              
            

            }
        }

    }
}
