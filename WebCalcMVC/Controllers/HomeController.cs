using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebCalcMVC.Models;

namespace WebCalcMVC.Controllers
{
    public class HomeController : Controller
    {

        // GET: Home
        [HttpGet]
        public ActionResult Index()
        {
            UserInput userIn = new UserInput();
            Session.Add("userInput", userIn);
            return View("Index", userIn);
        }

        [HttpPost]
        public ActionResult Index(string submitButton, string currentInput) {
            UserInput userIn = (UserInput)this.Session["userInput"];
            userIn.CurrentInput = currentInput;
            userIn.Result = Calculate(userIn);
            userIn.History += userIn.CurrentInput + submitButton;
            switch (submitButton)
            {
                case "+":
                    userIn.LastOperation = Operation.Add;
                    break;
                case "-":
                    userIn.LastOperation = Operation.Minus;
                    break;
                case "*":
                    userIn.LastOperation = Operation.Multi;
                    break;
                case "/":
                    userIn.LastOperation = Operation.Div;
                    break;
                case "C":
                    userIn.History = String.Empty;
                    userIn.LastOperation = Operation.None;
                    userIn.Result = 0.0;
                    userIn.CurrentInput = String.Empty;
                    break;
                case "=":
                    userIn.History += userIn.Result;
                    userIn.LastOperation = Operation.Result;
                    userIn.CurrentInput = String.Empty;
                    break;
            }
            return View("Index", userIn);

        }

        private double Calculate(UserInput userIn)
        {
            double result = 0.0;
            if (userIn.LastOperation == Operation.None)
            {
                result = ParseInput(userIn.CurrentInput);
            }
            else
            {
                result = userIn.Result;
                double value = ParseInput(userIn.CurrentInput);
                switch (userIn.LastOperation)
                {
                    case Operation.Add:
                        result += value;
                        break;
                    case Operation.Minus:
                        result -= value;
                        break;
                    case Operation.Multi:
                        result *= value;
                        break;
                    case Operation.Div:
                        result /= value;
                        break;
                    case Operation.Result:
                        userIn.History = userIn.Result.ToString();
                        break;
                }
            }
            return result;
        }

        private double ParseInput(String input)
        {
            double inputValue = 0.0;
            if (input != null && !input.Equals(""))
            {
                try
                {
                    inputValue = Double.Parse(input);
                }
                catch
                {
                    inputValue = 0.0;
                }
            }
            return inputValue;
        }

    }

}