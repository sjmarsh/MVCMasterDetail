using FluentValidation.Attributes;
using MVCMasterDetail.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCMasterDetail.Models
{
    [Validator(typeof(OrderValidator))]
    public class Order
    {
        public Order()
        {
            OrderItems = new List<OrderItem>();
        }

        public int Id { get; set; }

        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        
        public string Customer { get; set; }
        public virtual List<OrderItem> OrderItems { get; set; }
    }
}