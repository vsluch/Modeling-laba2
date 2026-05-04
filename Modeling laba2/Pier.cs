using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modeling_laba2
{
    public class Pier
    {
        public int Number { get; private set; }
        public PierOccupationType Occupation { get; private set; }
        public Tanker CurrentTanker { get; private set; }


        public Pier(int _number)
        {
            Number = _number;
            Occupation = PierOccupationType.Free;
            CurrentTanker = null;
        }


        // принять танкер
        public bool TakeTanker(Tanker _tanker)
        {
            if(_tanker == null || CurrentTanker != null) { return false; }

            CurrentTanker = _tanker;
            Occupation = PierOccupationType.Busy;
            
            return true;            
        }


        public void UpdateState()
        {
            CurrentTanker.UpdateState();
            if()
        }


        // отогнать танкер от причала
        public Tanker DriveTankerAway()
        {
            CurrentTanker.WaitingType = TankerWaitingType.InLine;
            Tanker temp = CurrentTanker;
            CurrentTanker = null;
            Occupation = PierOccupationType.Free;
            return temp;
        }



    }
}
