package pharmacy.rabbitMQ;

import com.fasterxml.jackson.databind.ObjectMapper;
import org.springframework.amqp.core.Message;
import org.springframework.amqp.core.MessageListener;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;
import org.springframework.stereotype.Service;
import pharmacy.dto.PrescriptionDto;
import pharmacy.service.PrescriptionService;

@Service
public class RabbitMQListener implements MessageListener {

    private static PrescriptionService prescriptionService;

    public static PrescriptionService getPrescriptionService() {
        return prescriptionService;
    }

    public static void setPrescriptionServiceService(PrescriptionService prescriptionService) {
        RabbitMQListener.prescriptionService = prescriptionService;
    }

    private static final ObjectMapper OBJECT_MAPPER = new ObjectMapper();

    public void onMessage(Message message) {
        System.out.println("Consuming Message - " + new String(message.getBody()));

        final String json = new String(message.getBody());
        try {
            final PrescriptionDto prescriptionDto = OBJECT_MAPPER.readValue(json, PrescriptionDto.class);
            System.out.println(prescriptionDto.Id);
            System.out.println(prescriptionDto.DoctorId);
            System.out.println(prescriptionDto.PatientId);
            System.out.println(prescriptionDto.Text);
            System.out.println(prescriptionDto.DoctorUsername);
            System.out.println(prescriptionDto.DrugNames);

            prescriptionService.prescriptionDtoToPrescription(prescriptionDto);
        }catch(final Exception e){e.printStackTrace();}
    }
}
