using ParrotShopBackend.Application.DTO;
using ParrotShopBackend.Domain;

namespace ParrotShopBackend.Application.Mappers;



public static class UserMapper
{
    public static User FromRegFormDTO(RegFormDTO dto)
    {
        User user = new User()
        {
            Name = dto.Name == null ? Guid.NewGuid().ToString() : dto.Name,
            Username = dto.Username == null ? Guid.NewGuid().ToString() : dto.Username,
            Email = dto.Email,
            Role = Role.User
        };

        return user;
    }
}
