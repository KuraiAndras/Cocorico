﻿using Cocorico.Shared.Contract;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cocorico.Server.Models.Entities.Sandwich
{
    public class Sandwich : IHashAssertable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public int GetAssertHash()
        {
            throw new NotImplementedException();
        }
    }
}
