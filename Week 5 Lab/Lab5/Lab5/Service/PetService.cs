using Lab5.Data.Entities;
using Lab5.Models.View;
using Lab5.Repository;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab5.Service
{
    public class PetService : IPetService
    {
        private readonly IPetRepository _repository;
        private readonly ILog _log = log4net.LogManager.GetLogger(typeof(PetService));

        public PetService(IPetRepository repository)
        {
            _repository = repository;
        }

        public PetViewModel GetPet(int id)
        {
            _log.Info($"GetPet, id: {id}");

            PetViewModel pet;
            try
            {
                pet = MapToPetViewModel(_repository.GetPet(id));
            }
            catch (Exception e)
            {
                _log.Error($"Failure to get pet: {id}", e);
                throw;
            }

            return pet;
        }

        public IEnumerable<PetViewModel> GetUsersPets(int userid)
        {
            _log.Info($"GetUsersPets, userid: {userid}");

            IEnumerable<PetViewModel> views;
            try
            {
                var pets = _repository.GetUsersPets(userid);
                views = pets.Select(MapToPetViewModel).ToList();
            }
            catch (Exception e)
            {
                _log.Error($"Failure to get users pets: {userid}", e);
                throw;
            }

            return views;
        }

        public void CreatePet(PetViewModel pet)
        {
            _log.Info($"CreatePet, user: {pet.UserId}");

            try
            {
                _repository.CreatePet(MapToPet(pet));
            }
            catch (Exception e)
            {
                _log.Error($"Failure to create pet for user: {pet.UserId}", e);
                throw;
            }
        }

        public void UpdatePet(PetViewModel pet)
        {
            _log.Info($"UpdatePet, pet: {pet.Id}, user: {pet.UserId}");

            try
            {
                var p = _repository.GetPet(pet.Id);
                CopyToPet(pet, p);
                _repository.UpdatePet(p);
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

            try
            {
                _repository.DeletePet(id);
            }
            catch (Exception e)
            {
                _log.Error($"Failure to delete pet: {id}", e);
                throw;
            }
        }

        //--------------------------------------------------//
        // METHODS

        private Pet MapToPet(PetViewModel pet)
        {
            _log.Info($"MapToPet, pet: {pet.Id}, user: {pet.UserId}");

            try
            {
                return new Pet
                {
                    Id = pet.Id,
                    Name = pet.Name,
                    Age = pet.Age,
                    NextCheckup = pet.NextCheckup,
                    VetName = pet.VetName,
                    UserId = pet.UserId
                };
            }
            catch (Exception e)
            {
                _log.Error($"Failure to map to pet: {pet.Id} for user: {pet.UserId}", e);
                throw;
            }
        }

        private PetViewModel MapToPetViewModel(Pet pet)
        {
            _log.Info($"MapToPetViewModel, pet: {pet.Id}, user: {pet.UserId}");

            try
            {
                return new PetViewModel
                {
                    Id = pet.Id,
                    Name = pet.Name,
                    Age = pet.Age,
                    NextCheckup = pet.NextCheckup,
                    VetName = pet.VetName,
                    UserId = pet.UserId,
                    CheckupAlert = pet.NextCheckup <= DateTime.Today.AddDays(14)
                };
            }
            catch (Exception e)
            {
                _log.Error($"Failure to map to PetViewModel: {pet.Id} for user: {pet.UserId}", e);
                throw;
            }
        }

        private void CopyToPet(PetViewModel view, Pet pet)
        {
            _log.Info($"CopyToPet, pet: {view.Id}, user: {view.UserId}");

            try
            {
                pet.Id = view.Id;
                pet.Name = view.Name;
                pet.Age = view.Age;
                pet.NextCheckup = view.NextCheckup;
                pet.VetName = view.VetName;
                pet.UserId = view.UserId;
            }
            catch (Exception e)
            {
                _log.Error($"Failure to copy to pet: {view.Id} for user: {view.UserId}", e);
                throw;
            }
        }
    }
}