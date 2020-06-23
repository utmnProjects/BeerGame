using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// Класс описывает модель Личных ресурсов команды. 
/// </summary>
/// <remarks>
/// Ассоциируется с моделью Команды через HashSet. Связь генерируется EntityFramework
/// Модель необходимо переработать, т.к. на данный момент содержит всю информацию по Личным ресурсам без декомпозиции на Ингредиенты и Деньги команды
/// </remarks>
namespace HardGame.Models
{
    public partial class GameData
    {
        public GameData()
        {
            User = new HashSet<User>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        public int? Price { get; set; }
        public int? Malt { get; set; }
        public int? Yeast { get; set; }
        public int? Sugar { get; set; }
        public int? Hop { get; set; }
        public int? LagerBeer { get; set; }
        public int? StoutBeer { get; set; }
        public int? AleBeer { get; set; }

        //Генерируется EntityFramework. НЕ РЕДАКТИРОВАТЬ ВРУЧНУЮ
        [InverseProperty("GameData")]
        public virtual ICollection<User> User { get; set; }
    }
}
