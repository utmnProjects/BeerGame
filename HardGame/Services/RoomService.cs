using HardGame.Data;
using HardGame.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace HardGame.Services
{
    public class RoomService : IRoomService
    {
        BeerHouseGameContext context;
        public RoomService(BeerHouseGameContext context)
        {
            this.context = context;
        }

        public int AddRoom(string name, int count, string userId)
        {
            var currentRoom = new Room
            {
                Name     = name,
                MaxCount = count,
                AdminId  = userId
            };

            context.Room.Add(currentRoom);
            context.SaveChanges();
            return currentRoom.Id;
        }

        public void RemoveRoom(int roomId)
        {
            var currentRoom = GetCurrentRoomById(roomId);
            context.Remove(currentRoom);
            context.SaveChanges();
        }


        public bool IsRoomExists(string name)
        {
            return context.Room
                .FirstOrDefault(x => x.Name == name) != null;
        }

        public List<Room> AllRooms()
        {
            return context.Room
                .Include(x => x.User)
                .Where(x => x.User.Count > 0)
                .ToList();
        }

        public Room GetCurrentRoomById(int id)
        {
            return context.Room
                .Include(x => x.User)
                .AsNoTracking()
                .FirstOrDefault(x => x.Id == id);
        }

        public bool IsRoomAdmin(int roomId, string userId)
        {
            var currentRoom = context.Room.FirstOrDefault(x => x.Id == roomId);
            if (currentRoom == null)
                throw new Exception("Комната не найдена");

            return currentRoom.AdminId == userId;
        }

        public void LeaveRoom(int roomId, string userId)
        {
            var currentRoom = context.Room.Include(x => x.User).FirstOrDefault(x => x.Id == roomId);
            if (currentRoom == null)
                throw new Exception("Комната не найдена");

            currentRoom.User.Remove(
                currentRoom.User.FirstOrDefault(x => x.Id == userId)
            );

            context.SaveChanges();

            var currentRoom2 = context.Room.Include(x => x.User).FirstOrDefault(x => x.Id == roomId);
        }

        public void EnterRoom(int roomId, string userId)
        {
            var currentRoom = context.Room.Include(x => x.User).FirstOrDefault(x => x.Id == roomId);
            if (currentRoom == null)
                throw new Exception("Комната не найдена");

            var currentUser = context.User.FirstOrDefault(x => x.Id == userId);
            if (currentRoom == null)
                throw new Exception("Пользователь не найдена");

            currentRoom.User.Add(currentUser);

            context.SaveChanges();
        }
    }
}
