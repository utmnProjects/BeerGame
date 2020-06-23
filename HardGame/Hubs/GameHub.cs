using HardGame.Data;
using HardGame.Models;
using HardGame.Services;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


/// <summary>
/// Хаб, который содержит логику создания и подключения к игре (подробнее про хабы см. документацию SignalR)
/// </summary>
/// <remarks>
/// Реализованы методы: создание новой игры, подключение к игре, выход из игры
/// Про сервисы см. документацию EntityFramework
/// </remarks>
namespace HardGame.Hubs
{
    public class GameHub : Hub
    {
        //Сервис команд
        private IUserService userService;
        //Сервис комнаты
        private IRoomService roomService;
        //Подключенные игроки к конкретной комнате
        private static Dictionary<string, string> connectedUsers = new Dictionary<string, string>();
        
        public GameHub(IUserService userService, IRoomService roomService)
        {
            this.userService = userService;
            this.roomService = roomService;
        }

        public async Task SignInAsync(string userName)
        {
            var userId = userService.Add(userName, Context.ConnectionId);
            connectedUsers.Add(Context.ConnectionId, userId);
            //Add in main group and send user to load roomList
            await Groups.AddToGroupAsync(Context.ConnectionId, "MainGroup");
            await Clients.Caller.SendAsync("LoadRoomList", userId);
        }

        public async Task CreateNewRoomAsync(string roomName, int maxCount)
        {
            var roomId = roomService.AddRoom(roomName, maxCount, connectedUsers[Context.ConnectionId]);
            JoinRoomAsync(roomId).Wait();
            //After created room update of all users pages 
            await Clients.Group("MainGroup").SendAsync("UpdateRoomList");
        }

        public async Task JoinRoomAsync(int roomId)
        {
            roomService.EnterRoom(roomId, connectedUsers[Context.ConnectionId]);
            //Send to caller load room 
            await Clients.Caller.SendAsync("EnterRoom", roomId);
            //Send to users in group room update page
            Clients.Group("Room=" + roomId.ToString()).SendAsync("UpdateRoom").Wait();
            //Add to group room
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "MainGroup");
            await Groups.AddToGroupAsync(Context.ConnectionId, "Room=" + roomId.ToString());
        }

        public async Task LeaveRoomAsync(int roomId)
        {
            //leave user from this room
            roomService.LeaveRoom(roomId, connectedUsers[Context.ConnectionId]);
            Task.Run(async () => {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, "Room=" + roomId.ToString());
                await Groups.AddToGroupAsync(Context.ConnectionId, "MainGroup");
                await Clients.Caller.SendAsync("LeaveRoom");
            }).Wait();
            //if leaved admin thin all other users must be leaved 
            if (roomService.IsRoomAdmin(roomId, connectedUsers[Context.ConnectionId])) {
                LeaveAdmin(roomId).Wait();
            }
            //else they pages must be updated
            else {
                Clients.Group("Room=" + roomId.ToString())
                    .SendAsync("UpdateRoom")
                    .Wait();
            }
        }

        public async Task LeaveAdmin(int roomId)
        {
            //give a deleting room
            var room = roomService.GetCurrentRoomById(roomId);
            //delete room
            roomService.RemoveRoom(roomId);
            //send users in roomList to update
            await Clients.Group("MainGroup").SendAsync("UpdateRoomList");
            //send users in room admin has leaved a room
            await Clients.Group("Room="+roomId).SendAsync("ErrorMessage", "Администратор покинул комнату.");
            //for each users in room send to leave from this room and load roomList
            foreach (var user in room.User){
                await Groups.AddToGroupAsync(user.ConnectId, "MainGroup");
                await Groups.RemoveFromGroupAsync(user.ConnectId, "Room=" + roomId);
                await Clients.Client(user.ConnectId)
                    .SendAsync("LeaveRoom");
            }
        }

        public override Task OnConnectedAsync()
        {
            Console.WriteLine("Connected");
            return base.OnConnectedAsync();
        }

        /*
            Метод работает некорректно. 
            Необходимо сделать так, чтобы при перезагрузке страницы команда отключалась от игры.
        */
        public override async Task OnDisconnectedAsync(Exception e)
        {
            Console.WriteLine($"Disconnected {e?.Message} {Context.ConnectionId}");
            await Task.Run(() =>
            {
                userService.Remove(connectedUsers[Context.ConnectionId]);
                connectedUsers.Remove(Context.ConnectionId);
            });
            await base.OnDisconnectedAsync(e);
        }
    }
}
