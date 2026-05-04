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
        Loading,    // Загрузка нефтью
        Prevention, // Прфилактические и ремонтные работы
        InLine,     // В очереди
        WentAway    // Уплыл
    }


    public enum PierOccupationType
    {
        Busy,
        Free
    }
}
