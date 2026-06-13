using Backend.Application.Models.DTOs;

namespace Backend.Application.Models.Responses
{
    public class GetAllActiveUsersRes
    {
        public IList<UserDTO> Data { get; set; }
    }
}
