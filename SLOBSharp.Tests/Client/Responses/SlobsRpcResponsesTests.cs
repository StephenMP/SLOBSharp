using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SLOBSharp.Client.Responses;
using Xunit;

namespace SLOBSharp.Tests.Client.Responses
{
    public class SlobsRpcResponsesTests
    {
        private static Random random = new Random();
        public static string String => Guid.ToString("N");
        public static Guid Guid => Guid.NewGuid();
        public static long Long => DateTime.Now.ToFileTime();
        public static int Int => random.Next();
        public static bool Bool => Int % 2 == 0;

        [Fact]
        public void CanSerializeAndDeserializeSlobsRpcResponse()
        {
            var slobsRpcResponse = new SlobsRpcResponse
            {
                Error = new SlobsError
                {
                    Code = Long,
                    Message = String
                },
                Id = String,
                Jsonrpc = "2.0",
                Result = new[]
                {
                    new SlobsResult
                    {
                        Async = Bool,
                        Audio = Bool,
                        DoNotDuplicate = Bool,
                        Height = Long,
                        Id = String,
                        Muted = Bool,
                        Name = String,
                        Nodes = new List<SlobsNode>
                        {
                            new SlobsNode
                            {
                                ChildrenIds = new List<string>
                                {
                                    String
                                },
                                Id = String,
                                Locked = Bool,
                                Name = String,
                                ObsSceneItemId = Long,
                                ParentId = String,
                                ResourceId = String,
                                SceneId = String,
                                SceneItemId = String,
                                SceneNodeType = SceneNodeType.Folder,
                                SourceId = String,
                                Transform = new Transform
                                {
                                    Crop = new Crop
                                    {
                                        Bottom = Long,
                                        Left = Long,
                                        Right = Long,
                                        Top = Long
                                    },
                                    Position = new Position
                                    {
                                        X = Long,
                                        Y = Long
                                    },
                                    Rotation = Long,
                                    Scale = new Position
                                    {
                                        X = Long,
                                        Y = Long
                                    }
                                },
                                Visible = Bool
                            },
                            new SlobsNode
                            {
                                ChildrenIds = new List<string>
                                {
                                    String
                                },
                                Id = String,
                                Locked = Bool,
                                Name = String,
                                ObsSceneItemId = Long,
                                ParentId = String,
                                ResourceId = String,
                                SceneId = String,
                                SceneItemId = String,
                                SceneNodeType = SceneNodeType.Item,
                                SourceId = String,
                                Transform = new Transform
                                {
                                    Crop = new Crop
                                    {
                                        Bottom = Long,
                                        Left = Long,
                                        Right = Long,
                                        Top = Long
                                    },
                                    Position = new Position
                                    {
                                        X = Long,
                                        Y = Long
                                    },
                                    Rotation = Long,
                                    Scale = new Position
                                    {
                                        X = Long,
                                        Y = Long
                                    }
                                },
                                Visible = Bool
                            }
                        },
                        RecordingStatus = String,
                        RecordingStatusTime = DateTime.Now,
                        ResourceId = String,
                        ResultType = String,
                        SourceId = String,
                        StreamingStatus = String,
                        StreamingStatusTime = DateTime.Now,
                        Type = String,
                        Video = Bool,
                        Width = Long
                    }
                }
            };

            var slobsRpcResponseJson = JsonConvert.SerializeObject(slobsRpcResponse);

            Assert.NotNull(slobsRpcResponseJson);
            Assert.NotEmpty(slobsRpcResponseJson);

            var deserializedSlobsRpcResponse = JsonConvert.DeserializeObject<SlobsRpcResponse>(slobsRpcResponseJson);

            Assert.NotNull(deserializedSlobsRpcResponse);
        }
    }
}
