using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiAppToDo.Models;
using MauiAppToDo.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiAppToDo.ViewModel;

public partial class MainViewModel : ObservableObject
{
    private readonly ToDoListService _toDoListService;

    [ObservableProperty]
    string to_do;
    [ObservableProperty]
    string newtitle;
    [ObservableProperty]
    string to_do2;


    [ObservableProperty]
    ObservableCollection<ToDoLists> todoItems; 

    [ObservableProperty]
    ObservableCollection<ToDoLists> complatedList;

     


    public MainViewModel()
    {
        _toDoListService= new ToDoListService();
        TodoItems = new ObservableCollection<ToDoLists>();
        ComplatedList = new ObservableCollection<ToDoLists>();
    }

    private List<ToDoLists> _toDoLists;

    public List<ToDoLists> ToDoLists
    {
        get => _toDoLists;
        set => SetProperty(ref _toDoLists, value);
    }

     
    public async Task InitializeAsync()
    {
        int userId = Preferences.Get("UserId", -1);
        if (userId == -1) return;

        var toDoLists = await _toDoListService.GetToDoLists(userId);
        if (toDoLists != null)
        {
            foreach(var item in toDoLists)
            {
               if(item.IsComplete == 0)
                {
                    TodoItems.Add(item);
                }
                else
                {
                    complatedList.Add(item);
                }
            }
        }
    }


    [RelayCommand]
    private async Task AddToDo()
    {
        if (string.IsNullOrWhiteSpace(To_do))
            return;
        int id = Preferences.Get("UserId", -1);
        var newToDoList = new ToDoLists
        {
            Title = To_do,          
            UserId =id,
            Description = To_do2,
            IsComplete=0,
            IsActive=true,       
        };

        var createdToDoList = await _toDoListService.ToDoLists(newToDoList.UserId, newToDoList);

        if (createdToDoList != null)
        {
             TodoItems.Add(createdToDoList);
             To_do = string.Empty;          
        }     
    }


    [RelayCommand]
    async Task Delete(object item)
    {
        if (item is ToDoLists toDoLists)
        {
            var succes = await _toDoListService.SetInactive(toDoLists);
            if (succes != null)
            {
                TodoItems.Remove(toDoLists);
            }
            TodoItems.Remove(toDoLists);
        }
    }

    [RelayCommand]
    async Task UpdateTitle(ToDoLists todolist)
    {
        if (todolist == null) return;
        await _toDoListService.UpdateTitle(todolist);
    }

    //[RelayCommand]
    //void Delete(string del)
    //{
    //    if (TodoItems.Contains(del))
    //    {
    //        TodoItems.Remove(del);

    //    }
    //}


    [RelayCommand]
    async Task Update(ToDoLists item)
    {
        Debug.WriteLine(item.GetType().ToString());
        if (item is ToDoLists toDoLists)
        {
            var updatedToDo = await _toDoListService.Put(toDoLists.Id);
            if (updatedToDo != null)
            {

                ComplatedList.Add(updatedToDo);
                TodoItems.Remove(updatedToDo);

            }
        }
    }

    //[RelayCommand]
    //private async Task UpdateTitle()
    //{

    //    if (string.IsNullOrWhiteSpace(Newtitle))
    //        return;
    //    int id = Preferences.Get("UserId", -1);
    //    int todoid = Preferences.Get("Id", -1);

    //    {
    //        var newToDoList = new ToDoLists
    //        {
    //            Id = to,
    //            Title = Newtitle,
    //            UserId = id,
    //            Description = "null",
    //            IsComplete = 0,
    //            IsActive = true,
    //        };
    //        var updatedToDo = await _toDoListService.UpdateTitle(newToDoList);
    //        if (updatedToDo != null)
    //        {
    //            TodoItems.Add(updatedToDo);
    //            Newtitle = string.Empty;
    //        }
    //    }
    //}



    [RelayCommand]
    private async Task ComplatedToDo(ToDoLists item)
    {
        //////if (string.IsNullOrWhiteSpace(To_do))
        //////    return;
        ////int id = Preferences.Get("UserId", -1);
        ////var newToDoList = new ToDoLists;
        //////{
        //////    Title = To_do,
        //////    UserId = id,
        //////    Description = "null",
        //////    IsComplete = 0,
        //////};

        ////var complatedToDoList = await _toDoListService.ToDoComplated(newToDoList.Id, newToDoList);

        ////if (complatedToDoList != null)
        ////{
        ////    ComplatedList.Add(complatedToDoList);
        ////    To_do = string.Empty;
        ////    TodoItems.Remove(complatedToDoList);
        ////}
        //var selectedItem = new ToDoLists();
        //if (selectedItem != null)
        //{
        //    var result = await _toDoListService.ToDoComplated(selectedItem.Id, selectedItem);
        //    if (result != null)
        //    {
        //        // do something with the completed todo item
        //        ComplatedList.Add(result);

        //        TodoItems.Remove(result);
        //    }
        //}
        Debug.WriteLine(item.GetType().ToString());
        if (item is ToDoLists toDoLists)
        {
            var updatedToDo = await _toDoListService.ToDoComplated(toDoLists);
            if (updatedToDo != null)
            {

                ComplatedList.Add(updatedToDo);
                TodoItems.Remove(updatedToDo);

            }
        }
    }
    
    //[RelayCommand]
    //public async Task AddToDos(string title)
    //{
    //    var newToDo = new ToDoLists { Title = title};
    //    var createdToDo = await _toDoListService.Post(newToDo);
    //    if (createdToDo != null)
    //    {
    //        ComplatedList.Add(createdToDo);
    //    }
    //}

    //[RelayCommand]
    //async void Complated(ToDoLists toDoLists)
    //{
    //    var toDoList = await _toDoListService.CompleteToDoList(toDoLists.Id);
    //    if (toDoList != null)
    //    {

    //        ComplatedList.Add(toDoLists.Title);
    //        TodoItems.Remove(toDoLists.Title);
    //    }
    //}


}