using BlazorForms.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorForms.Services
{
    public interface IPersonService
    {
        Task<ApiResponse<bool>> AddNewPerson(Person person);
        Task<ApiResponse<bool>> DeletePerson(int personId);
        Task<ApiResponse<List<Person>>> GetPeople();
        Task<ApiResponse<Person>> GetPersonById(int id);
        Task<ApiResponse<bool>> UpdatePerson(Person person);
    }
}