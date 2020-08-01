using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Anonym.WebUI.Models;
using Anonym.Business.Abstract;
using Anonym.Entities.Dtos;
using Microsoft.AspNetCore.Identity;
using Anonym.Entities.Concrete;

namespace Anonym.WebUI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
