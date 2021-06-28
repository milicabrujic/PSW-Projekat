package pharmacy.rabbitMQ;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.CommandLineRunner;
import org.springframework.core.annotation.Order;
import org.springframework.stereotype.Component;
import pharmacy.service.PrescriptionService;

@Component
@Order(value = 1)
public class RabbitInitRun implements CommandLineRunner {

    @Autowired
    PrescriptionService prescriptionService;

    @Override
    public void run(String... args) throws Exception {
        RabbitMQListener.setPrescriptionServiceService(prescriptionService);
    }
}
