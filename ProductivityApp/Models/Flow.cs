using System;
using System.Collections.Generic;
using System.Linq;

public class Flow {
  
    //Fields:: Setters and getters
    public string name {set; get;}
    public Survey inputSurvey {set; get;}
    public Assignment[] assignments {set; get;}
    private Criteria[] criteria {set; get;}
    private Destination[] destinations {set; get;}

    //Constructor
    public Flow() {

    }
    public Flow(string name, Survey survey) {
        this.name = name;
        this.inputSurvey = survey;
    }
    public Flow initializeFlow() {
        //copy templates
            //Get from database
        //copy forms
            //get from paths
        //return new Flow
        return new Flow();      //temporary
    }

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
            if ( criterion.name == filter.name) {
                //find the answer's name that matches the filter's value and determine if it is selected
                var answer = criterion.answers.Where(a => a.value == filter.value && a.Selected).FirstOrDefault();
                
                //if answer is null, we didn't find a match, don't apply the filter
                if (answer != null)
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