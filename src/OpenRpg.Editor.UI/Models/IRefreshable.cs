using System.Threading.Tasks;

namespace OpenRpg.Editor.UI.Models;

public interface IRefreshable
{
    Task Refresh();
}