using OpenRpg.AdviceEngine.Considerations;
using OpenRpg.AdviceEngine.Handlers.Considerations;

namespace OpenRpg.AdviceEngine.Extensions
{
    public static class IConsiderationHandlerExtensions
    {
        public static void RemoveConsideration(this IConsiderationHandler handler, IConsideration consideration)
        { handler.RemoveConsideration(consideration); }
    }
}