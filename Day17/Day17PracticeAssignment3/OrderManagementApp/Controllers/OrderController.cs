using Microsoft.AspNetCore.Mvc;
using OrderManagementApp.Models;

namespace OrderManagementApp.Controllers
{
    public class OrderController : Controller
    {
        // In-memory data for demo
        private static List<Customer> customers = new List<Customer>
        {
            new Customer { CustomerId = 1, FirstName = "Raj", LastName = "Kumar", Email = "raj@gmail.com", PhoneNumber = "9876543210", Address = "123 Main St, Delhi" },
            new Customer { CustomerId = 2, FirstName = "Priya", LastName = "Singh", Email = "priya@gmail.com", PhoneNumber = "9765432109", Address = "456 Oak Ave, Mumbai" }
        };

        private static List<Order> orders = new List<Order>
        {
            new Order { OrderId = 1, CustomerId = 1, OrderDate = DateTime.Now, DeliveryDate = DateTime.Now.AddDays(5), Status = "Processing", TotalAmount = 50000 },
            new Order { OrderId = 2, CustomerId = 2, OrderDate = DateTime.Now.AddDays(-2), DeliveryDate = DateTime.Now.AddDays(10), Status = "Pending", TotalAmount = 75000 }
        };

        private static List<OrderItem> orderItems = new List<OrderItem>
        {
            new OrderItem { OrderItemId = 1, OrderId = 1, ProductName = "Laptop", Quantity = 1, UnitPrice = 50000, DiscountPercent = 0 },
            new OrderItem { OrderItemId = 2, OrderId = 2, ProductName = "Mouse", Quantity = 100, UnitPrice = 500, DiscountPercent = 10 }
        };

        // GET: order/list or /order
        [HttpGet("")]
        [HttpGet("list")]
        [Route("list")]
        public IActionResult Index([FromQuery] int? customerId = null, [FromQuery] string? status = null)
        {
            var result = orders;
            
            if (customerId.HasValue)
                result = result.Where(o => o.CustomerId == customerId.Value).ToList();
            
            if (!string.IsNullOrEmpty(status))
                result = result.Where(o => o.Status == status).ToList();

            ViewBag.Customers = customers;
            return View(result);
        }

        // GET: order/details/1
        [HttpGet("details/{id:int}")]
        public IActionResult Details(int id)
        {
            var order = orders.FirstOrDefault(o => o.OrderId == id);
            if (order == null) return NotFound();

            order.Customer = customers.FirstOrDefault(c => c.CustomerId == order.CustomerId);
            order.Items = orderItems.Where(oi => oi.OrderId == id).ToList();

            return View(order);
        }

        // GET: order/create
        [HttpGet("create")]
        public IActionResult Create()
        {
            ViewBag.Customers = customers;
            return View(new Order());
        }

        // POST: order/create
        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Order order)
        {
            // Model-level validation runs automatically
            if (!ModelState.IsValid)
            {
                ViewBag.Customers = customers;
                return View(order);
            }

            order.OrderId = orders.Max(o => o.OrderId) + 1;
            orders.Add(order);

            return RedirectToAction(nameof(Details), new { id = order.OrderId });
        }

        // GET: order/edit/1
        [HttpGet("edit/{id:int}")]
        public IActionResult Edit(int id)
        {
            var order = orders.FirstOrDefault(o => o.OrderId == id);
            if (order == null) return NotFound();

            ViewBag.Customers = customers;
            return View(order);
        }

        // POST: order/edit/1
        [HttpPost("edit/{id:int}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Order order)
        {
            if (id != order.OrderId) return BadRequest();

            // Custom validation
            if (order.DiscountAmount < 0)
                ModelState.AddModelError("DiscountAmount", "Discount cannot be negative");

            if (!ModelState.IsValid)
            {
                ViewBag.Customers = customers;
                return View(order);
            }

            var existingOrder = orders.FirstOrDefault(o => o.OrderId == id);
            if (existingOrder != null)
            {
                existingOrder.CustomerId = order.CustomerId;
                existingOrder.DeliveryDate = order.DeliveryDate;
                existingOrder.Status = order.Status;
                existingOrder.ShippingAddress = order.ShippingAddress;
                existingOrder.DiscountAmount = order.DiscountAmount;
            }

            return RedirectToAction(nameof(Details), new { id = order.OrderId });
        }

        // GET: order/search/query
        [HttpGet("search/{query}")]
        public IActionResult Search(string query)
        {
            var results = orders.Where(o => 
                o.Status.Contains(query, StringComparison.OrdinalIgnoreCase)).ToList();
            
            return Json(results);
        }

        // Remote validation endpoint
        [AcceptVerbs("Get", "Post")]
        [Route("ValidateOrder")]
        public IActionResult ValidateOrder(DateTime orderDate, DateTime deliveryDate)
        {
            if (deliveryDate <= orderDate)
                return Json("Delivery date must be after order date");
            
            return Json(true);
        }
    }
}
