package rs.ac.uns.ftn.grpcdemo.dto;

public class DrugDto {
    public int Id;
    public String Name;
    public int amount;

    public DrugDto() {

    }
    public DrugDto(int id, String name, int amount ) {
        this.Id = id;
        this.Name = name;
        this.amount = amount;
    }
    public int getId() {
        return Id;
    }

    public void setId(int id) {
        this.Id = id;
    }

    public String getName() {
        return Name;
    }

    public void setName(String name) {
        this.Name = name;
    }

    public int getAmount() {
        return amount;
    }

    public void setAmount(int amount) {
        this.amount = amount;
    }
}
