package pharmacy.model;

import javax.persistence.*;
import java.util.List;
import java.util.Set;

@Entity
public class Prescription {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Column(name = "text", nullable = false)
    private String text;

    @Column(name = "doctorUsername", nullable = false)
    private String doctorUsername;

    @Column(name = "isPrescribed", nullable = false)
    private Boolean isPrescribed;

    @ManyToMany(fetch = FetchType.EAGER)
    private Set<Drug> drugs;

    public Prescription() { }

    public Prescription(Long id, String text, String doctorUsername, Set<Drug> drugs) {
        this.id = id;
        this.text = text;
        this.doctorUsername = doctorUsername;
        this.drugs = drugs;
    }

    public Long getId() {
        return id;
    }

    public void setId(Long id) {
        this.id = id;
    }

    public String getText() {
        return text;
    }

    public void setText(String text) {
        this.text = text;
    }

    public Set<Drug> getDrugs() {
        return drugs;
    }

    public void setDrugs(Set<Drug> drugs) {
        this.drugs = drugs;
    }

    public String getDoctorUsername() {
        return doctorUsername;
    }

    public void setDoctorUsername(String doctorUsername) {
        this.doctorUsername = doctorUsername;
    }

    public Boolean getPrescribed() {
        return isPrescribed;
    }

    public void setPrescribed(Boolean prescribed) {
        isPrescribed = prescribed;
    }
}
