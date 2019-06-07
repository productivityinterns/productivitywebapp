public class Flow {
  
    //Fields:: Setters and getters
    public String name {set; get;}
    public Survey inputSurvey {set; get;}
    public Assignment[] assignments {set; get;}
    private Criteria[] criteria {set; get;}
    private Destination[] destinations {set; get;}

    //Constructor
    public Flow() {

    }
    public Flow(String name, Survey survey) {
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
}