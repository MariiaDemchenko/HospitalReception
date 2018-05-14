using System.Runtime.Serialization;

namespace Microsoft.ServiceModel.ImageViewer.Contracts.DataContracts
{
    [DataContract]
    public class ImageProcessingFault
    {
        [DataMember]
        public string Action { get; set; }

        [DataMember]
        public string Message { get; set; }
    }
}