using MVCMasterDetail.Data;
using MVCMasterDetail.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace MVCMasterDetail.Controllers
{
    public class OrderController : Controller
    {
        private OrderDbContext _dbContext;

        public OrderController()
        {
            _dbContext = new OrderDbContext();
        }

        // GET: Order
        public ActionResult Index()
        {
            var orders = _dbContext.Orders.ToList();
            return View(orders);
        }

        public ActionResult Create()
        {
            var order = new Order { Date = DateTime.Today };
            return View("Create", order);
        }
        
        [HttpPost]
        public ActionResult Create(Order order)
        {
            if(!ModelState.IsValid)
            {
                return View("Create", order);
            }

            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();
            
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var order = _dbContext.Orders.Include(o => o.OrderItems).First(o => o.Id == id);
            return View("Edit", order);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Order order)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", order);
            }

            var existingOrder = _dbContext.Orders.Include(o => o.OrderItems).First(o => o.Id == order.Id);
            _dbContext.Entry(existingOrder).CurrentValues.SetValues(order);

            if (order.OrderItems != null)
            {
                // remove deletions
                foreach (var existingOrderItem in existingOrder.OrderItems.ToList())
                {
                    if (!order.OrderItems.Any(i => i.Id == existingOrderItem.Id))
                    {
                        _dbContext.OrderItems.Remove(existingOrderItem);
                    }
                }

                // add/ update
                foreach (var orderItem in order.OrderItems)
                {
                    if (orderItem.Id > 0)
                    {
                        var existingOrderItem = existingOrder.OrderItems.First(i => i.Id == orderItem.Id);
                        _dbContext.Entry(existingOrderItem).CurrentValues.SetValues(orderItem);
                    }
                    else
                    {
                        existingOrder.OrderItems.Add(orderItem);
                    }
                }
             
                await _dbContext.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }


        public ActionResult Details(int id)
        {
            var order = _dbContext.Orders.Include(o => o.OrderItems).First(o => o.Id == id);
            return View("Details", order);
        }

        public ActionResult Delete(int id)
        {
            var order = _dbContext.Orders.Find(id);
            _dbContext.Orders.Remove(order);
            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult CreateOrderItem()
        {
            var orderItem = new OrderItem();
            return PartialView("~/Views/Shared/EditorTemplates/_OrderItem.cshtml", orderItem);
        }
    }
}