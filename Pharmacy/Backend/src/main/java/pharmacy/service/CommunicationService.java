//package pharmacy.service;
//
//import io.grpc.stub.StreamObserver;
//import net.devh.boot.grpc.server.service.GrpcService;
//
//import java.util.UUID;
//
//@GrpcService
//public class CommunicationService extends SpringGrpcServiceGrpc.SpringGrpcServiceImplBase {
//
//    @Override
//    public void communicate(MessageProto request, StreamObserver<MessageResponseProto> responseObserver) {
//        System.out.println("Message: " + request.getMessage() + "; randomInteger: " + request.getRandomInteger());
//
//        MessageResponseProto responseMessage = MessageResponseProto.newBuilder()
//                .setResponse("Spring Boot: " + UUID.randomUUID().toString()).setStatus("Status 200").build();
//
//        responseObserver.onNext(responseMessage);
//        responseObserver.onCompleted();
//    }
//}
