﻿using FluentValidation.Attributes;
using MVCMasterDetail.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCMasterDetail.Models
{
    [Validator(typeof(OrderItemValidator))]
    public class OrderItem
    {
        public int Id { get; set; }
     
        public string Name { get; set; }
     
        public int Quantity { get; set; }
     
        public decimal Price { get; set; }

        
        public virtual Order Order { get; set; }
    }
}