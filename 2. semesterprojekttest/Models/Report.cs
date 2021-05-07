﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace _2._semesterprojekttest.Models
{
    [Table("Report")]
    public partial class Report
    {
        [Key]
        [Column("ReportID")]
        public int ReportId { get; set; }
        public int Reporter { get; set; }
        public int Reported { get; set; }
        [Required]
        [StringLength(500)]
        public string Message { get; set; }

        [ForeignKey(nameof(Reported))]
        [InverseProperty(nameof(CruizeUser.ReportReportedNavigations))]
        public virtual CruizeUser ReportedNavigation { get; set; }
        [ForeignKey(nameof(Reporter))]
        [InverseProperty(nameof(CruizeUser.ReportReporterNavigations))]
        public virtual CruizeUser ReporterNavigation { get; set; }
    }
}