using Microsoft.ServiceModel.ImageViewer.Contracts.DataContracts;
using Microsoft.ServiceModel.ImageViewer.Contracts.MessageContracts;
using System.IO;
using System.ServiceModel;

namespace Microsoft.ServiceModel.ImageViewer.Contracts.ServiceContracts
{
    [ServiceContract]
    [ServiceKnownType(typeof(ImageInfo))]
    public interface IImageViewer
    {
        [OperationContract]
        FilesData GetAllImages();
        [FaultContract(typeof(ImageProcessingFault))]
        [OperationContract]
        Stream DownloadImage(ImageInfo data);
        [OperationContract]
        [FaultContract(typeof(ImageProcessingFault))]
        void UploadImage(ImageData data);
    }
}