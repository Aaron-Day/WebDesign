using Lab9.Data.Entities;
using Lab9.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab9.Repository
{
    public class PetRepository : IPetRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILog _log = LogManager.GetLogger(typeof(PetRepository));

        public PetRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Pet GetPet(int id)
        {
            _log.Info($"GetPet, id: {id}");
            try
            {
                return _context.Pets.Find(id);
            }
            catch (Exception e)
            {
                _log.Error($"Failure to get pet: {id}", e);
                throw;
            }
        }

        public IEnumerable<Pet> GetAllPets()
        {
            return _context.Pets.ToList();
        }

        public IEnumerable<Pet> GetUsersPets(string userid)
        {
            _log.Info($"GetUsersPets, userid: {userid}");
            try
            {
                return _context.Pets.Where(p => p.UserId == userid).ToList();
            }
            catch (Exception e)
            {
                _log.Error($"Failure to get all pets for user: {userid}", e);
                throw;
            }
        }

        public void CreatePet(Pet pet)
        {
            _log.Info($"CreatePet, user: {pet.UserId}");
            try
            {
                _context.Pets.Add(pet);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                _log.Error($"Failure to create pet for user: {pet.UserId}", e);
                throw;
            }
        }

        public void UpdatePet(Pet pet)
        {
            _log.Info($"UpdatePet, pet: {pet.Id}, user: {pet.UserId}");
            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                _log.Error($"Failure to update pet: {pet.Id} for user: {pet.UserId}", e);
                throw;
            }
        }

        public void DeletePet(int id)
        {
            _log.Info($"DeletePet, id: {id}");

            Pet pet;
            try
            {
                pet = _context.Pets.Find(id);
            }
            catch (Exception e)
            {
                _log.Error($"Failure to find pet: {id}", e);
                throw;
            }

            if (pet == null)
            {
                _log.Debug($"Unable to find pet: {id}");
                return;
            }

            try
            {
                _context.Pets.Remove(pet);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                _log.Error($"Failure to remove pet: {id} for user: {pet.UserId}", e);
                throw;
            }
        }
    }
}