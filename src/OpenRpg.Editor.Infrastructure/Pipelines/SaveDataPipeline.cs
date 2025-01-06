using System.Collections.Generic;
using Persistity.Core.Serialization;
using Persistity.Endpoints;
using Persistity.Flow.Builders;
using Persistity.Flow.Pipelines;
using Persistity.Flow.Steps.Types;

namespace OpenRpg.Editor.Infrastructure.Pipelines
{
    public class SaveDataPipeline : FlowPipeline, ISaveDataPipeline
    {
        public ISerializer Serializer { get; }
        public ISendDataEndpoint Endpoint { get; }

        public SaveDataPipeline(PipelineBuilder pipelineBuilder, ISerializer serializer, ISendDataEndpoint endpoint)
        {
            Serializer = serializer;
            Endpoint = endpoint;
            Steps = BuildSteps(pipelineBuilder);
        }
        
        protected IEnumerable<IPipelineStep> BuildSteps(PipelineBuilder builder)
        {
            return builder
                .StartFromInput()
                .SerializeWith(Serializer)
                .ThenSendTo(Endpoint)
                .BuildSteps();
        }
    }
}