using Lab5.Models.View;
using System.Collections.Generic;

namespace Lab5.Service
{
    public interface IPetService
    {
        PetViewModel GetPet(int id);
        IEnumerable<PetViewModel> GetUsersPets(int userid);
        void CreatePet(PetViewModel pet);
        void UpdatePet(PetViewModel pet);
        void DeletePet(int id);
    }
}