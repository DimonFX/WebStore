﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Infrastructure.Interfaces;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IOrderService _ordersService;

        public ProfileController(IOrderService ordersService)
        {
            _ordersService = ordersService;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Orders()
        {
            var orders = _ordersService.GetUserOrders(User.Identity.Name);
            var orderModels = new List<UserOrderViewModel>(orders.Count());

            foreach (var order in orders)
            {
                orderModels.Add(new UserOrderViewModel()
                {
                    Id = order.Id,
                    Name = order.Name,
                    Address = order.Address,
                    Phone = order.Phone,
                    TotalSum = order.OrderItems.Sum(o => o.Price * o.Quantity)
                });
            }

            return View(orderModels);
        }
    }
}