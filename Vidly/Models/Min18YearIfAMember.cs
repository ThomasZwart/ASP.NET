using System;
using System.Collections.Generic;
// For the validationattribute
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Min18YearIfAMember : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // Het validatie object
            var customer = (Customer)validationContext.ObjectInstance;

            // Als er geen membershiptype is dan krijgt die een error en niet de birthday (niet teveel errors tegelijk)
            if (customer.MembershipTypeId == MembershipType.Unknown || customer.MembershipTypeId == MembershipType.PayAsYouGo) {
                return ValidationResult.Success;
            }

            if (customer.BirthDate == null) {
                return new ValidationResult("Please enter birthdate");
            }

            var age = DateTime.Today.Year - customer.BirthDate.Value.Year;

            return (age >= 18) ? ValidationResult.Success : new ValidationResult("Customer should be 18 years old");
        }
    }
} 