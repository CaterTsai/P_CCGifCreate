using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Input;

namespace P_CCGifCreate.photoElement
{
    public class photoUnit
    {
        public Image photo {get; private set;}
        public BitmapImage photoBitmap { get; private set; }
        public ComboBox photoList { get; private set; }
        public int cameraNo { get; private set; }

        #region Basic Element
        //-------------------------------------
        public photoUnit(int no, ref Image photoElement, ref ComboBox orderListElement)
        {
            cameraNo = no;
            photo = photoElement;
            photoList = orderListElement;
            photoList.SelectionChanged += photoList_SelectionChanged;
            
        }

        //-------------------------------------
        public void setPhotoDisplay(string name)
        {
            //setPhoto(getUri(name));
            setPhotoListSelect(name);
        }
        //-------------------------------------
        public void clearPhotoDisplay()
        {
            clearPhoto();
            clearPhotoList();
        }
        #endregion

        #region Photo Control


        //-------------------------------------
        private void setPhoto(string uri)
        {
            photoBitmap = new BitmapImage();
            photoBitmap.BeginInit();
            photoBitmap.UriSource = new Uri(uri);
            photoBitmap.CacheOption = BitmapCacheOption.OnLoad;
            photoBitmap.EndInit();
            photoBitmap.Freeze();
            
            photo.Source = photoBitmap;
        }

        //-------------------------------------
        private void clearPhoto()
        {
            photo.Source = null;
        }

        //-------------------------------------
        private string getUri(string photoID)
        {
            return Directory.GetCurrentDirectory() + "\\" + exParameterSingleton.getInstance.cameraFolderPath + photoID + "_" + cameraNo.ToString().PadLeft(2, '0') + ".jpg";
        }
        #endregion

        #region Photo List Control
        //-------------------------------------
        public void addPhotoList(string photoID)
        {
            if (!isPhotoIDExist(photoID))
            {
                photoList.Items.Add(photoID);
            }
        }

        //-------------------------------------
        private void setPhotoListSelect(string photoID)
        {
            if (isPhotoIDExist(photoID))
            {
                photoList.SelectedValue = photoID;
            }
        }

        //-------------------------------------
        private void clearPhotoList()
        {
            photoList.Items.Clear();
        }

        //-------------------------------------
        private bool isPhotoIDExist(string photoID)
        {
            return photoList.Items.Cast<string>().Any(item_ => item_.Equals(photoID));
        }

        //-------------------------------------
        private void photoList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {   
            if (e.AddedItems.Count > 0)
            {
                setPhoto(getUri(e.AddedItems[0].ToString()));
            }
        }

        
        #endregion
    }
}
