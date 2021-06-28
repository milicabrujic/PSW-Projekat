package pharmacy.contorller;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import pharmacy.dto.PrescriptionDto;
import pharmacy.model.Drug;
import pharmacy.model.Prescription;
import pharmacy.service.DrugService;
import pharmacy.service.PrescriptionService;

import javax.xml.bind.ValidationException;

@CrossOrigin
@RestController
@RequestMapping(value = "/drugs")
public class DrugController {

    @Autowired
    DrugService drugService;

    @PutMapping(consumes = "application/json", produces = "application/json")
    public ResponseEntity updateDrugs(@RequestBody PrescriptionDto prescriptionDto) {

        if(prescriptionDto == null)
            return new ResponseEntity<>("Invalid input data", HttpStatus.UNPROCESSABLE_ENTITY);

        if(drugService.updateDrugs(prescriptionDto) == null)
            return new ResponseEntity<>(HttpStatus.BAD_REQUEST);

        return new ResponseEntity<PrescriptionDto>(drugService.updateDrugs(prescriptionDto), HttpStatus.OK);
    }
}
