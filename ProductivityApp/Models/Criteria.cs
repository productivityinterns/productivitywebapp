public class Criteria {
    public String name {set; get;}
    public String prompt {set; get;}
    private Answer answer;

    public Answer getAnswer() {
        return this.answer;
    }
    public void setAnswer(String key, String value) {
        this.answer = new Answer(key,value);
    }

    public Criteria(String name) {
        this.name =  name;
        this.prompt = "Default criteria prompt.";
    } 

}