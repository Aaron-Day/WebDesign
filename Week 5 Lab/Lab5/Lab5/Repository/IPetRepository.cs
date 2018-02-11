using Lab5.Data.Entities;
using System.Collections.Generic;

namespace Lab5.Repository
{
    public interface IPetRepository
    {
        Pet GetPet(int id);
        IEnumerable<Pet> GetUsersPets(int userid);
        void CreatePet(Pet pet);
        void UpdatePet(Pet pet);
        void DeletePet(int id);
    }
}