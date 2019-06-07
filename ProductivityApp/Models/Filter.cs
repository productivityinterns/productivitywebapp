public class Filter{
    public String name {set; get;}
    public Object value {set; get;}

    public Filter(String name, Object value) {
        this.name = name;
        this.value = value;
    }

}