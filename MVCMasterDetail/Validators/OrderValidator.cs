using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using MVCMasterDetail.Models;

namespace MVCMasterDetail.Validators
{
    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator()
        {
            RuleFor(o => o.Customer).NotEmpty();
            RuleFor(o => o.Date).NotEmpty().LessThan(DateTime.Today.AddDays(1));
        }

    }
}