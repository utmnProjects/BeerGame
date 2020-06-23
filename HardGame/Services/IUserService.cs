using HardGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/// <summary>
/// Интерфейс сервиса команды 
/// </summary>
/// <remarks>
/// Создается EntityFramework
/// </remarks>
namespace HardGame.Services
{
    public interface IUserService
    {
    	/// <summary>
        /// Извлечение команды
        /// </summary>
        /// <returns>
        /// Объект класса User
        /// </returns>
        /// <param name="id">Идентификатор команды</param>	
        public User GetCurrentUser(string id);

        /// <summary>
        /// Добавление команды в комнату
        /// </summary>
        /// <returns>
        /// Идентификатор добавленной команды
        /// </returns>
        /// <param name="userName">Название команды</param>
        /// <param name="connectId">Идентификатор комнаты</param>	
        public string Add(string userName, string connectId);


        /// <summary>
        /// Удаление команды из таблицы
        /// </summary>
        /// <param name="userId">Идентификатор команды</param>
        public void Remove(string userId);

        /// <summary>
        /// Проверка существует ли команда в таблице
        /// </summary>
        /// <returns>
        /// Bool
        /// </returns>
        /// <param name="name">Название команды</param>
        public bool IsExistsUser(string name);
    }
}
