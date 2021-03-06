// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace _2._semesterprojekttest.Models
{
    [Table("Request")]
    public partial class Request
    {
        [Key]
        [Column("RequestID")]
        public int RequestId { get; set; }
        [Column("UserID")]
        public int UserId { get; set; }
        [Column("RouteID")]
        public int RouteId { get; set; }
        [Required]
        [StringLength(500)]
        public string Message { get; set; }

        [ForeignKey(nameof(RouteId))]
        [InverseProperty("Requests")]
        public virtual Route Route { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(CruizeUser.Requests))]
        public virtual CruizeUser User { get; set; }
    }
}