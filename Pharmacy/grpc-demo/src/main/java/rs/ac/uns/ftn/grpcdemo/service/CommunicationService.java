package rs.ac.uns.ftn.grpcdemo.service;

import io.grpc.stub.StreamObserver;
import net.devh.boot.grpc.server.service.GrpcService;
import org.springframework.beans.factory.annotation.Autowired;
import rs.ac.uns.ftn.grpc.Medication;
import rs.ac.uns.ftn.grpc.MessageProto;
import rs.ac.uns.ftn.grpc.MessageResponseProto;
import rs.ac.uns.ftn.grpc.SpringGrpcServiceGrpc;
import rs.ac.uns.ftn.grpcdemo.dto.DrugDto;
import rs.ac.uns.ftn.grpcdemo.repository.DrugRepository;

import java.util.UUID;

@GrpcService
public class CommunicationService extends SpringGrpcServiceGrpc.SpringGrpcServiceImplBase {


    @Autowired
    DrugRepository drugRepository;

    @Override
    public void communicate(MessageProto request, StreamObserver<MessageResponseProto> responseObserver) {
        System.out.println("Message: " + request.getMessage() + "; randomInteger: " + request.getRandomInteger() );
        DrugDto drug = drugRepository.findByName(request.getMedication());
        System.out.println(drug.getName());
        MessageResponseProto responseMessage;
        if(drug != null) {
            Medication.Builder medicationProto = transferMedicationToMedicationProto(drug);
            Medication med = medicationProto.build();
            System.out.println(med);
            responseMessage = MessageResponseProto.newBuilder()
                    .setResponse("medication from spring")
                    .setStatus("Status 200")
                    .setMedication(med).build();

        } else {
            responseMessage = MessageResponseProto.newBuilder()
                    .setResponse("eroro")
                    .setStatus("404").build();
        }


        responseObserver.onNext(responseMessage);
        responseObserver.onCompleted();
    }
    private Medication.Builder transferMedicationToMedicationProto(DrugDto drug) {
        Medication.Builder medicationProto = Medication.newBuilder();
        medicationProto.setId(drug.getId());
        medicationProto.setName(drug.getName());
        medicationProto.setAmount(drug.getAmount());
        return medicationProto;
    }
}
