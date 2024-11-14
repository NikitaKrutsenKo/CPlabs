using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ClassLibLab5;

namespace Lab5.Controllers
{
    public class LabsController : Controller
    {
        [Authorize]
        public IActionResult Lab1()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Lab1Result(string userInput)
        {
            try
            {
                var result = Lab1Lib.RunLab1(userInput);
                ViewBag.Result = result;
                return View("Lab1");
            }
            catch (Exception ex)
            {
                ViewBag.Result = null;
                ViewBag.Error = ex;
                return View("Lab1");
            }
        }
        [Authorize]
        public IActionResult Lab2()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Lab2Result(string userInput)
        {
            try
            {
                var result = Lab2Lib.RunLab2(userInput);
                ViewBag.Result = result;
                return View("Lab2");
            }
            catch(Exception ex) 
            {
                ViewBag.Result = null;
                ViewBag.Error = ex;
                return View("Lab2");
            }
        }
        [Authorize]
        public IActionResult Lab3()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Lab3Result(string userInput)
        {
            try
            {
                var result = Lab3Lib.RunLab3(userInput);
                ViewBag.Result = result;
                return View("Lab3");
            }
            catch(Exception ex)
            {
                ViewBag.Result = null;
                ViewBag.Error = ex;
                return View("Lab3");
            }
        }
    }
}
