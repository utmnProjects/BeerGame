using HardGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/// <summary>
/// Интерфейс сервиса комнаты  
/// </summary>
/// <remarks>
/// Создается EntityFramework
/// </remarks>
namespace HardGame.Services
{
    public interface IRoomService
    {

        /// <summary>
        /// Извлечение Комнаты по идентификатору
        /// </summary>
        /// <returns>
        /// Модель класса Room
        /// </returns>
        public Room GetCurrentRoomById(int id);

        /// <summary>
        /// Извлечение всех созданных комнат
        /// </summary>
        /// <returns>
        /// Коллекция объектов класса Room
        /// </returns>
        public List<Room> AllRooms();


        /// <summary>
        /// Создание новой комнаты
        /// </summary>
        /// <returns>
        /// Коллекция объектов класса Room
        /// </returns>
        /// <param name="name">Название комнаты</param>
        /// <param name="count">Максимальное кол-во команд</param>
        /// <param name="userId">Идентификатор Админа, который создает комнату</param>
        public int AddRoom(string name, int count, string userId);


        /// <summary>
        /// Удаление комнаты
        /// </summary>
        /// <param name="roomId">Идентификатор комнаты</param>
        public void RemoveRoom(int roomId);


        /// <summary>
        /// Проверка на создана ли комната
        /// </summary>
        /// <returns>
        /// Bool
        /// </returns>
        /// <param name="name">Название комнаты</param>
        public bool IsRoomExists(string name);


        /// <summary>
        /// Проверка на то, является ли User Админом комнаты
        /// </summary>
        /// <returns>
        /// Bool
        /// </returns>
        /// <param name="roomId">Идентификатор комнаты</param>
        /// <param name="userId">Идентификатор User</param>
        public bool IsRoomAdmin(int roomId, string userId);


        /// <summary>
        /// Выход из комнаты
        /// </summary>
        /// <param name="roomId">Идентификатор комнаты</param>
        /// <param name="userId">Идентификатор User</param>
        public void LeaveRoom(int roomId, string userId);


        /// <summary>
        /// Присоединение к комнате
        /// </summary>
        /// <param name="roomId">Идентификатор комнаты</param>
        /// <param name="userId">Идентификатор User</param>
        public void EnterRoom(int roomId, string userId);
    }
}
