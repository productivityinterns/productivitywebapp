class Flow {
  
    //Fields:: Setters and getters
    public String name {set; get;}
    public Survey inputSurvey {set; get;}
    public Assignment[] assignments {set; get;}
    private Criteria[] criteria {set; get;}
    private Destination[] destinations {set; get;}

    //Constructor
    public Flow(String name, Survey survey) {
        this.name = name;
        this.inputSurvey = survey;
    }
    
}