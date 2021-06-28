package pharmacy.contorller;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;
import pharmacy.dto.PrescriptionDto;
import pharmacy.model.Prescription;
import pharmacy.service.PrescriptionService;

import java.util.List;

@CrossOrigin
@RestController
@RequestMapping(value = "/prescriptions")
public class PrescriptionController {

    @Autowired
    private PrescriptionService prescriptionService;

    @GetMapping()
    public ResponseEntity<List<PrescriptionDto>> getAllPrescriptions() {
        return new ResponseEntity<List<PrescriptionDto>>(prescriptionService.getAllPrescriptions(), HttpStatus.OK);
    }
}
