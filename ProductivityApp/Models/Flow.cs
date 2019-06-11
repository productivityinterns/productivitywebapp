using ProductivityApp.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class Flow {
  
    //Fields:: Setters and getters
    //unique id for filled flows
    public Guid Id {get;set;}
    public string name {set; get;}
    public string Description {get;set;}
    public Survey inputSurvey {set; get;}
    public IList<Assignment> assignments {set; get;}
    public IList<Criteria> criteria {set; get;}
    public IList<Destination> destinations {set; get;}

    //Constructor
    public Flow() {

    }
    public Flow(string name, Survey survey) {
        this.name = name;
        this.inputSurvey = survey;
    }
    public Flow initializeFlow() {
        //copy templates
        Flow newFlow = CloneTemplate();
        newFlow.AssignNewGuidIds();
        //we created the flow right now...
        newFlow.inputSurvey.timeCreated = DateTime.Now;
        //we'll figure out how to add the user later..
        newFlow.inputSurvey.user = "we don't get the userid right now";
        //Get from database -- we already have the template from db, so this step probably doesn't need to do anything -MG

        //copy forms 
        //get form paths

        //return new Flow
        return newFlow;  //not yet saved to db!
    }
    /// <summary>
    /// Clone a flow by serializing/deserializing and then resetting all Id's to new ids
    /// </summary>
    /// <returns></returns>
    public Flow CloneTemplate()
    {
        Flow clonedFlow = CopyHelpers.Clone<Flow>(this);
        //now set all guid id's to new id's. There are fancy ways to do this, but let's jsut get it done and worry about that another time. -MG
        //we could for example tag stuff with [JsonIgnore] and then use the jsonserializers. but we might want to actually send the flow id later in json in the webapp

        //we might also consider just using a combination of parent id and unique-to-this-flow but again this is probably aside from trying to get this done ASAP. Once we know the mechanics of it
        //this gets a lot easier and we can approach it with a "I've done this before" attitude!
        // ok, i started doing below, and it felt realy wrong, so I will try to do this in reflection. -MG
        /*
        clonedFlow.Id = Guid.NewGuid();
        clonedFlow.inputSurvey.Id = Guid.NewGuid();
        foreach(var field in clonedFlow.inputSurvey.fields)
        {
            field.Id = Guid.NewGuid();
        }
        foreach(var assignment in clonedFlow.assignments)
        {
            assignment.Id = Guid.NewGuid();
            assignment.inputField.Id = Guid.NewGuid();
        }
        foreach(var criteria in clonedFlow.criteria)
        {
            criteria.Id = Guid.NewGuid();
            foreach(var ca in criteria.answers)
            {
                ca.Id = Guid.NewGuid();
            }
        }
        foreach(var dest in clonedFlow.destinations)
        {
            dest.Id = Guid.NewGuid();
        }
        ....that was way too wordy, see body of InitializeFlow for inefficient yet way less manual-intensive reflection method
         */

        
        return clonedFlow;
    }
    //Since this is an instance method, you already have the template (this object) and don't need a parameter! -MG
    public Flow copyFromTemplate(Flow template) {
        Flow newFlow = new Flow();
        newFlow.name = template.name;
        newFlow.inputSurvey = template.inputSurvey;
        newFlow.assignments = template.assignments;
        newFlow.criteria = template.criteria;
        newFlow.destinations = template.destinations;
        return newFlow;
    }
    public bool checkFilter(Filter filter) {

        foreach (Criteria criterion in this.criteria)
        {   
            if ( criterion.Category == filter.name) {
                //find the answer's name that matches the filter's value and determine if it is selected
                var answer = criterion.SelectedValue;
                
                //if answer is null, we didn't find a match, don't apply the filter
                if (!String.IsNullOrEmpty(answer))
                {
                    return true;
                }
                else
                {
                    return false;                    
                }
                
            }
        }
        return false;
    }
}