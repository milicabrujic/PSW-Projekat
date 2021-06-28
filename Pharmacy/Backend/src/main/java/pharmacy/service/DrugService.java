package pharmacy.service;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.stereotype.Service;
import pharmacy.dto.PrescriptionDto;
import pharmacy.model.Drug;
import pharmacy.model.Prescription;
import pharmacy.repository.DrugRepository;
import pharmacy.repository.PrescriptionRepository;

import java.util.Optional;

@Service
public class DrugService {

    @Autowired
    DrugRepository drugRepository;

    @Autowired
    PrescriptionRepository prescriptionRepository;

    public PrescriptionDto updateDrugs(PrescriptionDto prescriptionDto){

        for(String drugName : prescriptionDto.DrugNames){
            Drug drug = drugRepository.getByName(drugName).orElse(null);

            if(drug.getAmount() == 0)
                return null;

            drug.setAmount(drug.getAmount() - 1);
            drugRepository.save(drug);
        }

        changePrescriptionStatus(prescriptionDto);

        return prescriptionDto;
    }

    public void changePrescriptionStatus(PrescriptionDto prescriptionDto){

        Prescription prescription = prescriptionRepository.getByText(prescriptionDto.getText()).orElse(null);

        prescription.setPrescribed(true);
        prescriptionRepository.save(prescription);
    }
}
