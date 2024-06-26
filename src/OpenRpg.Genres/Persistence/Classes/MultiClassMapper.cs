using System.Linq;
using OpenRpg.Core.Classes;

namespace OpenRpg.Genres.Persistence.Classes
{
    public class MultiClassMapper : IMultiClassMapper
    {
        public IClassMapper ClassMapper { get; }

        public MultiClassMapper(IClassMapper classMapper)
        {
            ClassMapper = classMapper;
        }

        public IMultiClass Map(MultiClassData data)
        { return InitializeMultiClass(data); }
        
        public virtual IMultiClass InitializeMultiClass(MultiClassData data)
        { return new DefaultMultiClass(data.Classes.Select(ClassMapper.Map)); }
    }
}