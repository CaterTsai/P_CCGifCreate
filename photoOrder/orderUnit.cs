using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P_CCGifCreate.photoOrder
{
    public class orderUnit
    {
        private bool[] _camChecker = new bool[exParameterSingleton.getInstance.cameraNum];
        private int _camCounter = 0;

        //Property
        public string orderID { get; private set; }
        public bool isOrderComplete
        {
            get
            {
                return (_camCounter == exParameterSingleton.getInstance.cameraNum);
            }
        }

        //-------------------------------------
        public orderUnit(string id)
        {
            orderID = id;
            _camCounter = 0;
            for (int i = 0; i < _camChecker.Length; i++)
            {
                _camChecker[i] = false;
            }
        }

        //-------------------------------------
        public void setOrderCam(int camID)
        {
            if(camID >= 0 && camID <= exParameterSingleton.getInstance.cameraNum && !_camChecker[camID])
            {
                _camChecker[camID] = true;
                _camCounter++;

            }
        }
    }
}
