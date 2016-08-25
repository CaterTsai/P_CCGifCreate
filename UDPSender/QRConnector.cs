using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace P_CCGifCreate.UDPSender
{
    public class QRConnector
    {
        private IPEndPoint _RemoteEndPoint = null;
        private Socket _QRServer = null;
        private bool _isSetup = false;

        //Const Parameter
        private static readonly string udpBeginEndTex = "\x01";
        private static readonly string udpIntervalTex = "\x02";

        //--------------------------------
        public QRConnector()
        {
            init();
        }

        //--------------------------------
        private void init()
        {
            try
            {
                _RemoteEndPoint = new IPEndPoint(
                    IPAddress.Parse(exParameterSingleton.getInstance.qrIP),
                    exParameterSingleton.getInstance.qrPort);

                _QRServer = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                
                _isSetup = true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            
        }

        //--------------------------------
        public void sendQRInfo(string url, string serialNo)
        {
            if(_isSetup)
            {
                string senderMsg_ = udpBeginEndTex + url + udpIntervalTex + "序號:"+ serialNo + udpBeginEndTex;
                byte[] sourceData_ = Encoding.UTF8.GetBytes(senderMsg_);

                _QRServer.SendTo(sourceData_, sourceData_.Length, SocketFlags.None, _RemoteEndPoint);
            }
        }
    }
}
