using E_D_Project_1.Models;

namespace E_D_Project_1.Repository
{
    public interface IEdRepository
    {
       // Task<Ed1> AddUserAsync(User user);
        Task<ResponseDto> AddUserAsync(Ed1 ed1);
        Task<Ed1> GetUserByUsernameAsync(string username);
        Task<List<Ed1>> GetAllUsersAsync();

        
    }
}
