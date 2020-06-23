using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// Класс описывает модель Команды. 
/// </summary>
/// <remarks>
/// Содержит поля: 
//      Id - идентификатор команды
//      Name - название команды
//      RoomId - идентификатор комнаты, к которой подключена команда
//      GameDataId - идентификатор Личных ресурсов
/// </remarks>
namespace HardGame.Models
{
    public partial class User
    {
        [Key]
        [Column("ID")]
        [StringLength(200)]
        public string Id { get; set; }
        [StringLength(200)]
        public string Name { get; set; }
        [Column("ConnectID")]
        public string ConnectId { get; set; }
        [Column("RoomID")]
        public int? RoomId { get; set; }
        [Column("GameDataID")]
        public int? GameDataId { get; set; }

        //Генерируется EntityFramework. НЕ РЕДАКТИРОВАТЬ ВРУЧНУЮ
        [ForeignKey(nameof(GameDataId))]
        [InverseProperty("User")]
        public virtual GameData GameData { get; set; }
        [ForeignKey(nameof(RoomId))]
        [InverseProperty("User")]
        public virtual Room Room { get; set; }
    }
}
