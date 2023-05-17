using MauiAppToDo.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
//using ThreadNetwork;
using static System.Net.WebRequestMethods;
namespace MauiAppToDo.Services
{
    public class ToDoListService
    {
         private const string ApiUrl = "https://192.168.159.1:45455/api";
        //  private const string ApiUrl = "https://192.168.64.1:45457/api";

        private readonly HttpClient _httpClient;
        public ToDoListService()
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };

            _httpClient = new HttpClient(handler);
        }

        public async Task<ToDoLists> ToDoLists(int userId,ToDoLists toDoLists)
        {          
            var response = await _httpClient.PostAsJsonAsync($"{ApiUrl}/ToDos/AddToDoList?userId={userId}",toDoLists);
            response.EnsureSuccessStatusCode();
            var createdToDoList = await response.Content.ReadFromJsonAsync<ToDoLists>(); 
            return createdToDoList;
        }

        //belirli bir kullanıcıya ait tüm todo listelerini getirmek için
        public async Task<List<ToDoLists>> GetToDoLists(int userId)
        {
            var response = await _httpClient.GetAsync($"{ApiUrl}/ToDos/GetToDoLists?userId={userId}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<ToDoLists>>(content);
            }
            return null;
        }

        public async Task<ToDoLists> UpdateTitle(ToDoLists toDoLists)
        {
            var response = await _httpClient.PostAsJsonAsync($"{ApiUrl}/ToDos/UpdateTitle?id={toDoLists.Id}", toDoLists);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ToDoLists>();
        }

        //isActive 
        public async Task<ToDoLists> SetInactive(ToDoLists toDoLists)
        {
            var response = await _httpClient.PostAsJsonAsync($"{ApiUrl}/ToDos/PutInActive?Id={toDoLists.Id}", toDoLists);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ToDoLists>();
        }
        ////https://localhost:7072/api/ToDos/DeleteToDoList?id=60
        //public async Task<bool> DeleteToDoList(int id)
        //{
        //    var response = await _httpClient.DeleteAsync($"{ApiUrl}/ToDos/DeleteToDoList?id={id}");
        //    return response.IsSuccessStatusCode;
        //}

        //public async Task DeleteToDoList(int id)
        //{
        //    var response = await _httpClient.DeleteAsync($"{ApiUrl}/ToDos/DeleteToDoList?id={id}");
        //    response.EnsureSuccessStatusCode();
        //}

        //// bu kısım hatalı ????
        //////https://192.168.64.1:45457/api/ToDos/CompleteToDoList?id=59
        ////update 
        //public async Task<ToDoLists> CompleteToDoList(int id)
        //{
        //    var response = await _httpClient.PutAsync($"{ApiUrl}/ToDos/CompleteToDoList?id={id}", null);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        var content = await response.Content.ReadAsStringAsync();
        //        var updateToDoList = JsonConvert.DeserializeObject<ToDoLists>(content);
        //        return updateToDoList;
        //    }
        //    return null;

        //}

        public async Task<ToDoLists> Put(int id, bool isNewItem = false)
        {
            var response = await _httpClient.PutAsync($"{ApiUrl}/ToDos/Put?Id={id}", null);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var updatedList = JsonConvert.DeserializeObject<ToDoLists>(content);
                return updatedList;
            }
            return null;
        }

        // /ToDos/CompleteToDoList?id=8
        //complated
        public async Task<ToDoLists> ToDoComplated( ToDoLists toDoLists)
        {
            var response = await _httpClient.PostAsJsonAsync($"{ApiUrl}/ToDos/CompleteToDoList?id={toDoLists.Id}",toDoLists);
            response.EnsureSuccessStatusCode();
            //var content = await response.Content.ReadAsStringAsync();
            //var complatedToDoList = JsonConvert.DeserializeObject<ToDoLists>(content);
            //var complatedToDoList = await response.Content.ReadFromJsonAsync<ToDoLists>();
            //return complatedToDoList;
            return await response.Content.ReadFromJsonAsync<ToDoLists>();
        }

        internal object UpdateTitle(ToDoLists toDoLists, object todolist)
        {
            throw new NotImplementedException();
        }


        //public async Task<ToDoLists> Post(ToDoLists item)
        //{
        //    var serializedItem = JsonConvert.SerializeObject(item);
        //    var response = await _httpClient.PostAsync($"{ApiUrl}/ToDos/Post", new StringContent(serializedItem, Encoding.UTF8, "application/json"));
        //    if (response.IsSuccessStatusCode)
        //    {
        //        var content = await response.Content.ReadAsStringAsync();
        //        var createdItem = JsonConvert.DeserializeObject<ToDoLists>(content);
        //        return createdItem;
        //    }
        //    return null;
        //}


    }
}//
