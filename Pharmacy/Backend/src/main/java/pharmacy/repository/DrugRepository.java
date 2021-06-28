package pharmacy.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import pharmacy.model.Drug;

import java.util.Optional;

public interface DrugRepository extends JpaRepository<Drug, Long> {

    Optional<Drug> getByName(String name);
}
