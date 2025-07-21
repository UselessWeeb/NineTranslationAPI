using System.Threading.Tasks;
using ViewModels;

namespace Services.Interfaces
{
    public interface IPostService
    {
        Task<IEnumerable<ProjectDto>> getPostByNameAsync(string name);
    }
}
