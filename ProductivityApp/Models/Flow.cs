using ProductivityApp.Models;
using ProductivityApp.Models.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

[Serializable]
[Table("Flows")]
public class Flow {
  
        //TODO: We need to add IList<Form> to this class and the database references
        //RIght now theres nowhere for forms to connect 

    //Fields:: Setters and getters
    //unique id for filled flows
    public Guid Id {get;set;}
    public string name {set; get;}
    public string Description {get;set;}
    public Survey inputSurvey {set; get;}
    public IList<Criteria> criteria {set; get;}
    public Destination destination {set; get;}

    /// <summary>
    /// The filename of the image for the flow button
    /// </summary>
    [NotMapped]
    public string ThumbnailImage { get; set; }
    public IList<Form> forms {set; get;}
    /// <summary>
    /// Ugly hack. If true, we get into templates.
    /// 
    /// If false, its a copied flow.
    /// 
    /// We really should make separate classes with separate models, lesson learned.
  
    /// </summary>
    public bool IsATemplate { get; set; }

    //Constructor
    public Flow() {

    }
    public Flow(string name, Survey survey) {
        this.name = name;
        this.inputSurvey = survey;
    }
    ///<summary>
    /// This method creates a new flow based on a tamplate flow
    /// <returns>copy of the template flow</returns>
    ///</summary>
    public Flow initializeFlow() {
        //copy templates
        Flow newFlow = CloneTemplate();
        
        newFlow.AssignNewGuidIds();
        //we created the flow right now...
        newFlow.inputSurvey.timeCreated = DateTime.Now;
        //we'll figure out how to add the user later..
        newFlow.inputSurvey.user = "we don't get the userid right now";
        //Get from database -- we already have the template from db, so this step probably doesn't need to do anything -MG
        newFlow.IsATemplate = false;
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
        return clonedFlow;
    }
    //Since this is an instance method, you already have the template (this object) and don't need a parameter! -MG

    ///<summary>
    /// This method determines if a given filter exists
    /// <param name="filter">The filter that is being checked</param>
    /// <returns>true if filter exists</returns>
    ///</summary>
    public bool checkFilter(Filter filter) {
        if (filter == null ) {
            return true;
        }

        foreach (Criteria criterion in this.criteria)
        {   
            if ( criterion.Category == filter.name) {
                //find the answer's name that matches the filter's value and determine if it is selected
                var answer = criterion.SelectedValue;
                
                //if answer is null, we didn't find a match, don't apply the filter
                if (!String.IsNullOrEmpty(answer) && answer == filter.value)
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

    /// <summary>
    /// Read the template in the assignment inputfield, and render the results.
    /// </summary>
    /// <param name="assignment"></param>
    /// <returns></returns>
    public string GetAssignmentText(Assignment assignment){
        string theText = "";
        var matchingField = this.inputSurvey.fields.Where(f => f.tag != null &&  f.tag.ToLower() == assignment.inputField?.ToLower()).FirstOrDefault();
       var matchingCriteria = this.criteria.Where(c => c.Category != null && c.Category.ToLower() == assignment.inputField?.ToLower()).FirstOrDefault();
        if(matchingField != null)
        {
            theText = matchingField.answer;
        }else if (matchingCriteria != null){
            theText = matchingCriteria.SelectedValue;
        }
        return theText ?? "";
    }
}