package pharmacy.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import pharmacy.model.Drug;
import pharmacy.model.Prescription;

import java.util.Optional;

public interface PrescriptionRepository extends JpaRepository<Prescription, Long> {

    Optional<Prescription> getByText(String text);
}
