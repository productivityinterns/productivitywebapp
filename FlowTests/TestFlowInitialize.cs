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
