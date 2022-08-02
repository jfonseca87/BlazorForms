using Blazored.Toast.Services;
using BlazorForms.Extensions;
using BlazorForms.Models;
using BlazorForms.Utils;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BlazorForms.Services
{
    public class PersonService : IPersonService
    {
        private readonly HttpClient _httpClient;
        private readonly IToastService _toastService;
        private readonly IConfiguration _configuration;

        public PersonService(HttpClient httpClient, IToastService toastService, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _toastService = toastService;
            _configuration = configuration;
        }

        public async Task<ApiResponse<List<Person>>> GetPeople()
        {
            var response = await _httpClient.GetAsync($"{_configuration["baseUrl"]}/api/people");
            var (status, data, errors) = await response.ToClientResponse();

            ProcessErrorResponse(status, errors, "getting");

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
            var response = await _httpClient.GetAsync($"{_configuration["baseUrl"]}/api/people/{id}");
            var (status, data, errors) = await response.ToClientResponse();

            ProcessErrorResponse(status, errors, "getting");

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
            var response = await _httpClient.PostAsync($"{_configuration["baseUrl"]}/api/people", content);
            var (status, data, errors) = await response.ToClientResponse();

            ProcessErrorResponse(status, errors, "ceating");

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
            var response = await _httpClient.PutAsync($"{_configuration["baseUrl"]}/api/people", content);
            var (status, data, errors) = await response.ToClientResponse();

            ProcessErrorResponse(status, errors, "updating");

            return new ApiResponse<bool>
            {
                Status = status,
                Errors = errors,
                Data = status == ResponseStatus.Success && JsonConvert.DeserializeObject<bool>(data)
            };
        }

        public async Task<ApiResponse<bool>> DeletePerson(int personId)
        {
            var response = await _httpClient.DeleteAsync($"{_configuration["baseUrl"]}/api/people/{personId}");
            var (status, data, errors) = await response.ToClientResponse();

            ProcessErrorResponse(status, errors, "deleting");

            return new ApiResponse<bool>
            {
                Status = status,
                Errors = errors,
                Data = status == ResponseStatus.Success && JsonConvert.DeserializeObject<bool>(data)
            };
        }

        private void ProcessErrorResponse(ResponseStatus status, List<string> errors, string action)
        {
            Console.WriteLine(JsonConvert.SerializeObject(errors));

            switch (status)
            {
                case ResponseStatus.Error:
                    _toastService.ShowError($"An error ocurred {action} a person");
                    break;
                case ResponseStatus.Unauthorized:
                case ResponseStatus.Forbidden:
                    _toastService.ShowError(errors.FirstOrDefault());
                    break;
                default:
                    break;
            }
        }
    }
}
