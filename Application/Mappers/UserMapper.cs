using ParrotShopBackend.Application.DTO;
using ParrotShopBackend.Domain;

namespace ParrotShopBackend.Application.Mappers;



public static class UserMapper
{
    public static User FromDTO(UserDTO dto)
    {
        User user = new User()
        {
            Name = dto.Name,
            Username = dto.Username,
            Email = dto.Email,
            Role = Role.User
        };

        return user;
    }
}
