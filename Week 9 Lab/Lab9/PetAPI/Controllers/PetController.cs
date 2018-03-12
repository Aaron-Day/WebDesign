using Lab9.Models;
using Lab9.Models.View;
using Lab9.Repository;
using Lab9.Service;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace PetAPI.Controllers
{
    public class PetController : ApiController
    {
        private readonly IPetService _service;

        public PetController()
        {
            _service = new PetService(new PetRepository(new ApplicationDbContext()));
        }

        public IEnumerable<PetViewModel> GetAllPets()
        {
            return _service.GetAllPets();
        }

        public IHttpActionResult GetPet(int id)
        {
            try
            {
                var pet = _service.GetPet(id);
                if (pet == null)
                {
                    return NotFound();
                }
                return Ok(pet);
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }
    }
}
