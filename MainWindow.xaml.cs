using System;
using System.Collections.Generic;
using System.Windows;

using P_CCGifCreate.photoElement;
using P_CCGifCreate.photoOrder;
using P_CCGifCreate.imageProcesser;
using P_CCGifCreate.BatExecuter;
using P_CCGifCreate.httpRequest;
using P_CCGifCreate.UDPSender;
using System.IO;

namespace P_CCGifCreate
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        private photoWatcher _photoWatcher = new photoWatcher();
        private Dictionary<int, photoUnit> _camMgr = new Dictionary<int, photoUnit>();
        private photoFrameMixer _photoFrameMixer = new photoFrameMixer();
        private QRConnector _qrConnector = new QRConnector();

        //-------------------------------------
        public MainWindow()
        {
            InitializeComponent();

            setupPhotoOrderWatcher();
            setupPhotoCtrl();
        }

        #region Photo Order Watcher
        //-------------------------------------
        private void setupPhotoOrderWatcher()
        {
            _photoWatcher.photoOrderEvent += onPhotoOrderEvent;
            _photoWatcher.start();
        }

        //-------------------------------------
        private void onPhotoOrderEvent(object sender, photoOrderEventArgs e)
        {
            Dispatcher.Invoke(
                (Action)delegate ()
                {
                    setInfo("new photo set :" + e.photoOrderID);
                    addPhotoOrder(e.photoOrderID);
                    addPhotoOrderEachUnit(e.photoOrderID);
                });
        }
        
        #endregion

        #region Photo control
        //-------------------------------------
        public void setupPhotoCtrl()
        {
            _camMgr.Add(0, new photoUnit(0, ref imgCam1, ref cbCam1));
            _camMgr.Add(1, new photoUnit(1, ref imgCam2, ref cbCam2));
            _camMgr.Add(2, new photoUnit(2, ref imgCam3, ref cbCam3));
            _camMgr.Add(3, new photoUnit(3, ref imgCam4, ref cbCam4));
            _camMgr.Add(4, new photoUnit(4, ref imgCam5, ref cbCam5));
            _camMgr.Add(5, new photoUnit(5, ref imgCam6, ref cbCam6));
            _camMgr.Add(6, new photoUnit(6, ref imgCam7, ref cbCam7));
        }

        //-------------------------------------
        public void setPhotoEachUnit(string photoID)
        {
            foreach(var photoUnit in _camMgr)
            {
                photoUnit.Value.setPhotoDisplay(photoID);
            }
        }

        //-------------------------------------
        public void addPhotoOrderEachUnit(string photoID)
        {
            foreach (var photoUnit in _camMgr)
            {
                photoUnit.Value.addPhotoList(photoID);
            }
        }

        //-------------------------------------
        public void clearPhotoOrderEachUnit()
        {
            foreach (var photoUnit in _camMgr)
            {
                photoUnit.Value.clearPhotoDisplay();
            }
        }

        //-------------------------------------
        public void savePhotoOrderEachUnit(string path)
        {
            foreach (var photoUnit in _camMgr)
            {
                string fullPath_ = path + photoUnit.Key.ToString() + ".jpg";
                _photoFrameMixer.Save(photoUnit.Value.photoBitmap, fullPath_);
            }
        }

        //-------------------------------------
        public void savePhotoOrderEachUnitWithPhotoFrame(string path)
        {
            foreach (var photoUnit in _camMgr)
            {
                string fullPath_ = path + photoUnit.Key.ToString() + ".jpg";
                _photoFrameMixer.addPhotoframeAndSave(photoUnit.Value.photoBitmap, fullPath_);
            }
        }
        #endregion

        #region Gif uploader
        //-------------------------------------
        private void uploadGif(string serialNo)
        {
            gifUploder.upload(exParameterSingleton.getInstance.gifResultFolder + serialNo + ".gif");
        }
        #endregion

        #region UI Event
        //-------------------------------------
        private void btnDisplay_Click(object sender, RoutedEventArgs e)
        {   
            if(!string.IsNullOrWhiteSpace(cbPhotoOrder.Text))
            {
                setInfo("set photos: " + cbPhotoOrder.Text);
                setPhotoEachUnit(cbPhotoOrder.Text);
            }
        }

        //-------------------------------------
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            clearAllPhoto();
        }

        //-------------------------------------
        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tbSerialNo.Text))
            {

                setInfo("Photo Processing...");
                savePhotoOrderEachUnit(exParameterSingleton.getInstance.gifTempFolder);
                //savePhotoOrderEachUnitWithPhotoFrame(exParameterSingleton.getInstance.gifTempFrameFolder);

                setInfo("Create gif...");
                BatExe.createGif(tbSerialNo.Text);
                //BatExe.createGifwithFrame(tbSerialNo.Text);

                setInfo("Upload to server...");
                uploadGif(tbSerialNo.Text);

                _qrConnector.sendQRInfo(exParameterSingleton.getInstance.qrShareUrl + tbSerialNo.Text, tbSerialNo.Text);

                clearAllPhoto();
                setInfo("Complete");
                
            }
            else
            {
                MessageBox.Show("請先輸入序號");
            }

            
        }
        #endregion

        #region UI
        //-------------------------------------
        private void addPhotoOrder(string photoOrderID)
        {
            cbPhotoOrder.Items.Add(photoOrderID);
            cbPhotoOrder.SelectedItem = photoOrderID;
        }

        //-------------------------------------
        private void clearAllPhoto()
        {
            clearPhotoOrder();
            clearPhotoOrderEachUnit();
            GC.Collect();

            BatExe.clearPhoto();
        }

        //-------------------------------------
        private void clearPhotoOrder()
        {
            cbPhotoOrder.Items.Clear();
        }
        #endregion

        #region Info Display
        //-------------------------------------
        public void setInfo(string msg)
        {
            infoDispaly.Content = msg;
        }
        #endregion
    }
}
