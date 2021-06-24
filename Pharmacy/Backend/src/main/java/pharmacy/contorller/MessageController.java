package pharmacy.contorller;

import ch.qos.logback.core.CoreConstants;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@CrossOrigin
@RestController
@RequestMapping(value = "/message")
public class MessageController {

    @GetMapping()
    public ResponseEntity<String> getHello() {
        System.out.println("Hello");
        return new ResponseEntity("Hello", HttpStatus.OK);
    }
}
