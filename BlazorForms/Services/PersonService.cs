using BlazorForms.Extensions;
using BlazorForms.Models;
using BlazorForms.Utils;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorForms.Services
{
    public class PersonService : IPersonService
    {
        private readonly HttpClient _httpClient;

        public PersonService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        public async Task<ApiResponse<List<Person>>> GetPeople()
        {
            var response = await _httpClient.GetAsync("api/people");
            var (status, data, errors) = await response.ToClientResponse();

            return new ApiResponse<List<Person>>
            {
                Status = status,
                Errors = errors,
                Data = status == ResponseStatus.Success
                    ? JsonConvert.DeserializeObject<List<Person>>(data)
                    : new List<Person>()
            };
        }

        public async Task<ApiResponse<Person>> GetPersonById(int id)
        {
            var response = await _httpClient.GetAsync($"api/people/{id}");
            var (status, data, errors) = await response.ToClientResponse();

            return new ApiResponse<Person>
            {
                Status = status,
                Errors = errors,
                Data = status == ResponseStatus.Success
                    ? JsonConvert.DeserializeObject<Person>(data)
                    : new Person()
            };
        }
        
        public async Task<ApiResponse<bool>> AddNewPerson(Person person)
        {
            var json = JsonConvert.SerializeObject(person);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/people", content);
            var (status, data, errors) = await response.ToClientResponse();
            
            return new ApiResponse<bool>
            {
                Status = status,
                Errors = errors,
                Data = status == ResponseStatus.Success && JsonConvert.DeserializeObject<bool>(data)
            };
        }

        public async Task<ApiResponse<bool>> UpdatePerson(Person person)
        {
            var json = JsonConvert.SerializeObject(person);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync("api/people", content);
            var (status, data, errors) = await response.ToClientResponse();

            return new ApiResponse<bool>
            {
                Status = status,
                Errors = errors,
                Data = status == ResponseStatus.Success && JsonConvert.DeserializeObject<bool>(data)
            };
        }

        public async Task<ApiResponse<bool>> DeletePerson(int personId)
        {
            var response = await _httpClient.DeleteAsync($"api/people/{personId}");
            var (status, data, errors) = await response.ToClientResponse();

            return new ApiResponse<bool>
            {
                Status = status,
                Errors = errors,
                Data = status == ResponseStatus.Success && JsonConvert.DeserializeObject<bool>(data)
            };
        }
    }
}
