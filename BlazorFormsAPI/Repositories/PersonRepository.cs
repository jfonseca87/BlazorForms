using BlazorFormsAPI.Models;
using Dapper;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorFormsAPI.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly string _connString;

        public PersonRepository()
        {
            _connString = "Server=localhost;Port=3306;Database=Test;Uid=root;Pwd=Abc.123456;";
        }

        public async Task<IEnumerable<Person>> GetPeople()
        {
            using var conn = new MySqlConnection(_connString);
            return await conn.QueryAsync<Person>("select * from person");
        }

        public async Task<Person> GetPersonById(int id)
        {
            using var conn = new MySqlConnection(_connString);
            return await conn.QuerySingleAsync<Person>($"select * from person where Id = {id}");
        }

        public async Task<bool> AddPerson(Person person)
        {
            using var conn = new MySqlConnection(_connString);
            int newId = await conn.ExecuteScalarAsync<int>(@$"insert into person 
                                           (FirstName, LastName, Email, PhoneNumber, Birthdate, IsActive, Comments, WorkingExperience, WorkingAbroad, SeniorityLevel)
                                           values ('{person.FirstName}', '{person.LastName}', '{person.Email}', '{person.PhoneNumber}', '{person.Birthdate:yyyy-MM-dd}', 
                                           {person.IsActive}, '{person.Comments}', {person.WorkingExperience}, {person.WorkingAbroad}, {person.SeniorityLevel});
                                           select last_insert_id();");

            return newId > 0;
        }

        public async Task<bool> UpdatePerson(Person person)
        {
            using var conn = new MySqlConnection(_connString);
            int rowsAffected = await conn.ExecuteAsync(@$"update person set
                                           FirstName = '{person.FirstName}', LastName = '{person.LastName}', Email = '{person.Email}', 
                                           PhoneNumber = '{person.PhoneNumber}', Birthdate = '{person.Birthdate:yyyy-MM-dd}', IsActive = {person.IsActive}, 
                                           Comments = '{person.Comments}', WorkingExperience = {person.WorkingExperience}, WorkingAbroad = {person.WorkingAbroad}, SeniorityLevel = {person.SeniorityLevel}
                                           where Id = {person.Id};");
            return rowsAffected > 0;
        }

        public async Task<bool> DeletePerson(int id)
        {
            using var conn = new MySqlConnection(_connString);
            int rowsAffected = await conn.ExecuteAsync($"delete from person where id = {id}");
            return rowsAffected > 0;
        }
    }
}