using System;
using System.Runtime.Serialization;

namespace Microsoft.ServiceModel.ImageViewer.Contracts.DataContracts
{
    [DataContract]
    public class ImageInfo
    {
        [DataMember] public string Name;
        [DataMember] public DateTime Date;
        [DataMember] public Constants.ImageSize Size;
        [DataMember] public long ImageSizeBytes;
    }
}