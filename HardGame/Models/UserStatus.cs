using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/// <summary>
/// Перечисление содержит статусы Команды
/// </summary>
/// <remarks>
/// Статусы: 
///    NewUser - создана новая команда
///    InRoomList - создана комната 
///    InRoom - подключена к комнате 
///    InGame - учавствует в игре
/// </remarks>
namespace HardGame.Models
{
    public enum UserStatus
    {
        NewUser,
        InRoomList,
        InRoom,
        InGame
    }
}
