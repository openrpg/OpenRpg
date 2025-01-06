using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Persistity.Core.Serialization;
using Persistity.Endpoints;
using Persistity.Flow.Builders;
using Persistity.Flow.Pipelines;
using Persistity.Flow.Steps.Types;

namespace OpenRpg.Editor.Infrastructure.Pipelines
{
    public class LoadDataPipeline<T> : FlowPipeline, ILoadDataPipeline<T>
    {
        public IDeserializer Deserializer { get; }
        public IReceiveDataEndpoint Endpoint { get; }
        public Func<object, Task<object>> OptionalProcessing { get; }

        public LoadDataPipeline(PipelineBuilder pipelineBuilder, IDeserializer deserializer, IReceiveDataEndpoint endpoint, Func<object, Task<object>> optionalProcessing = null)
        {
            Deserializer = deserializer;
            Endpoint = endpoint;
            OptionalProcessing = optionalProcessing;
            Steps = BuildSteps(pipelineBuilder);
        }
        
        protected IEnumerable<IPipelineStep> BuildSteps(PipelineBuilder builder)
        {
            var builtSteps = builder
                .StartFrom(Endpoint)
                .DeserializeWith<T>(Deserializer);

            if (OptionalProcessing != null)
            {
                builtSteps = builtSteps.ThenInvoke(OptionalProcessing);
            }
                
            return builtSteps.BuildSteps();
        }
    }
}