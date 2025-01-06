using Persistity.Core.Serialization;
using Persistity.Endpoints;
using Persistity.Flow.Builders;

namespace OpenRpg.Editor.Infrastructure.Pipelines.Typed
{
    public class SaveDataPipeline<T> : SaveDataPipeline, ISaveDataPipeline<T>
    {
        public SaveDataPipeline(PipelineBuilder pipelineBuilder, ISerializer serializer, ISendDataEndpoint endpoint) : base(pipelineBuilder, serializer, endpoint)
        {
        }
    }
}