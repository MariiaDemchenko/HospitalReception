using Microsoft.ServiceModel.ImageViewer.Contracts.DataContracts;
using System.IO;
using System.ServiceModel;

namespace Microsoft.ServiceModel.ImageViewer.Contracts.MessageContracts
{
    [MessageContract]
    public class ImageData
    {
        [MessageHeader]
        public ImageInfo ImageInfo;

        [MessageBodyMember]
        public Stream ImageStream;

        public void Dispose()
        {
            if (ImageStream != null)
            {
                ImageStream.Close();
                ImageStream = null;
            }
        }
    }
}