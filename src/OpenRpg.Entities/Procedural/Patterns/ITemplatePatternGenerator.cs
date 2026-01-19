using System.Collections.Generic;
using OpenRpg.Core.Templates;

namespace OpenRpg.Entities.Procedural.Patterns;

public interface ITemplatePatternGenerator<out TOut, TConfig> 
    where TOut : ITemplate
    where TConfig : PatternGeneratorVariables
{
    public IReadOnlyCollection<TOut> Generate(TConfig config);
}