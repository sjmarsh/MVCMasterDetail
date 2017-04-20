using FluentValidation;
using MVCMasterDetail.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCMasterDetail.Validators
{
    public class OrderItemValidator : AbstractValidator<OrderItem>
    {
        public OrderItemValidator()
        {
            RuleFor(o => o.Name).NotEmpty();
            RuleFor(o => o.Quantity).GreaterThan(0);
            RuleFor(o => o.Price).GreaterThan(0);
        }
    }
}