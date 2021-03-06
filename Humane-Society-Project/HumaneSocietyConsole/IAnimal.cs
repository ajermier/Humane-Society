﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumaneSocietyConsole
{
    interface IAnimal
    {
        int Species { get; }
        string Name { get; set; }
        string Sex { get; }
        int Age { get; set; }
        double Weight { get; set; }
        string Color { get; set; }
        double Food { get; set; }
        bool Shots { get; set; }
    }
}
