﻿using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.Areas.Admin.ViewComponents.AdminUILayoutViewComponents
{
    public class _FooterAdminUILayoutComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}