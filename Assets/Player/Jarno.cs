
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Player
{
    public class Jarno : Player
    {
        public Jarno(string name, float speed, int likability, float money, int drunkLevel, int funLevel) :
            base(name, speed, likability, money, drunkLevel, funLevel)
        {
            hour = 17;
            minute = 0;
        }

        override public string Think()
        {
            return "I am Jarno.";
        }
    }
}