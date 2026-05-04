using Modeling_laba2;

public class Port
{
    public readonly int _hoursInYear = 8760;

    public Storm StormGen { get; private set; }
    public List<Pier> Piers { get; private set; }
    public ShipGenerator ShipGen { get; private set; }
    public List<Tanker> Line { get; private set; }

    // Статистика
    public int ServedFirst { get; private set; }
    public int ServedSecond { get; private set; }
    public int ServedThird { get; private set; }
    public long TotalWaitingTimeFirst { get; private set; }
    public long TotalWaitingTimeSecond { get; private set; }
    public long TotalWaitingTimeThird { get; private set; }

    public Port()
    {
        StormGen = new Storm();
        Piers = new List<Pier>();
        Piers.Add(new Pier(1));
        Piers.Add(new Pier(2));
        Piers.Add(new Pier(3));

        ShipGen = new ShipGenerator();
        Line = new List<Tanker>();

        // Инициализация статистики
        ServedFirst = 0;
        ServedSecond = 0;
        ServedThird = 0;
        TotalWaitingTimeFirst = 0;
        TotalWaitingTimeSecond = 0;
        TotalWaitingTimeThird = 0;
    }

    public void Hour()
    {
        foreach (Tanker t in Line)
        {
            t.UpdateState();
        }

        bool isStorm = StormGen.StormNow();


        if (!isStorm)
        {
            Tanker newTanker = ShipGen.TankerMaybeArrives();
            if (newTanker != null)
            {
                Line.Add(newTanker);
            }
        }

        
        List<Tanker> toRemove = new List<Tanker>();
        List<Tanker> toAdd = new List<Tanker>();

        foreach (Tanker t in Line)  // сначала третий тип
        {
            if (t.Type == TankerType.Third)
            {
                bool foundFree = false;
                foreach (Pier pier in Piers)
                {
                    if (pier.Occupation == PierOccupationType.Free)
                    {
                        pier.TakeTanker(t);
                        toRemove.Add(t);
                        foundFree = true;
                        break;
                    }
                }
                if (!foundFree)
                {
                    foreach (Pier pier in Piers)
                    {
                        if (pier.CurrentTanker?.Type != TankerType.Third)
                        {
                            Tanker oldTanker = pier.DriveTankerAway();
                            pier.TakeTanker(t);
                            toRemove.Add(t);
                            toAdd.Add(oldTanker);
                            break;
                        }
                    }
                }
            }
        }

        foreach (Tanker t in toRemove) Line.Remove(t);
        foreach (Tanker t in toAdd) Line.Add(t);
        toRemove.Clear();
        toAdd.Clear();

        // Очередь для первого и второго типов
        foreach (Tanker t in Line)
        {
            if (t.Type == TankerType.First)
            {
                foreach (Pier pier in Piers)
                {
                    if ((pier.Number == 1 || pier.Number == 2) && pier.Occupation == PierOccupationType.Free)
                    {
                        pier.TakeTanker(t);
                        toRemove.Add(t);
                        break;
                    }
                }
            }
            else if (t.Type == TankerType.Second)
            {
                foreach (Pier pier in Piers)
                {
                    if (pier.Number == 3 && pier.Occupation == PierOccupationType.Free)
                    {
                        pier.TakeTanker(t);
                        toRemove.Add(t);
                        break;
                    }
                }
            }
        }

        foreach (Tanker t in toRemove) Line.Remove(t);

        // Обновление состояния причалов и сбор статистики по ушедшим танкерам
        for (int i = 0; i < Piers.Count; i++)
        {
            Pier pier = Piers[i];
            Tanker departed = pier.UpdateState();
            if (departed != null)
            {
                switch (departed.Type)
                {
                    case TankerType.First:
                        ServedFirst++;
                        TotalWaitingTimeFirst += departed.WaitingTime;
                        break;
                    case TankerType.Second:
                        ServedSecond++;
                        TotalWaitingTimeSecond += departed.WaitingTime;
                        break;
                    case TankerType.Third:
                        ServedThird++;
                        TotalWaitingTimeThird += departed.WaitingTime;
                        break;
                }
            }
        }
    }



