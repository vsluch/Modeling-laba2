using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modeling_laba2
{
    public class Storm
    {
        private readonly int _stormMeanTime = 150;
        public int TimeUntilStorm { get; private set; }

        public bool IsStormNow { get; private set; }
        public int StormTime {  get; private set; } // продолжительность шторма

        public Storm()
        {
            StormTime = 0;
            IsStormNow = false;
            SetTimeUntilStorm();
        }


        public void SetTimeUntilStorm()
        {
            TimeUntilStorm = DistributionLaws.GenExp(_stormMeanTime);
        }


        // ежечасная проверка, идет ли сейчас шторм
        public bool StormNow()
        {
            if (!IsStormNow)
            {
                if (TimeUntilStorm <= 0)    // начало шторма
                {
                    IsStormNow = true;
                    StormTime = DistributionLaws.GenUniformInt(20, 2);
                }
            }
            else
            {
                if (StormTime <= 0) // шторм закончился
                {
                    IsStormNow = false;
                    TimeUntilStorm = DistributionLaws.GenExp(_stormMeanTime) + 1;   // здесь 1 - особенность алгоритма
                }
            }

            ReduceCounters();
            return IsStormNow;
        }


        public void ReduceCounters()
        {
            if (TimeUntilStorm > 0)
            {
                TimeUntilStorm--;
            }
            if(StormTime > 0)
            {
                StormTime--;
            }
        }
    }
}
