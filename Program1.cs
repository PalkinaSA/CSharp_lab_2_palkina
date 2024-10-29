using System;

public class LineSegment
{
    // Поля
    private double x;   // Координата начала отрезка на координатной прямой
    private double y;   // Координата конца отрезка на координатной прямой

    // Свойства
    public double X {
        get { return x; }
    }
    public double Y {
        get { return y; }
    }

    // Конструкторы
    public LineSegment(double x, double y)
    {
        this.x = x;
        this.y = y;
    }

    // Метод для нахождения пересечения двух отрезков
    public static LineSegment Intersect(LineSegment segment1, LineSegment segment2)
    {
        // Сортируем по возрастанию координаты
        double start1 = Math.Min(segment1.X, segment1.Y);
        double end1 = Math.Max(segment1.X, segment1.Y);
        double start2 = Math.Min(segment2.X, segment2.Y);
        double end2 = Math.Max(segment2.X, segment2.Y);

        // Определяем координаты пересечения
        double intersectStart = Math.Max(start1, start2); // Начало пересекающегося отрезка
        double intersectEnd = Math.Min(end1, end2);       // Конец пересекающегося отрезка

        // Если пересечение существует
        if (intersectStart <= intersectEnd)
        {
            return new LineSegment(intersectStart, intersectEnd); // Возвращаем пересекающийся отрезок
        }

        // Если пересечения нет
        return null;
    }

    // Перегрузка операций
    // Унарная операция "!" — установка одной из координат в 0
    public static LineSegment operator !(LineSegment segment)
    {
        if (segment.X <= segment.Y)
        {
            if (segment.X <= 0) return new LineSegment(segment.X, 0);
            else return new LineSegment(0, segment.Y);
        }
        else
        {
            if (segment.Y <= 0) return new LineSegment(segment.X, 0);
            else return new LineSegment(0, segment.Y);
        }
    }

    // Приведение к int (неявное) — целая часть координаты конца отрезка (Y)
    public static implicit operator int(LineSegment segment)
    {
        return (int)Math.Floor(segment.Y);
    }

    // Приведение к double (явное) — координата начала отрезка (X)
    public static explicit operator double(LineSegment segment)
    {
        return segment.X;
    }

    // Бинарная операция + для "LineSegment + int"
    public static LineSegment operator +(LineSegment segment, int value)
    {
        double newX = segment.X + value;    // Увеличиваем X

        return (newX >= segment.Y) ? new LineSegment(segment.Y, newX) : new LineSegment(newX, segment.Y); 
    }

    // Бинарная операция + для "int + LineSegment"
    public static LineSegment operator +(int value, LineSegment segment)
    {
        double newY = segment.Y + value;    // Увеличиваем Y

        return (newY >= segment.X) ? new LineSegment(segment.X, newY) : new LineSegment(newY, segment.X); 
    }

    // Бинарная операция ">" — проверяем, включает ли левый отрезок правый
    public static bool operator >(LineSegment left, LineSegment right)
    {
        return left.X <= right.X && left.Y >= right.Y;
    }

    // Бинарная операция "<" — противоположность ">"
    public static bool operator <(LineSegment left, LineSegment right)
    {
        return !(left > right);
    }

    // Переопределение метода ToString для удобного вывода
    public override string ToString()
    {
        return $"LineSegment: [{X}, {Y}]";
    }
}

public class Program1
{
    public static void Main(string[] args)
    {
        double x1, y1, x2, y2;

        // Ввод от пользователя для первого отрезка
        Console.WriteLine("Введите координаты первого отрезка:");
        
        try
        {
            Console.Write("Начальная точка (X1): ");
            x1 = Convert.ToDouble(Console.ReadLine());
            Console.Write("Конечная точка (Y1): ");
            y1 = Convert.ToDouble(Console.ReadLine());
        } catch
        {
            Console.WriteLine("Некорректно введены числа");
            return;
        }
        
        // Ввод от пользователя для второго отрезка
        Console.WriteLine("Введите координаты второго отрезка:");
        
        try
        {
            Console.Write("Начальная точка (X2): ");
            x2 = Convert.ToDouble(Console.ReadLine());
            Console.Write("Конечная точка (Y2): ");
            y2 = Convert.ToDouble(Console.ReadLine());
        }
        catch
        {
            Console.WriteLine("Некорректно введены числа");
            return;
        }

        // Создаем отрезки
        LineSegment segment1 = new LineSegment(x1, y1);
        LineSegment segment2 = new LineSegment(x2, y2);

        // Находим пересечение
        LineSegment intersection = LineSegment.Intersect(segment1, segment2);

        // Вывод результата
        if (intersection != null)
        {
            Console.WriteLine($"Отрезки пересекаются на отрезке: [{intersection.X}, {intersection.Y}]");
        }
        else
        {
            Console.WriteLine("Отрезки не пересекаются.");
        }

        // Тестирование операций

        // Унарная операция "!"
        LineSegment modifiedSegment = !segment1;
        Console.WriteLine("Segment1 после операции ! : " + modifiedSegment.ToString());

        // Приведение типов
        int endAsInt = segment1;  // Неявное приведение к int (целая часть Y)
        Console.WriteLine("Y как int (неявное) для segment1: " + endAsInt);

        double startAsDouble = (double)segment1; // Явное приведение к double (X)
        Console.WriteLine("X как double (явное) для segment1: " + startAsDouble);

        Console.Write("Введите целое число для добавления к отрезкам: ");

        int value;
        try
        {
            value = Convert.ToInt32(Console.ReadLine());
        }
        catch 
        {
            Console.WriteLine("Некорректно введено число");
            return;
        }

        // Бинарная операция + для "LineSegment + int"
        LineSegment increasedSegment1 = segment1 + value;
        Console.WriteLine("segment1 + " + value + " = " + increasedSegment1.ToString());

        // Бинарная операция + для "int + LineSegment"
        LineSegment increasedSegment2 = value + segment2;
        Console.WriteLine(value + " + segment2 = " + increasedSegment2.ToString());

        // Операция сравнения ">"
        bool isContained = segment1 > segment2;
        Console.WriteLine("Включает ли segment1 отрезок segment2: " + isContained);

        bool isNotContained = segment2 > segment1;
        Console.WriteLine("Включает ли segment2 отрезок segment1: " + isNotContained);
    }
}
