using Microsoft.AspNetCore.JsonPatch;
using ParrotShopBackend.Application.DTO;
using ParrotShopBackend.Application.Exceptions;
using ParrotShopBackend.Application.Extensions;
using ParrotShopBackend.Application.Mappers;
using ParrotShopBackend.Domain;
using ParrotShopBackend.Infrastructure.Repos;

namespace ParrotShopBackend.Application.Services;



public class ParrotService(IParrotRepository _parrotRepo, RedisCacheExtension _redis) : IParrotService
{
    public async Task AddTraitToParrotAsync(long Id, TraitsDTO tDTO)
    {
        Parrot? parrot = await _parrotRepo.GetParrotByIdAsync(Id, true);
        if (parrot is null) throw new ParrotDoesntExistException("Parrot doesn't exist");
        if (parrot.Traits is not null) throw new InvalidFormException("Parrot already has traits, consider patching.");
        parrot.Traits = TraitsMapper.FromTraitsDTO(tDTO);
        //await _redis.UpdateCacheOnNewInfo(Id, parrot.Traits);
        await _parrotRepo.UpdateParrotAsync();
    }

    public async Task CreateNewParrotAsync(NewParrotDTO npDTO)
    {
        Parrot parrot = ParrotMapper.FromParrotDTO(npDTO);
        await _parrotRepo.AddAsync(parrot);
    }

    public async Task<List<Parrot>> FilterParrotsAsync(ParrotFilterDTO filterDTO)
    {
        List<long> Ids = await _redis.ApplyFilterAsync(filterDTO);
        List<Parrot?> parrots = [];
        foreach (long Id in Ids)
        {
            parrots.Add(await _parrotRepo.GetParrotByIdAsync(Id, true));
        }
        return parrots;

    }

    public async Task<List<Parrot>> GetAllParrotsAsync(bool ignoreSoftDelFilter = false)
    {
        return await _parrotRepo.GetAllParrotsAsync(ignoreSoftDelFilter);
    }

    public Task<Parrot?> GetParrotByIdAsync(long Id, bool includeTraits = false)
    {
        return _parrotRepo.GetParrotByIdAsync(Id, includeTraits);
    }

    public async Task RemoveParrotAsync(long Id)
    {
        await _parrotRepo.RemoveAsync(Id);
    }

    public async Task<Parrot?> RestoreParrotAsync(long ParrotId)
    {
        return await _parrotRepo.RestoreParrotAsync(ParrotId);
    }

    public async Task SoftDeleteParrotAsync(long ParrotId)
    {
        await _parrotRepo.SoftDeleteParrotAsync(ParrotId);
    }

    public async Task<bool> UpdateParrotAsync(long Id, JsonPatchDocument<Parrot> patchDoc)
    {
        Parrot? parrot = await _parrotRepo.GetParrotByIdAsync(Id);
        if (parrot is null) return true;
        bool hasError = false;
        patchDoc.ApplyTo(   parrot,
                            onError =>
                            {
                                hasError = true;
                            });
        if (!hasError) {await _parrotRepo.UpdateParrotAsync(); return false;}
        else return true;
    }
}