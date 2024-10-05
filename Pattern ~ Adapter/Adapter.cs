// зарядка телефона через адаптер

using System;

// Интерфейс зарядного устройства, который ожидает телефон
public interface ICharger
{
    void ChargePhone();
}

// Реализация зарядного устройства с разъемом USB-C
public class UsbCCharger : ICharger
{
    public void ChargePhone()
    {
        Console.WriteLine("Телефон заряжается через USB-C.");
    }
}

// Класс телефона, который требует зарядное устройство
public class Phone
{
    private ICharger _charger;

    // Конструктор принимает объект зарядного устройства через интерфейс ICharger
    public Phone(ICharger charger)
    {
        _charger = charger;
    }

    // Метод для зарядки телефона
    public void Charge()
    {
        _charger.ChargePhone();
    }
}

// Класс старого зарядного устройства с разъемом Micro-USB
// Этот класс не реализует интерфейс ICharger, поэтому его нужно адаптировать
public class MicroUsbCharger
{
    public void ChargeWithMicroUsb()
    {
        Console.WriteLine("Телефон заряжается через Micro-USB.");
    }
}

// Адаптер, который делает MicroUsbCharger совместимым с ICharger
public class MicroUsbToUsbCAdapter : ICharger
{
    private MicroUsbCharger _microUsbCharger;

    // Конструктор адаптера принимает объект MicroUsbCharger
    public MicroUsbToUsbCAdapter(MicroUsbCharger microUsbCharger)
    {
        _microUsbCharger = microUsbCharger;
    }

    // Метод адаптера для реализации интерфейса ICharger
    // Вызывает метод ChargeWithMicroUsb из класса MicroUsbCharger
    public void ChargePhone()
    {
        _microUsbCharger.ChargeWithMicroUsb();
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Создаем объект зарядного устройства USB-C
        ICharger usbCCharger = new UsbCCharger();
        
        // Подключаем телефон к зарядке через USB-C
        Phone phoneWithUsbC = new Phone(usbCCharger);
        Console.WriteLine("Зарядка через USB-C:");
        phoneWithUsbC.Charge();  // Вывод: Телефон заряжается через USB-C.

        // Теперь у нас есть старое зарядное устройство с Micro-USB
        MicroUsbCharger oldCharger = new MicroUsbCharger();

        // Для использования этого зарядного устройства нам нужен адаптер
        ICharger adapter = new MicroUsbToUsbCAdapter(oldCharger);
        
        // Подключаем телефон к зарядке через адаптер
        Phone phoneWithAdapter = new Phone(adapter);
        Console.WriteLine("\nЗарядка через адаптер Micro-USB -> USB-C:");
        phoneWithAdapter.Charge();  // Вывод: Телефон заряжается через Micro-USB.
    }
}
