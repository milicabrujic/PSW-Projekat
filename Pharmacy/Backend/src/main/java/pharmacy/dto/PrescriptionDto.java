package pharmacy.dto;

import pharmacy.model.Drug;
import pharmacy.model.Prescription;

import java.util.ArrayList;
import java.util.List;

public class PrescriptionDto {

    public int Id;
    public String Text;
    public int PatientId;
    public int DoctorId;
    public String DoctorUsername;
    public List<String> DrugNames = new ArrayList<String>();
    public Boolean isPrescribed = false;

    public PrescriptionDto() { }

    public PrescriptionDto(Prescription prescription) {
        this.Id = Integer.parseInt(prescription.getId().toString());
        this.Text = prescription.getText();
        this.DoctorUsername = prescription.getDoctorUsername();
        this.isPrescribed = prescription.getPrescribed();

        for(Drug drug : prescription.getDrugs()){
            this.DrugNames.add(drug.getName());
        }
    }

    public int getId() {
        return Id;
    }

    public void setId(int id) {
        Id = id;
    }

    public String getText() {
        return Text;
    }

    public void setText(String text) {
        Text = text;
    }

    public int getPatientId() {
        return PatientId;
    }

    public void setPatientId(int patientId) {
        PatientId = patientId;
    }

    public int getDoctorId() {
        return DoctorId;
    }

    public void setDoctorId(int doctorId) {
        DoctorId = doctorId;
    }

    public List<String> getDrugNames() {
        return DrugNames;
    }

    public void setDrugNames(List<String> drugNames) {
        DrugNames = drugNames;
    }

    public String getDoctorUsername() {
        return DoctorUsername;
    }

    public void setDoctorUsername(String doctorUsername) {
        DoctorUsername = doctorUsername;
    }

    public Boolean getPrescribed() {
        return isPrescribed;
    }

    public void setPrescribed(Boolean prescribed) {
        isPrescribed = prescribed;
    }
}
