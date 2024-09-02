using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Soccer.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            // Có thể thêm dữ liệu cho trang chủ ở đây
            ViewBag.WelcomeMessage = "Chào mừng đến với Trang Quản Lý Bóng Đá";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Giới thiệu về ứng dụng quản lý bóng đá.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Thông tin liên hệ.";
            return View();
        }

        public ActionResult Sidebar()
        {
            // Tạo danh sách các mục cho sidebar
            var sidebarItems = new List<SidebarItem>
            {
                new SidebarItem { Controller = "CauThu", Action = "Index", Title = "Cầu Thủ" },
                new SidebarItem { Controller = "DoiBong", Action = "Index", Title = "Đội Bóng" },
                new SidebarItem { Controller = "LichThiDau", Action = "Index", Title = "Lịch Thi Đấu" },
                new SidebarItem { Controller = "TranDau", Action = "Index", Title = "Trận Đấu" },
                new SidebarItem { Controller = "TrongTai", Action = "Index", Title = "Trọng Tài" }
            };

            return PartialView("_Sidebar", sidebarItems);
        }
    }

    public class SidebarItem
    {
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Title { get; set; }
    }
}