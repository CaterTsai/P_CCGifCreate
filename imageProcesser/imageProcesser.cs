using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace P_CCGifCreate.imageProcesser
{
    class photoFrameMixer
    {
        private Bitmap _photoFrame;

        //-------------------------------------
        public photoFrameMixer()
        {
            var photoFrame_ = Image.FromFile(exParameterSingleton.getInstance.photoframePath);
            _photoFrame = new Bitmap(photoFrame_);
            photoFrame_.Dispose();
        }

        //-------------------------------------
        public void addPhotoframeAndSave(BitmapImage input, string fullpath)
        {
            Bitmap source_ = BitmapImage2Bitmap(ref input);

            var target_ = new Bitmap(   exParameterSingleton.getInstance.gifWidth,
                                        exParameterSingleton.getInstance.gifHeight,
                                        PixelFormat.Format32bppArgb);

            using (var canvas_ = Graphics.FromImage(target_))
            {
                canvas_.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;

                Rectangle drawRect_ = new Rectangle(0, 0, target_.Width, target_.Height);
                canvas_.DrawImage(source_, drawRect_);
                canvas_.DrawImage(_photoFrame, drawRect_);

                target_.Save(fullpath, ImageFormat.Jpeg);
            }   
        }

        //-------------------------------------
        private Bitmap BitmapImage2Bitmap(ref BitmapImage bitmapImage)
        {
            using (MemoryStream outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapImage));
                enc.Save(outStream);
                return new Bitmap(outStream);
            }
        }
        
    }
}
