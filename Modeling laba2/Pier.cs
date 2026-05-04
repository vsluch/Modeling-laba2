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
        public void TakeTanker(Tanker _tanker)
        {
            if(_tanker == null) { return; }

            CurrentTanker = _tanker;
            Occupation = PierOccupationType.Busy;

            if (CurrentTanker.RemainingPreventionTime > 0)
            {
                CurrentTanker.WaitingType = TankerWaitingType.Prevention;
            }
            else if (CurrentTanker.RemainingLoadingTime > 0)
            {
                CurrentTanker.WaitingType = TankerWaitingType.Loading;
            }
            else
            {
                Tanker temp = DriveTankerAway();
                Occupation = PierOccupationType.Free;
            }        
        }


        public Tanker UpdateState()
        {
            if (CurrentTanker != null)
            {
                CurrentTanker.UpdateState();

                if (CurrentTanker.WaitingType == TankerWaitingType.Prevention && CurrentTanker.RemainingPreventionTime < 1)
                {
                    CurrentTanker.StartLoading();
                }
                if (CurrentTanker.WaitingType == TankerWaitingType.Loading && CurrentTanker.RemainingLoadingTime < 1)
                {
                    return DriveTankerAway();
                }
            }
            return null;
        }


        // отогнать танкер от причала
        public Tanker DriveTankerAway()
        {
            if(CurrentTanker == null) { return null; }

            if (CurrentTanker.RemainingLoadingTime < 1)
            {
                CurrentTanker.WaitingType = TankerWaitingType.WentAway;
            }
            else
            {
                CurrentTanker.WaitingType = TankerWaitingType.InLine;
            }
            Tanker temp = CurrentTanker;
            CurrentTanker = null;
            Occupation = PierOccupationType.Free;
            return temp;
        }



    }
}
