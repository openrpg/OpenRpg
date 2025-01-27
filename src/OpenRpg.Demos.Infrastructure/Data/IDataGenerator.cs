using System.Collections;
using System.Collections.Generic;

namespace OpenRpg.Demos.Infrastructure.Data;

public interface IDataGenerator<out T>
{
    IEnumerable<T> GenerateData();
}