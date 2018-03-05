﻿using Lab8.Data.Entities;
using Lab8.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab8.Repository
{
    public class PetRepository : IPetRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILog _log = log4net.LogManager.GetLogger(typeof(PetRepository));

        public PetRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Pet GetPet(int id)
        {
            _log.Info($"GetPet, id: {id}");

            Pet pet;
            try
            {
                pet = _context.MyPets.Find(id);
            }
            catch (Exception e)
            {
                _log.Error($"Failure to get pet: {id}", e);
                throw;
            }
            return pet;
        }

        public IEnumerable<Pet> GetUsersPets(int userid)
        {
            _log.Info($"GetUsersPets, userid: {userid}");

            IEnumerable<Pet> pets;
            try
            {
                pets = _context.MyPets.Where(p => p.UserId == userid).ToList();
            }
            catch (Exception e)
            {
                _log.Error($"Failure to get all pets for user: {userid}", e);
                throw;
            }
            return pets;
        }

        public void CreatePet(Pet pet)
        {
            _log.Info($"CreatePet, user: {pet.UserId}");

            try
            {
                _context.MyPets.Add(pet);
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
                pet = _context.MyPets.Find(id);
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
                _context.MyPets.Remove(pet);
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