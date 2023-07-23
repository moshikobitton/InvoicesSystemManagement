using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace server_side.Models
{
    public class Invoice
    {

        [Key]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Date { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string Status { get; set; }
        [Column(TypeName = "int")]
        public int Amount { get; set; }
    }

}
