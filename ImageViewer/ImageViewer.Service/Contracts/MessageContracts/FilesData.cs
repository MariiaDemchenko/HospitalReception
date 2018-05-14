using Microsoft.ServiceModel.ImageViewer.Contracts.DataContracts;
using System.ServiceModel;

namespace Microsoft.ServiceModel.ImageViewer.Contracts.MessageContracts
{
    [MessageContract]
    public class FilesData
    {
        [MessageBodyMember]
        public ImageInfo[] ImageFiles { get; set; }
    }
}