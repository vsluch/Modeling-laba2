using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modeling_laba2
{
    public static class DistributionLaws
    {
        private static readonly Random _random = new Random();

        public static int GenUniformInt(int target, int error)
        {
            return _random.Next(target - error, target + error + 1);
        }

        // получить результат по шансу
        public static bool CheckChange(float chance)
        {
            return Random.Shared.NextDouble() < chance;
        }
    }
}
