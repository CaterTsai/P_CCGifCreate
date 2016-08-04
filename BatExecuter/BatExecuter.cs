using System;
using System.Diagnostics;
using System.IO;

namespace P_CCGifCreate.BatExecuter
{
    class BatExe
    {
        static public void createGif(string serialNo)
        {
            Process gifBat_ = new Process();

            try
            {
                gifBat_.StartInfo.FileName = "createGif.bat";
                gifBat_.StartInfo.WorkingDirectory = Directory.GetCurrentDirectory() + "\\" + "data\\";
                gifBat_.StartInfo.CreateNoWindow = false;
                gifBat_.StartInfo.Arguments = serialNo;
                gifBat_.Start();
                gifBat_.WaitForExit();

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.StackTrace.ToString());
            }

        }

        static public void clearPhoto()
        {
            Process clearBat_ = new Process();

            try
            {
                clearBat_.StartInfo.FileName = "clearPhotos.bat";
                clearBat_.StartInfo.WorkingDirectory = Directory.GetCurrentDirectory() + "\\" + "data\\";
                clearBat_.StartInfo.CreateNoWindow = false;
                clearBat_.Start();
                clearBat_.WaitForExit();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace.ToString());
            }
        }
    }
}