    public void Hour2()
    {
        foreach (Tanker t in Line)
        {
            t.UpdateState();
        }

        bool isStorm = StormGen.StormNow();

        // Прибытие новых танкеров
        if (!isStorm)
        {
            Tanker newTanker = ShipGen.TankerMaybeArrives();
            if (newTanker != null)
            {
                Line.Add(newTanker);
            }
        }

        List<Tanker> toRemove = new List<Tanker>();
        List<Tanker> toAdd = new List<Tanker>();

        // Обработка третьего типа: сначала свободные причалы,
        // затем, если ожидание >=20, вытеснение
        foreach (Tanker t in Line)
        {
            if (t.Type == TankerType.Third)
            {
                bool foundFree = false;
                foreach (Pier pier in Piers)
                {
                    if (pier.Occupation == PierOccupationType.Free)
                    {
                        pier.TakeTanker(t);
                        toRemove.Add(t);
                        foundFree = true;
                        break;
                    }
                }
                if (!foundFree && t.WaitingTime >= 20)
                {
                    foreach (Pier pier in Piers)
                    {
                        if (pier.CurrentTanker?.Type != TankerType.Third)
                        {
                            Tanker oldTanker = pier.DriveTankerAway();
                            pier.TakeTanker(t);
                            toRemove.Add(t);
                            toAdd.Add(oldTanker);
                            break;
                        }
                    }
                }
            }
        }


        foreach (Tanker t in toRemove) Line.Remove(t);
        foreach (Tanker t in toAdd) Line.Add(t);
        toRemove.Clear();
        toAdd.Clear();


        // Обработка первого и второго типов
        foreach (Tanker t in Line)
        {
            if (t.Type == TankerType.First)
            {
                foreach (Pier pier in Piers)
                {
                    if ((pier.Number == 1 || pier.Number == 2) && pier.Occupation == PierOccupationType.Free)
                    {
                        pier.TakeTanker(t);
                        toRemove.Add(t);
                        break;
                    }
                }
            }
            else if (t.Type == TankerType.Second)
            {
                foreach (Pier pier in Piers)
                {
                    if (pier.Number == 3 && pier.Occupation == PierOccupationType.Free)
                    {
                        pier.TakeTanker(t);
                        toRemove.Add(t);
                        break;
                    }
                }
            }
        }

        foreach (Tanker t in toRemove) Line.Remove(t);

        // Обновление причалов и сбор статистики
        for (int i = 0; i < Piers.Count; i++)
        {
            Pier pier = Piers[i];
            Tanker departed = pier.UpdateState();
            if (departed != null)
            {
                switch (departed.Type)
                {
                    case TankerType.First:
                        ServedFirst++;
                        TotalWaitingTimeFirst += departed.WaitingTime;
                        break;
                    case TankerType.Second:
                        ServedSecond++;
                        TotalWaitingTimeSecond += departed.WaitingTime;
                        break;
                    case TankerType.Third:
                        ServedThird++;
                        TotalWaitingTimeThird += departed.WaitingTime;
                        break;
                }
            }
        }
    }



    // вывод статистики
    public void PrintReport()
    {
        Console.WriteLine($"\n\nПромоделировано часов: {_hoursInYear}");

        Console.WriteLine(" Обслуженные танкеры:");
        Console.WriteLine($"Первый тип: {ServedFirst} шт.");
        Console.WriteLine($"Второй тип: {ServedSecond} шт.");
        Console.WriteLine($"Третий тип: {ServedThird} шт.");
        Console.WriteLine($"Всего: {ServedFirst + ServedSecond + ServedThird} шт.");

        Console.WriteLine("\nСреднее время ожидания в очереди (часов):");
        if (ServedFirst > 0)
            Console.WriteLine($"Первый тип: {(double)TotalWaitingTimeFirst / ServedFirst:F2}");
        else
            Console.WriteLine($"Первый тип: нет данных");
        if (ServedSecond > 0)
            Console.WriteLine($"Второй тип: {(double)TotalWaitingTimeSecond / ServedSecond:F2}");
        else
            Console.WriteLine($"Второй тип: нет данных");
        if (ServedThird > 0)
            Console.WriteLine($"Третий тип: {(double)TotalWaitingTimeThird / ServedThird:F2}");
        else
            Console.WriteLine($"Третий тип: нет данных");
    }
}