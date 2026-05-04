using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modeling_laba2
{
    public class Tanker
    {
        public TankerType Type { get; private set; }
        public TankerWaitingType WaitingType { get; set; }
        public int RemainingLoadingTime {  get; private set; }
        public int WaitingTime { get; private set; }
        public int RemainingPreventionTime { get; set; }


        private static float _probabilityTankerBreakdown = 0.1f;


        public Tanker(TankerType _type)
        {
            Type = _type;
            WaitingType = TankerWaitingType.InLine;     // по умолчанию в очереди
            WaitingTime = 0;
            CalculationRemainingTime();
        }


        public void UpdateState()
        {
            if(WaitingType == TankerWaitingType.InLine)
            {
                WaitingTime += 1;
            }
            else if(WaitingType == TankerWaitingType.Prevention)
            {
                if(RemainingPreventionTime != 0) { RemainingPreventionTime--; }
            }
            else if(WaitingType == TankerWaitingType.Loading)
            {
                if(RemainingLoadingTime != 0) { RemainingLoadingTime--; }
            }
        }


        public void StartLoading()
        {
            WaitingType = TankerWaitingType.Loading;
        }


        private void CalculationRemainingTime()
        {
            int target_hours, error_hours;
            if (Type == TankerType.First)
            {
                target_hours = 18; error_hours = 2;
            }
            else if (Type == TankerType.Second)
            {
                target_hours = 24; error_hours = 3;
            }
            else
            {
                target_hours = 40; error_hours = 7;
            }
            RemainingLoadingTime = DistributionLaws.GenUniformInt(target_hours, error_hours);

            int profilact_time = DistributionLaws.GenUniformInt(4, 2);
            bool renovation_needed = DistributionLaws.CheckChange(_probabilityTankerBreakdown);
            if (renovation_needed)
            {
                int renovation_time = DistributionLaws.GenUniformInt(6, 3);
                profilact_time += renovation_time;
            }
            RemainingPreventionTime = profilact_time;
        }


        public override string ToString()
        {
            int number = 0;
            switch (Type)
            {
                case TankerType.First:
                    number = 1;
                    break;
                case TankerType.Second:
                    number = 2;
                    break;
                case TankerType.Third:
                    number = 3;
                    break;
            }

            string waiting_type = "NoWaitingType";
            switch (WaitingType)
            {
                case TankerWaitingType.InLine:
                    waiting_type = "InLine";
                    break;
                case TankerWaitingType.Prevention:
                    waiting_type = "Prevention";
                    break;
                case TankerWaitingType.Loading:
                    waiting_type = "Loading";
                    break;
                case TankerWaitingType.WentAway:
                    waiting_type = "WentAway";
                    break;
            }

            return $"Tanker{number} {waiting_type}";
        }


        public string GetInformation()
        {
            return $"{ToString()}: ВрЗагрузки: {RemainingLoadingTime} ВрРабот: {RemainingPreventionTime} ВрОжидания: {WaitingTime}";
        }
    }
}
