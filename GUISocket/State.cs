using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUISocket
{
    internal class State
    {
        int MainState = 0;
        int SubState = 0;
        int SubSubState = 0;

        public int MainState1 { get => MainState; set => MainState = value; }
        public int SubState1 { get => SubState; set => SubState = value; }
        public int SubSubState1 { get => SubSubState; set => SubSubState = value; }
    }
}
