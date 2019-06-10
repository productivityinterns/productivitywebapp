class Filter{
    public String name {set; get;}
    public Object value {set; get;}

    public Filter(String name, Object value) {
        this.name = name;
        this.value = value;
    }

    public bool checkFilter(Criteria[] criteriaArray) {
        foreach (Criteria criterion in criteriaArray)
        {   
            if ( criterion.name == this.name) {
                Answer temp = criterion.answer;
                if (criterion.value = this.value) {
                    return true;
                }
            }
        }
        return false;
    }
}