﻿using Blazored.Toast.Services;
using BlazorForms.Extensions;
using BlazorForms.Models;
using BlazorForms.Utils;
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
        private readonly ILocalStorageService _localStorageService;

        public PersonService(HttpClient httpClient, IToastService toastService, ILocalStorageService localStorageService)
        {
            _httpClient = httpClient;
            _toastService = toastService;
            _localStorageService = localStorageService;
        }

        public async Task<ApiResponse<List<Person>>> GetPeople()
        {
            await SetDefaultHeaders();
            var response = await _httpClient.GetAsync("api/people");
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
            await SetDefaultHeaders();
            var response = await _httpClient.GetAsync($"api/people/{id}");
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
            await SetDefaultHeaders();
            var json = JsonConvert.SerializeObject(person);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/people", content);
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
            await SetDefaultHeaders();
            var json = JsonConvert.SerializeObject(person);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync("api/people", content);
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
            await SetDefaultHeaders();
            var response = await _httpClient.DeleteAsync($"api/people/{personId}");
            var (status, data, errors) = await response.ToClientResponse();

            ProcessErrorResponse(status, errors, "deleting");

            return new ApiResponse<bool>
            {
                Status = status,
                Errors = errors,
                Data = status == ResponseStatus.Success && JsonConvert.DeserializeObject<bool>(data)
            };
        }

        private async Task SetDefaultHeaders()
        {
            string token = await _localStorageService.GetStringValue("token");
            _httpClient.DefaultRequestHeaders.Authorization = string.IsNullOrEmpty(token)
                ? null
                : new AuthenticationHeaderValue("Bearer", token);
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
