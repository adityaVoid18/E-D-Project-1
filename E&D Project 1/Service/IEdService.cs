using E_D_Project_1.Models;

namespace E_D_Project_1.Service
{
    public interface IEdService
    {
        Task<Ed1> RegisterUserAsync(Ed1 request);
        Task<string> LoginUserAsync(Ed1 request);
        Task<List<Ed1>> GetAllAsync();
    }
}
