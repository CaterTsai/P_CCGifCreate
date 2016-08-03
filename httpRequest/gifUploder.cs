using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace P_CCGifCreate.httpRequest
{
    class gifUploder
    {
        static private WebClient _gifUpload = new WebClient();

        static public bool upload(string filePath)
        {
            byte[] responseArray_ = _gifUpload.UploadFile(  exParameterSingleton.getInstance.gifUploadUrl,
                                                            "POST",
                                                            filePath);

            Console.WriteLine(System.Text.Encoding.ASCII.GetString(responseArray_));
            return true;
        }

    }
}
