using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modeling_laba2
{
    public enum TankerType
    {
        First,
        Second,
        Third
    }

    public enum TankerWaitingType
    {
        InLine,     // В очереди
        Prevention, // Прфилактические и ремонтные работы
        Loading,    // Загрузка нефтью
        WentAway    // Уплыл
    }


    public enum PierOccupationType
    {
        Busy,
        Free
    }
}
