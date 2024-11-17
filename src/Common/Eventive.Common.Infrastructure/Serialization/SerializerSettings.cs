using Newtonsoft.Json;

namespace Eventive.Common.Infrastructure.Serialization;

//TypeNameHandling: Enables including type information in the JSON,
//allowing for the proper deserialization of derived types.

//MetadataPropertyHandling: Specifies how metadata properties
//($type, $id, etc.) are read and used during deserialization.

public static class SerializerSettings
{
    public static readonly JsonSerializerSettings Instance = new()
    {
        TypeNameHandling = TypeNameHandling.All,
        MetadataPropertyHandling = MetadataPropertyHandling.ReadAhead
    };
}
