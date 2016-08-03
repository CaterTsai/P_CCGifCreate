using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Text;

namespace P_CCGifCreate
{
    public class exParameterSingleton
    {
        public string cameraFolderPath { get; private set; }
        public string gifTempFolder { get; private set; }
        public string photoframePath { get; private set; }

        public string gifResultFolder { get; private set; }
        public string gifBat { get; private set; }
        public string gifUploadUrl { get; private set; }
        public int gifWidth { get; private set;}
        public int gifHeight { get; private set; }

        public string qrIP { get; private set; }
        public int qrPort { get; private set; }
        public string qrShareUrl { get; private set; }

        public int cameraNum { get; private set; }

        //-------------------------------
        private void loadConfig()
        {
            using (var streamReader_ = new StreamReader(@"data/_config.json", Encoding.UTF8))
            {
                var source_ = streamReader_.ReadToEnd();
                JObject config_ = JObject.Parse(source_);

                cameraFolderPath = (string)config_["cameraFolderPath"];
                gifTempFolder = (string)config_["gifTempFolder"];
                photoframePath = (string)config_["photoframePath"];

                gifResultFolder = (string)config_["gifResultFolder"];
                gifBat = (string)config_["gifBat"];
                gifWidth = (int)config_["gifWidth"];
                gifHeight = (int)config_["gifHeight"];

                gifUploadUrl = (string)config_["gifUploadUrl"];
                qrShareUrl = (string)config_["qrShareUrl"];

                qrIP = (string)config_["qrIP"];
                qrPort = (int)config_["qrPort"];

                cameraNum = (int)config_["cameraNum"];
            }
            
        }

        //-------------------------------
        private void setDefault()
        {
            //Default value
            cameraFolderPath = "data/camPhotos/";
            gifTempFolder = "data/gifTemp/";
            photoframePath = "data/images/photoFrame.png";

            gifResultFolder = "data/gifResult/";
            gifBat = "data/createGif.bat";
            gifWidth = 720;
            gifHeight = 480;

            gifUploadUrl = @"http://events.artgital.com/cc/cc.php";
            qrShareUrl = @"http://events.artgital.com/cc/share.php?id=";

            qrIP = "127.0.0.1";
            qrPort = 11999;

            cameraNum = 7;
        }

        #region Singleton
        private static exParameterSingleton _instance = null;
        //------------------------------------------
        private exParameterSingleton()
        {
            setDefault();
            //loadConfig();
        }

        //------------------------------------------
        public static exParameterSingleton getInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new exParameterSingleton();
                }
                return _instance;
            }
        }
        #endregion
    }
}
