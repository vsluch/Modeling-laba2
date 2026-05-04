using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modeling_laba2
{
    public class ShipGenerator
    {
        private readonly float _firstTypeChance = 0.25f;
        private readonly float _secondTypeChance = 0.55f;
        private readonly float _thirdTypeChance = 0.2f;

        private readonly int _timeTarget = 27;
        private readonly int _timeError = 4;

        public int TimeUntilArrival { get; private set; }

        public ShipGenerator()
        {
            SetTimeUntilArrival();
        }


        public void SetTimeUntilArrival()
        {
            TimeUntilArrival = DistributionLaws.GenUniformInt(_timeTarget, _timeError);
        }

        // будет вызываться каждый час. Если не приплыл - то null
        public Tanker TankerMaybeArrives()
        {
            if(TimeUntilArrival != 0) 
            {
                ReduceTimeUntilArrival();
                return null; 
            }

            float f = DistributionLaws.GenUniform();
            TankerType new_tanker_type;
            if(f < _firstTypeChance)
            {
                new_tanker_type = TankerType.First;
            }
            else if(f < _firstTypeChance + _secondTypeChance)
            {
                new_tanker_type = TankerType.Second;
            }
            else
            {
                new_tanker_type = TankerType.Third;
            }

            SetTimeUntilArrival();
            return new Tanker(new_tanker_type);
        }


        public void ReduceTimeUntilArrival()
        {
            if(TimeUntilArrival > 0)
            {
                TimeUntilArrival--;
            }
        }
    }
}
