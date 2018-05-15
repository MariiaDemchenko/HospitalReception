using System;
using System.Runtime.Serialization;

namespace Microsoft.ServiceModel.ImageViewer.Contracts.DataContracts
{
    [DataContract]
    public class ImageInfo
    {
        [DataMember] public string Name { get; set; }
        [DataMember] public DateTime Date { get; set; }
        [DataMember] public Constants.ImageSize Size { get; set; }
        [DataMember] public long ImageSizeBytes { get; set; }
    }
}