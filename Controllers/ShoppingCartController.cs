using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeBanHang.Models;
using WeBanHang.Models.EF;

namespace WeBanHang.Controllers
{
    public class ShoppingCartController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: ShoppingCart
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Checkout()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                ViewBag.CheckCart = cart;
            }
            return View();
        }
        public ActionResult CheckoutSuccess() 
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Checkout(Order order)
        {
            if (ModelState.IsValid)
            {
                ShoppingCart cart = (ShoppingCart)Session["Cart"];
                if (cart != null)
                {
                    cart.items.ForEach(item => order.OrderDetails.Add(new OrderDetail
                    {
                        ProductId = item.ProductId,
                        Quantity= item.Quantity,
                        Price= item.Price,
                    }));
                    order.TotalAmount = cart.items.Sum(item=>(item.Quantity*item.Price));
                    order.CreatedDate = DateTime.Now;
                    order.ModifiedDate = DateTime.Now;
                    order.CreatedBy = order.CustomerName;
                    Random x = new Random();
                    order.Code = "DH" + x.Next(0, 9) + x.Next(0, 9) + x.Next(0, 9) + x.Next(0, 9) + x.Next(0, 9);
                    db.Orders.Add(order);
                    db.SaveChanges();

                    var strSanPham = "";
                    var ThanhTien = Decimal.Zero;
                    var TongTien = Decimal.Zero;

                    foreach (var item in cart.items)
                    {
                        strSanPham += "<tr>" +
                            "<td>"+item.ProductName+"</td>"+
                            "<td>" + item.Quantity + "</td>" +
                            "<td>" + WeBanHang.Common.Common.FormatNumber(item.TotalPrice,0) + "</td>" +
                            "</tr>";
                        ThanhTien += item.Price * item.Quantity;
                    }
                    TongTien = ThanhTien;
                    string contentCustomer = System.IO.File.ReadAllText(Server.MapPath("~/Content/template/send2.html"));
                    contentCustomer = contentCustomer.Replace("{{MaDon}}",order.Code);
                    contentCustomer = contentCustomer.Replace("{{SanPham}}", strSanPham);
                    contentCustomer = contentCustomer.Replace("{{TenKhachHang}}", order.CustomerName);
                    contentCustomer = contentCustomer.Replace("{{DiaChiNhanHang}}", order.Address);
                    contentCustomer = contentCustomer.Replace("{{Email}}", order.Email);
                    contentCustomer = contentCustomer.Replace("{{Phone}}", order.Phone);
                    contentCustomer = contentCustomer.Replace("{{NgayDatHang}}", order.CreatedDate.ToString());
                    contentCustomer = contentCustomer.Replace("{{ThanhTien}}", WeBanHang.Common.Common.FormatNumber(ThanhTien, 0));
                    contentCustomer = contentCustomer.Replace("{{TongTien}}", WeBanHang.Common.Common.FormatNumber(TongTien, 0));
                    WeBanHang.Common.Common.SendMail("ShopOnline","Đơn hàng #"+order.Code,contentCustomer,order.Email);
                    cart.ClearShoppingCart();
                    return RedirectToAction("CheckoutSuccess");
                }
            }
            return View("Checkout", order);
        }

        public ActionResult OrderConfirmation() 
        {
            //ShoppingCart cart = (ShoppingCart)Session["Cart"];
            //if (cart != null)
            //{
            //    return PartialView(cart.items);
            //}
            return PartialView();
        }

        public ActionResult Partial_Checkout_Items()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                return PartialView(cart.items);
            }
            return PartialView();
        }

        public ActionResult Partial_Item_Cart()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                return PartialView(cart.items);
            }
            return PartialView();
        }

        public ActionResult ShowCheckoutItems()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart!=null)
            {
                return Json(new {success= true, count = cart.items.Count },JsonRequestBehavior.AllowGet);

            }
            return Json(new {success= false, count = 0 }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AddToCart(int id, int quantity)
        {
            var code = new { success = false, msg = "", code = -1,count=0};
            var checkProduct = db.Products.FirstOrDefault(p => p.Id == id);
            if (checkProduct != null)
            {
                ShoppingCart cart = (ShoppingCart)Session["Cart"];

                if (cart == null)
                {
                    cart = new ShoppingCart();

                } 
                
                ShoppingCartItem item = new ShoppingCartItem()
                {
                    ProductId= id,
                    ProductName = checkProduct.Title,
                    Alias = checkProduct.Alias,
                    CategoryName = checkProduct.ProductCategory.Title,
                    Quantity = quantity
                };
                if (checkProduct.ProductImages.FirstOrDefault(x=>x.IsDefault) != null)
                {
                    item.ProductImg = checkProduct.ProductImages.FirstOrDefault(y=>y.IsDefault).Image;
                }
                if (checkProduct.PriceSale > 0)
                {
                    item.Price = checkProduct.PriceSale;
                } else
                {
                    item.Price = checkProduct.Price;
                }
                item.TotalPrice = item.Price * item.Quantity;
                cart.AddToCart(item, quantity);
                Session["Cart"] = cart;
                code = new { success = true, msg = "Thêm sản phẩm vào giỏ hàng thành công", code = 1,count=cart.items.Count};
            }
            return Json(code);
        }

       

        [HttpPost]
        public ActionResult RemoveItem(int id)
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null) 
            {
                var checkProduct = db.Products.FirstOrDefault(p => p.Id == id);
                if (checkProduct != null)
                {
                    cart.RemoveItem(id);
                    return Json(new { success = true });
                }
            }
            return Json(new { success = false });

        }
        [HttpPost]
        public ActionResult RemoveAll()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                cart.ClearShoppingCart();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        [HttpPost]
        public ActionResult UpdateProductQuantity(int id,int quantity)
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                cart.UpdateQuantityOfItem(id,quantity);
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}