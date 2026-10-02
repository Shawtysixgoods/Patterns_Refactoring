## Практическая работа: Принципы SOLID в Python
Цель работы: Научиться находить нарушения архитектурных принципов SOLID в коде на Python и проводить рефакторинг для создания гибких, расширяемых и тестируемых систем.
------------------------------
## Задание 1. Single Responsibility Principle (S)
Ниже представлен класс OrderManager, который берет на себя слишком много обязанностей на нашей «киностудии»: он считает стоимость заказа, сохраняет его в базу данных и отправляет чек на email.
## Исходный код с нарушением:
```py
class OrderManager:
    def __init__(self, item_name, quantity, price):
        self.item_name = item_name
        self.quantity = quantity
        self.price = price

    def calculate_total(self):
        return self.quantity * self.price

    def save_to_database(self):
        print(f"Подключение к БД... Заказ {self.item_name} сохранен.")

    def send_email_receipt(self):
        print(f"Отправка email... Чек за {self.item_name} отправлен покупателю.")
```
## Ваша задача:

   1. Определите, сколько зон ответственности сейчас имеет класс OrderManager.
   2. Разделите этот код на три отдельных класса, каждый из которых будет отвечать строго за одну задачу (хранение/расчет данных, работа с БД, отправка уведомлений).

------------------------------
## Задание 2. Open/Closed Principle (O)
Вам поручили расширить систему логирования на киноплощадке. Сейчас класс Logger умеет отправлять логи только в консоль. 
Если завтра потребуется писать логи в файл или отправлять в Telegram, придется переписывать метод log.
## Исходный код с нарушением:
```py
class Logger:
    def log(self, message, format_type):
        if format_type == "console":
            print(f"[Console Log]: {message}")
        elif format_type == "file":
            # Представьте, что здесь логика записи в файл
            print(f"[File Log]: Запись в файл: {message}")
        # Если появится 'telegram', придется добавлять новый elif и менять этот класс!
```
## Ваша задача:

   1. Сделайте систему открытой для расширения, но закрытой для изменения.
   2. Используйте модуль abc и создайте абстрактный класс LogStrategy с методом send(message).
   3. Реализуйте две конкретные стратегии: ConsoleLogger и FileLogger.
   4. Перепишите класс Logger так, чтобы он принимал стратегию и вызывал её, не зная деталей реализации.

------------------------------
## Задание 3. Liskov Substitution Principle (L)
В системе учета сотрудников киностудии есть базовый класс Worker. Однако при создании подкласса RobotAssistant (робота-помощника) 
возникла проблема: роботы не получают зарплату на банковскую карту, из-за чего программа падает с ошибкой.
## Исходный код с нарушением:
```py
class Worker:
    def __init__(self, name):
        self.name = name

    def pay_salary(self, card_number):
        print(f"Выплачена зарплата сотруднику {self.name} на карту {card_number}")
class HumanActor(Worker):
    pass
class RobotAssistant(Worker):
    def pay_salary(self, card_number):
        # Нарушение LSP! Роботу нельзя перевести деньги на карту, логика ломается.
        raise NotImplementedError("Роботы работают бесплатно и не имеют карт!")
def process_payroll(worker: Worker):
    # Эта функция ожидает, что подставив ЛЮБОГО Worker, код отработает без ошибок
    worker.pay_salary("4444-5555-6666-7777")
```
## Ваша задача:

   1. Объясните, почему RobotAssistant нарушает контракт базового класса Worker.
   2. Проведите рефакторинг: измените иерархию классов (например, выделите общий интерфейс или разделите оплачиваемых и неоплачиваемых работников), чтобы исключить генерацию исключений NotImplementedError при подстановке классов-наследников.

------------------------------
## Задание 4. Interface Segregation Principle (I)
Перед вами «толстый» интерфейс (протокол) SmartDevice, описывающий умную технику в павильонах киностудии. 
Из-за этого обычная умная лампочка вынуждена реализовывать функции записи видео и проигрывания музыки.
## Исходный код с нарушением:
```py
from typing import Protocol
class SmartDevice(Protocol):
    def turn_on(self): ...
    def record_video(self): ...
    def play_music(self): ...
class SuperCamera:
    def turn_on(self):
        print("Камера включена.")
    def record_video(self):
        print("Запись дубля пошла!")
    def play_music(self):
        print("Камера не умеет играть музыку, но метод реализовать пришлось...")
class LightBulb:
    def turn_on(self):
        print("Свет в павильоне загорелся.")
    def record_video(self):
        pass # Бессмысленный пустой метод
    def play_music(self):
        pass # Бессмысленный пустой метод
```
## Ваша задача:

   1. Разделите один «толстый» интерфейс SmartDevice на три маленьких и специфичных (используя typing.Protocol).
   2. Перепишите классы SuperCamera и LightBulb так, чтобы они соответствовали только тем протоколам, методы которых им действительно необходимы.

------------------------------
## Задание 5. Dependency Inversion Principle (D)
Высокоуровневый класс MovieDirector (Режиссер) жестко привязан к низкоуровневой детали — конкретной камере SonyCamera. Если камеру украдут или заменят на RedCamera, режиссер не сможет работать.
## Исходный код с нарушением:
```py
class SonyCamera:
    def capture(self):
        return "Запись видео в формате 4K на Sony"
class MovieDirector:
    def __init__(self):
        # Жесткая зависимость от конкретного класса!
        self.camera = SonyCamera()

    def film_scene(self):
        print(f"Режиссер дает команду. {self.camera.capture()}")
```
## Ваша задача:

   1. Избавьте класс MovieDirector от прямой зависимости от SonyCamera.
   2. Создайте абстракцию (например, протокол или интерфейс Camera).
   3. Примените паттерн Внедрение зависимостей (Dependency Injection): передавайте объект камеры в конструктор MovieDirector.__init__(self, camera: Camera).
   4. Добавьте класс RedCamera и покажите, что теперь MovieDirector может снимать на любую камеру без изменения своего кода.



