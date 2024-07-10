using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUISocket
{
    internal class RecievedData
    {
        byte sensor1;
        byte sensor2;
        //byte catg;
        int len; // how many data followes it
        //LinkedList <int> param;
        int nEncS; // soorce encoder count
        int nEncD; // Dummy encoder Count
        int nEncI; // 
        int dwellTime;
        private int totalTime;
        private int errorcode;

        RecievedData(string dtRecievd)
        {
            //param = new LinkedList <int>();
            string[] words = dtRecievd.Split(",");
            Len=words.Length;
            Sensor1 = byte.Parse(words[1]);
            Sensor2 = byte.Parse(words[2]);
            NEncI = int.Parse(words[3]);
            NEncS= int.Parse(words[4]);
            NEncD = int.Parse(words[5]);
            DwellTime = int.Parse(words[6]);
            TotalTime = int.Parse(words[7]);
            Errorcode = int.Parse(words[8]);
        }

        public byte Sensor1 { get => sensor1; set => sensor1 = value; }
        public byte Sensor2 { get => sensor2; set => sensor2 = value; }
        public int Len { get => len; set => len = value; }
        public int NEncS { get => nEncS; set => nEncS = value; }
        public int NEncD { get => nEncD; set => nEncD = value; }
        public int NEncI { get => nEncI; set => nEncI = value; }
        public int DwellTime { get => dwellTime; set => dwellTime = value; }
        public int TotalTime { get => totalTime; set => totalTime = value; }
        public int Errorcode { get => errorcode; set => errorcode = value; }
    }
}
