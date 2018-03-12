using Lab9.Models.View;
using System.Collections.Generic;

namespace Lab9.Service
{
    public interface IPetService
    {
        PetViewModel GetPet(int id);
        IEnumerable<PetViewModel> GetAllPets();
        IEnumerable<PetViewModel> GetUsersPets(string userid);
        void CreatePet(PetViewModel pet);
        void UpdatePet(PetViewModel pet);
        void DeletePet(int id);
    }
}