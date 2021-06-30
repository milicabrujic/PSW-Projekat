//using Com.Example.Grpc;
using Grpc.Core;
using Rs.Ac.Uns.Ftn.Grpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSW_backend
{
    public class NetGrpcServiceImpl : NetGrpcService.NetGrpcServiceBase
    {
        public override Task<MessageResponseProto> transfer(MessageProto request, ServerCallContext context)
        {
            Console.WriteLine(request.Message + " from spring; random int: " + request.RandomInteger.ToString() + "from medication"  + request.Medication);
           // Console.WriteLine(request.Medication);
            MessageResponseProto response = new MessageResponseProto();
            response.Response = "NET GRPC RESPONSE " + Guid.NewGuid().ToString();
            response.Status = "STATUS OK";
            return Task.FromResult(response);
        }
    }
}
