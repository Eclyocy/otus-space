using GameController.Database.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameController.Database.Models
{
    public class User
    {
        [Key, Column("userid")]
        public Guid UserId { get; set; }

        [Column("nameuser")]
        public required string NameUser { get; set; }

        [Column("hashpass")]
        public required string HashPass { get; set; }

        public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();
    }
}
