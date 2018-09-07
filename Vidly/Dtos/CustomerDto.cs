using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Vidly.Models;

namespace Vidly.Dtos
{
    // Data transer objects
    public class CustomerDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public bool IsSubscribedToNewsLetter { get; set; }

        // Now replaces membershiptype in the table with a corresponding ID
        public byte MembershipTypeId { get; set; }

        public MembershipTypeDto MembershipType { get; set; }

        // So that the @Html.LabelFor(m => m.BirthDate) displays this text 
      //  [Min18YearIfAMember]
        public DateTime? BirthDate { get; set; }
    }
}