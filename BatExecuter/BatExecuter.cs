using System;
using System.Diagnostics;
using System.IO;

namespace P_CCGifCreate.BatExecuter
{
    class gifCreater
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
    }
}
