using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SLOBSharp.Domain.Mapping;

namespace SLOBSharp.Client.Responses
{
    public enum SceneNodeType { Folder, Item };

    public class Crop
    {
        [JsonProperty("bottom")]
        public long Bottom { get; set; }

        [JsonProperty("left")]
        public long Left { get; set; }

        [JsonProperty("right")]
        public long Right { get; set; }

        [JsonProperty("top")]
        public long Top { get; set; }
    }

    public class Position
    {
        [JsonProperty("x")]
        public double X { get; set; }

        [JsonProperty("y")]
        public double Y { get; set; }
    }

    public class SceneNodeTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType) => objectType == typeof(SceneNodeType) || objectType == typeof(SceneNodeType?);

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            switch (serializer.Deserialize<string>(reader))
            {
                case "folder":
                    return SceneNodeType.Folder;

                case "item":
                    return SceneNodeType.Item;
            }

            throw new Exception("Cannot unmarshal type SceneNodeType");
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null) serializer.Serialize(writer, null);

            switch ((SceneNodeType)value)
            {
                case SceneNodeType.Folder:
                    serializer.Serialize(writer, "folder");
                    return;

                case SceneNodeType.Item:
                    serializer.Serialize(writer, "item");
                    return;
            }

            throw new Exception("Cannot marshal type SceneNodeType");
        }
    }

    public class SlobsError
    {
        [JsonProperty("code")]
        public long Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }

    public class SlobsNode
    {
        [JsonProperty("childrenIds")]
        public List<string> ChildrenIds { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("locked")]
        public bool? Locked { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("obsSceneItemId")]
        public long? ObsSceneItemId { get; set; }

        [JsonProperty("parentId")]
        public string ParentId { get; set; }

        [JsonProperty("resourceId")]
        public string ResourceId { get; set; }

        [JsonProperty("sceneId")]
        public string SceneId { get; set; }

        [JsonProperty("sceneItemId")]
        public string SceneItemId { get; set; }

        [JsonProperty("sceneNodeType")]
        [JsonConverter(typeof(SceneNodeTypeConverter))]
        public SceneNodeType SceneNodeType { get; set; }

        [JsonProperty("sourceId")]
        public string SourceId { get; set; }

        [JsonProperty("transform")]
        public Transform Transform { get; set; }

        [JsonProperty("visible")]
        public bool? Visible { get; set; }
    }

    public class SlobsResult
    {
        [JsonProperty("async")]
        public bool Async { get; set; }

        [JsonProperty("audio")]
        public bool Audio { get; set; }

        [JsonProperty("doNotDuplicate")]
        public bool DoNotDuplicate { get; set; }

        [JsonProperty("height")]
        public long Height { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("muted")]
        public bool Muted { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("nodes")]
        public List<SlobsNode> Nodes { get; set; }

        [JsonProperty("recordingStatus")]
        public string RecordingStatus { get; set; }

        [JsonProperty("recordingStatusTime")]
        public DateTimeOffset RecordingStatusTime { get; set; }

        [JsonProperty("resourceId")]
        public string ResourceId { get; set; }

        [JsonProperty("type")]
        public string ResultType { get; set; }

        [JsonProperty("sourceId")]
        public string SourceId { get; set; }

        [JsonProperty("streamingStatus")]
        public string StreamingStatus { get; set; }

        [JsonProperty("streamingStatusTime")]
        public DateTimeOffset StreamingStatusTime { get; set; }

        [JsonProperty("_type")]
        public string Type { get; set; }

        [JsonProperty("video")]
        public bool Video { get; set; }

        [JsonProperty("width")]
        public long Width { get; set; }
    }

    public class SlobsRpcResponse
    {
        [JsonProperty("error")]
        public SlobsError Error { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("jsonrpc")]
        public string Jsonrpc { get; set; }

        [JsonProperty("result")]
        [JsonConverter(typeof(SingleOrArrayConverter<SlobsResult>))]
        public IEnumerable<SlobsResult> Result { get; set; }
    }

    public class Transform
    {
        [JsonProperty("crop")]
        public Crop Crop { get; set; }

        [JsonProperty("position")]
        public Position Position { get; set; }

        [JsonProperty("rotation")]
        public long Rotation { get; set; }

        [JsonProperty("scale")]
        public Position Scale { get; set; }
    }
}
