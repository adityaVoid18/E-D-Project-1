using E_D_Project_1.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_D_Project_1.Repository
{
    public class EdRepository : IEdRepository
    {
        private readonly edDBContext _edDBContext;

        public EdRepository(edDBContext edDBContext)
        {
            _edDBContext = edDBContext;
        }
        public async Task<ResponseDto> AddUserAsync(Ed1 ed1)
        {
            await _edDBContext.Ed1s.AddAsync(ed1);  // Add
            await _edDBContext.SaveChangesAsync();  // Save the data 

            ResponseDto responseDto = new ResponseDto()
            {
                Message = "SUCCESS",
                StatusCode = 0
            };
            return responseDto;
        }

        public async Task<List<Ed1>> GetAllUsersAsync()
        {
            return await _edDBContext.Ed1s.ToListAsync();
        }

        public async Task<Ed1> GetUserByUsernameAsync(string username)
        {
            return await _edDBContext.Ed1s.FirstOrDefaultAsync(u => u.Username == username);    
        }
    }
}
