using Lab9.Data.Entities;
using System.Collections.Generic;

namespace Lab9.Repository
{
    public interface IPetRepository
    {
        Pet GetPet(int id);
        IEnumerable<Pet> GetAllPets();
        IEnumerable<Pet> GetUsersPets(string userid);
        void CreatePet(Pet pet);
        void UpdatePet(Pet pet);
        void DeletePet(int id);
    }
}