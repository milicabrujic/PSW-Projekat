package rs.ac.uns.ftn.grpcdemo.repository;

import org.springframework.stereotype.Service;
import rs.ac.uns.ftn.grpcdemo.dto.DrugDto;

import java.util.ArrayList;
import java.util.List;

@Service
public class DrugRepository {

  public List<DrugDto> createDrugs() {
         List<DrugDto> drugs = new ArrayList<DrugDto>();
         drugs.add(new DrugDto(200,"brufen",5));
         drugs.add(new DrugDto(201,"kafetin",10));
         return drugs;
    }

   public DrugDto findByName(String name) {
       List<DrugDto> drugs = createDrugs();
       for (DrugDto drug: drugs
            ) {
           if(drug.getName().equals(name)) {
               return  drug;
           }
       }
       return null;
    }

}
