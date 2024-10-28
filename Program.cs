using System;

public class AlarmSystem
{
    // Поля
    private bool _isMotionSensorTriggered;
    private bool _isSoundSensorTriggered;

    // Свойства
    public bool IsMotionSensorTriggered 
    { 
        get 
        { return _isMotionSensorTriggered; } 
    }
    public bool IsSoundSensorTriggered 
    { 
        get
        { return _isSoundSensorTriggered; }
    }

    // Конструкторы
    public AlarmSystem()    // Без параметров
    {
        _isMotionSensorTriggered = false;
        _isSoundSensorTriggered = false;
    }
    public AlarmSystem(bool isMotionSensorTriggered, bool isSoundSensorTriggered)   // С двумя параметрами
    {
        _isMotionSensorTriggered = isMotionSensorTriggered;
        _isSoundSensorTriggered = isSoundSensorTriggered;
    }
    public AlarmSystem(AlarmSystem other)   // Конструктор копирования
    {
        _isMotionSensorTriggered = other.IsMotionSensorTriggered;
        _isSoundSensorTriggered = other.IsSoundSensorTriggered;
    }

    // Методы
    public bool NotDisjunction()    // Метод, вычисляющий отрицание дизъюнкции полей
    {
        return !(_isMotionSensorTriggered || _isSoundSensorTriggered);
    }

    public override string ToString()   // Перегрузка метода для вывода строки информации о состоянии сигнализации
    {
        return $"Реакция датчика движения: {_isMotionSensorTriggered}, " +
            $"Реакция датчика звука: {_isSoundSensorTriggered}";
    }
}

public class AdvancedAlarmSystem : AlarmSystem 
{
    // Поля
    private int _batteryLevel;              // уровень заряда батареи
    private bool _isConnectedToNetwork;     // состояние подключения к сети

    // Конструкторы
    public AdvancedAlarmSystem() : base()   // Без параметров
    {
        _batteryLevel = 100;
        _isConnectedToNetwork = true;
    }

    public AdvancedAlarmSystem(int batteryLevel, bool isConnectedToNetwork) : base()    // Два параметра
    {
        _batteryLevel = batteryLevel;
        _isConnectedToNetwork = isConnectedToNetwork;
    }

    public AdvancedAlarmSystem(bool isSoundSensorTriggered, bool isMotionSensorTriggered, // Все параметры есть
        int batteryLevel, bool isConnectedToNetwork)
        : base(isSoundSensorTriggered, isMotionSensorTriggered)
    {
        _batteryLevel = batteryLevel;
        _isConnectedToNetwork = isConnectedToNetwork;
    }

    // Методы
    public bool IsBatteryLevelSufficient()  // Проверка, что заряда батареи достаточно для работы
    {
        return _batteryLevel > 20; // Предположим, что ниже 20% система может не работать надежно
    }

    public string GetConnectionStatus() // Проверка состояния связи
    {
        return _isConnectedToNetwork ? "Подключен к сети" : "Подключение отсутствует";
    }

    public override string ToString()   // Вывод полного состояния системы
    {
        return base.ToString() + $", Уровень заряда батареи: {_batteryLevel}%, " +
            $"Состояние сети: {GetConnectionStatus()}";
    }
}

class Program
{
    public static void Main(string[] args)
    {
        // Тесты для класса AlarmSystem
        // 1. Тест конструктора без параметров
        AlarmSystem alarm1 = new AlarmSystem();
        Console.WriteLine("Test 1 - Конструктор без параметров для AlarmSystem:");
        Console.WriteLine(alarm1.ToString()); 
        Console.WriteLine("Отрицание дизъюнкции: " + alarm1.NotDisjunction());
        Console.WriteLine();

        // 2. Тест конструктора с параметрами
        
        Console.WriteLine("Test 2 - Конструктор с параметрами:");

        bool motionTrigger, soundTrigger;
        try
        {
            Console.Write("Включение датчика движения (true/false): ");
            motionTrigger = Convert.ToBoolean(Console.ReadLine());
            Console.Write("Включение датчика звука (true/false): ");
            soundTrigger = Convert.ToBoolean(Console.ReadLine());
        } catch
        {
            Console.WriteLine("Некорректный ввод данных");
            return;
        }
        
        AlarmSystem alarm2 = new AlarmSystem(motionTrigger, soundTrigger);

        Console.WriteLine(alarm2.ToString());
        Console.WriteLine("Отрицание дизъюнкции: " + alarm2.NotDisjunction()); 
        Console.WriteLine();

        // 3. Тест конструктора копирования
        AlarmSystem alarm3 = new AlarmSystem(alarm2);
        Console.WriteLine("Test 3 - Конструктор копирования (alarm3 = alarm2):");
        Console.WriteLine(alarm3.ToString());
        Console.WriteLine("Отрицание дизъюнкции: " + alarm3.NotDisjunction()); 
        Console.WriteLine();

        // Тесты для класса AdvancedAlarmSystem

        // 4. Тест конструктора без параметров
        AdvancedAlarmSystem advAlarm1 = new AdvancedAlarmSystem();
        Console.WriteLine("Test 4 - Конструктор без параметров для AdvancedAlarmSystem:");
        Console.WriteLine(advAlarm1.ToString());
        Console.WriteLine("Подходит ли заряд батареи для стабильной работы: " + advAlarm1.IsBatteryLevelSufficient());
        Console.WriteLine();

        // 5. Тест конструктора с параметрами для батареи и сети
        
        Console.WriteLine("Test 5 - Конструктор с зарядом батареи и состоянием сети:");

        int batteryLevel;
        bool networkConnection;

        try
        {
            Console.Write("Количество заряда (0-100): ");
            batteryLevel = Convert.ToInt32(Console.ReadLine());
            if (batteryLevel < 0 || batteryLevel > 100) throw new Exception();
            Console.Write("Подключение к сети (true/false): ");
            networkConnection = Convert.ToBoolean(Console.ReadLine());
        }
        catch
        {
            Console.WriteLine("Некорректный ввод данных");
            return;
        }
        
        AdvancedAlarmSystem advAlarm2 = new AdvancedAlarmSystem(batteryLevel, networkConnection);

        Console.WriteLine(advAlarm2.ToString());  
        Console.WriteLine("Подходит ли заряд батареи для стабильной работы: " + advAlarm2.IsBatteryLevelSufficient());
        Console.WriteLine();

        // 6. Тест конструктора со всеми параметрами
        
        Console.WriteLine("Test 6 - Конструктор со всеми параметрами:");

        try
        {
            Console.Write("Включение датчика движения (true/false): ");
            motionTrigger = Convert.ToBoolean(Console.ReadLine());
            Console.Write("Включение датчика звука (true/false): ");
            soundTrigger = Convert.ToBoolean(Console.ReadLine());
            Console.Write("Количество заряда (0-100): ");
            batteryLevel = Convert.ToInt32(Console.ReadLine());
            if (batteryLevel < 0 || batteryLevel > 100) throw new Exception();
            Console.Write("Подключение к сети (true/false): ");
            networkConnection = Convert.ToBoolean(Console.ReadLine());
        }
        catch
        {
            Console.WriteLine("Некорректный ввод данных");
            return;
        }
        
        AdvancedAlarmSystem advAlarm3 = new AdvancedAlarmSystem(motionTrigger, soundTrigger, 
            batteryLevel, networkConnection);

        Console.WriteLine(advAlarm3.ToString());  
        Console.WriteLine("Подходит ли заряд батареи для стабильной работы: " + advAlarm3.IsBatteryLevelSufficient());
        Console.WriteLine();
    }
}