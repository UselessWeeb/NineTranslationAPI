using Dto;
using System.Threading.Tasks;
using ViewModels;

namespace Services.Interfaces
{
    public interface IPostService
    {
        Task<IEnumerable<ProjectDto>> getPostByNameAsync(string name);
        Task CreatePostAsync(CreatePostDto postDto);
        Task DeletePostAsync(int id);
        Task DeletePostASync(string finder);
        Task DisablePost(string finder);
    }
}
