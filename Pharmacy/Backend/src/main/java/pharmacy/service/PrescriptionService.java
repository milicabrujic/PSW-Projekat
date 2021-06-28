package pharmacy.service;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import pharmacy.dto.PrescriptionDto;
import pharmacy.model.Drug;
import pharmacy.model.Prescription;
import pharmacy.repository.DrugRepository;
import pharmacy.repository.PrescriptionRepository;

import java.util.*;

@Service
public class PrescriptionService {

    @Autowired
    DrugRepository drugRepository;

    @Autowired
    PrescriptionRepository prescriptionRepository;

    public List<PrescriptionDto> getAllPrescriptions() {

        List<PrescriptionDto> prescriptionsDTOlist = new ArrayList<>();
        List<Prescription> prescriptions = prescriptionRepository.findAll();

        for (Prescription  prescription : prescriptions) {
            if(!prescription.getPrescribed())
                prescriptionsDTOlist.add(new PrescriptionDto(prescription));
        }

        return prescriptionsDTOlist;
    }

    public void prescriptionDtoToPrescription(PrescriptionDto prescriptionDto){

        Prescription prescription = new Prescription();

        setFields(prescription, prescriptionDto);

        setDrugNames(prescription, prescriptionDto);

        prescriptionRepository.save(prescription);
    }

    public void setDrugNames(Prescription prescription, PrescriptionDto prescriptionDto){

        for(String drugName : prescriptionDto.DrugNames){
            if(prescription.getDrugs() == null)
                prescription.setDrugs(new HashSet<Drug>());

            prescription.getDrugs().add(drugRepository.getByName(drugName).orElse(null));
        }
    }

    public void setFields(Prescription prescription, PrescriptionDto prescriptionDto){

        prescription.setText(prescriptionDto.Text);
        prescription.setDoctorUsername(prescriptionDto.DoctorUsername);
        prescription.setPrescribed(prescriptionDto.getPrescribed());
    }
}