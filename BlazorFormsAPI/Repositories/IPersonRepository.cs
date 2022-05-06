using BlazorFormsAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorFormsAPI.Repositories
{
    public interface IPersonRepository
    {
        Task<bool> AddPerson(Person person);
        Task<bool> DeletePerson(int id);
        Task<IEnumerable<Person>> GetPeople();
        Task<Person> GetPersonById(int id);
        Task<bool> UpdatePerson(Person person);
    }
}