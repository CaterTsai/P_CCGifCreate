using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace P_CCGifCreate.photoOrder
{
    public class photoOrderEventArgs : EventArgs
    {
        public photoOrderEventArgs(string photoID)
        {
            photoOrderID = photoID;
        }
        public string photoOrderID { get; private set; }
    }

    public class photoWatcher
    {
        //-------------------------------------
        //element
        private Dictionary<string, orderUnit> _orderMap = new Dictionary<string, orderUnit>();

        private FileSystemWatcher _watcher = null;
        private bool _isStart = false;

        //Event
        public delegate void PhotoOrderEventHandler(object sender, photoOrderEventArgs e);
        public event PhotoOrderEventHandler photoOrderEvent;
        

        #region Basic Method
        //-------------------------------------
        public photoWatcher()
        {}

        //-------------------------------------
        ~photoWatcher()
        {
            if(_isStart)
            {
                stop();
            }
        }
        #endregion

        #region file watcher
        //-------------------------------------
        public bool start()
        {
            if ( _isStart)
            {
                return false;
            }

            _watcher = new FileSystemWatcher();
            _watcher.Path = exParameterSingleton.getInstance.cameraFolderPath;
            _watcher.Filter = "*.jpg";
            
            _watcher.Created += onPhoto;

            _watcher.EnableRaisingEvents = true;
            _watcher.IncludeSubdirectories = true;

            _isStart = true;

            //Console.WriteLine("[photoWatcher] Start Success.");
            return true;
        }

        //-------------------------------------
        public void stop()
        {
            if (_isStart)
            {
                _watcher.Created -= onPhoto;
                _watcher = null;
            }
        }

        //-------------------------------------
        private void onPhoto(object source, FileSystemEventArgs e)
        {   
            //photo format "{photo id}_{camera ID}.jpg" : "0001_01.jpg"
            int photoStart_ = e.FullPath.LastIndexOf("\\") + 1;
            int camStart_ = photoStart_ + 5;

            int camID_ = int.Parse(e.FullPath.Substring(camStart_, 2));
            string photoID_ = e.FullPath.Substring(photoStart_, 4);

            Console.WriteLine("Get photo : " + photoID_ + " from camera : " + camID_.ToString());
            addToOrderMap(camID_, photoID_);

            checkOrderComplete(photoID_);
        }
        
        //-------------------------------------
        private void addToOrderMap(int camID, string photoID)
        {
            orderUnit orderUnit_;
            if (!_orderMap.TryGetValue(photoID, out orderUnit_))
            {
                _orderMap.Add(photoID, new orderUnit(photoID));
                orderUnit_ = _orderMap[photoID];
            }
            orderUnit_.setOrderCam(camID);
        }
        
        //-------------------------------------
        private void checkOrderComplete(string photoID)
        {
            orderUnit orderUnit_;
            if (!_orderMap.TryGetValue(photoID, out orderUnit_))
            {
                return;
            }

            if(orderUnit_.isOrderComplete)
            {
                //Trigger complete event
                photoOrderEvent(this, new photoOrderEventArgs(photoID));
                _orderMap.Remove(photoID);
            }
        }
        #endregion
        
    }
}
