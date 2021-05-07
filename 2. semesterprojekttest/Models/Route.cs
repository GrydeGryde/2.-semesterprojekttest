﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace _2._semesterprojekttest.Models
{
    [Table("Route")]
    public partial class Route
    {
        public Route()
        {
            Passengers = new HashSet<Passenger>();
        }

        [Key]
        [Column("RouteID")]
        public int RouteId { get; set; }
        [Column("UserID")]
        public int UserId { get; set; }
        [Required]
        [StringLength(100)]
        public string Start { get; set; }
        [Required]
        [StringLength(100)]
        public string Goal { get; set; }
        public int Day { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Arrival { get; set; }
        public int Space { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(CruizeUser.Routes))]
        public virtual CruizeUser User { get; set; }
        [InverseProperty(nameof(Passenger.Route))]
        public virtual ICollection<Passenger> Passengers { get; set; }
    }
}