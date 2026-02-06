using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using ParrotShopBackend.Application.DTO;
using ParrotShopBackend.Application.Exceptions;
using ParrotShopBackend.Application.Mappers;
using ParrotShopBackend.Domain;
using ParrotShopBackend.Infrastructure.Repos;

namespace ParrotShopBackend.Application.Services;



public class AuthService(IUserService _userSvc, IUserRepository _userRepo) : IAuthService
{
    public async Task RegisterAsync(RegFormDTO rfDTO)
    {
        User user = UserMapper.FromRegFormDTO(rfDTO);
        PasswordHasher<User> passwordHasher = new PasswordHasher<User>();
        user.PasswordHash = passwordHasher.HashPassword(user, rfDTO.Password!);
        try
        {
            await _userRepo.AddUserToDBAsync(user);
        }
        catch (DbUpdateException e) when (e.InnerException is PostgresException pgr)
        {
            if (pgr.SqlState == "23505")
            {
                if (pgr.ConstraintName == "AK_Users_Username")
                {
                    throw new UserAlreadyExistsException("Username already exists.");
                }
                else if (pgr.ConstraintName == "IX_Users_Email")
                {
                    throw new UserAlreadyExistsException("Email already exists.");
                }
                else
                {
                    throw;
                }
            }
        }

    }


}