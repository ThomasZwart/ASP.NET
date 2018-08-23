﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public bool IsSubscribedToNewsLetter { get; set; }
   
        public MembershipType MembershipType { get; set; }

        [Display(Name = "Membership Type")]
        // Now replaces membershiptype in the table with a corresponding ID
        public byte MembershipTypeId { get; set; }

        // So that the @Html.LabelFor(m => m.BirthDate) displays this text
        [Display(Name = "Date of Birth")]
        public DateTime? BirthDate { get; set; }
    }
}