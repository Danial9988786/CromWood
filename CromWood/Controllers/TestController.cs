﻿using CromWood.Business.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CromWood.Controllers
{
    public class TestController : Controller
    {
        private readonly ITestService testService;
        public TestController(ITestService service)
        {
            testService = service;
        }
        public async Task<IActionResult> Index()
        {
            var tests = await testService.GetTests();
            return View(tests);
        }

        [Authorize]
        public IActionResult Auth()
        {
            return View();
        }
    }
}