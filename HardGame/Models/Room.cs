using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// Класс описывает модель Комнаты игры. 
/// </summary>
/// <remarks>
/// Содержит поля: 
//      Id - идентификатор комнаты
//      Name - название комнаты
//      MaxCount - максимальное кол-во участников
//      GameSettingsId - данные, загруженные Админом 
//      AdminId - идентификатор создателя комнаты
/// </remarks>
namespace HardGame.Models
{
    public partial class Room
    {
        public Room()
        {
            User = new HashSet<User>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        public int? MaxCount { get; set; }
        [Column("GameSettingsID")]
        public int? GameSettingsId { get; set; }
        [Column("AdminID")]
        [StringLength(200)]
        public string AdminId { get; set; }

        //Генерируется EntityFramework. НЕ РЕДАКТИРОВАТЬ ВРУЧНУЮ
        [InverseProperty("Room")]
        public virtual ICollection<User> User { get; set; }
    }
}
