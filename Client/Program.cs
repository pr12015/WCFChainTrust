using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using CertificateManager;
using System.Security.Cryptography.X509Certificates;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            const string srvCertCN = "wcfservice";

            var binding = new NetTcpBinding();
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;

            var srvCert = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, srvCertCN);
            var address = new EndpointAddress(new Uri("net.tcp://localhost:9999/Receiver"), new X509CertificateEndpointIdentity(srvCert));

            using (WCFClient proxy = new WCFClient(binding, address))
            {
                proxy.TestCommunication();
                Console.ReadKey(false);
            }
        }
    }
}
