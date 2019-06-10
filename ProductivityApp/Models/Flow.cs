public class Flow {
  
    //Fields:: Setters and getters
    public string name {set; get;}
    public Survey inputSurvey {set; get;}
    public Assignment[] assignments {set; get;}
    private Criteria[] criteria {set; get;}
    private Destination[] destinations {set; get;}

    public Flow()
    {

    }
    //Constructor
    public Flow(string name, Survey survey) {
        this.name = name;
        this.inputSurvey = survey;
    }
    
}