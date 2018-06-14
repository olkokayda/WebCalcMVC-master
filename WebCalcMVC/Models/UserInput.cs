using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebCalcMVC.Models
{
    public class UserInput
    {

        public string History { get; set; }
        public double Result { get; set; }
        public string CurrentInput { get; set; }
        public Operation LastOperation { get; set; }

        public UserInput()
        {
            History = String.Empty;
            CurrentInput = String.Empty;
            LastOperation = Operation.None;
            Result = 0.0;
        }
    }
}