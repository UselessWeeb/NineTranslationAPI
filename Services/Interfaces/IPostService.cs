using Dto;
using System.Threading.Tasks;
using ViewModels;

namespace Services.Interfaces
{
    public interface IPostService
    {
        Task<IEnumerable<ProjectDto>> getPostByNameAsync(string name);
        Task DeletePostAsync(int id);
        Task DeletePostASync(string finder);
        Task DisablePost(string finder);
        Task<IEnumerable<ProjectDto>> GetAllPostsAsync();
    }
}
