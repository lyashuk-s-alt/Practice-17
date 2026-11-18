using System;

namespace SealedModifiersPractice
{
    // =========================================================================
    // ЧАСТИНА 1: ЗАГАЛЬНЕ ЗАВДАННЯ (Заборона успадкування класу - sealed class)
    // =========================================================================

    abstract class Shape
    {
        public abstract double GetArea();
    }

    // Оголошуємо клас Rectangle як sealed (закритий для успадкування)
    sealed class Rectangle : Shape
    {
        public double Width { get; set; }
        public double Height { get; set; }

        public Rectangle(double width, double height)
        {
            Width = width;
            Height = height;
        }

        public override double GetArea()
        {
            return Width * Height;
        }
    }

    /* 
     * СПРОБА ПОРУШЕННЯ ПРАВИЛА (Закоментовано, щоб програма компілювалася):
     * 
     * class Square : Rectangle
     * {
     *     public Square(double side) : base(side, side) { }
     * }
     * 
     * ТЕКСТ ПОМИЛКИ КОМПІЛЯТОРА:
     * error CS0509: 'Square': cannot derive from sealed type 'Rectangle'
     * (Переклад: 'Square' не може успадковуватися від закритого типу 'Rectangle')
     */


    // =========================================================================
    // ЧАСТИНА 2: ДОМАШНЄ ЗАВДАННЯ (Заборона перевизначення методу в ланцюжку)
    // =========================================================================

    // Клас 1 (Базовий): оголошує віртуальні методи
    class BaseDevice
    {
        public virtual void Start()
        {
            Console.WriteLine("1. BaseDevice: Базовий запуск пристрою.");
        }

        public virtual void Diagnostics()
        {
            Console.WriteLine("1. BaseDevice: Стандартна діагностика.");
        }
    }

    // Клас 2 (Проміжний): перевизначає обидва методи, але один із них закриває (sealed)
    class SmartDevice : BaseDevice
    {
        // Забороняємо подальше перевизначення методу Start у класах-нащадках
        public sealed override void Start()
        {
            Console.WriteLine("2. SmartDevice: Захищений запуск системи (sealed override).");
        }

        // Цей метод залишається відкритим для подальшого перевизначення
        public override void Diagnostics()
        {
            Console.WriteLine("2. SmartDevice: Розширена смарт-діагностика.");
        }
    }

    // Клас 3 (Кінцевий нащадок у ланцюжку)
    class Smartphone : SmartDevice
    {
        /*
         * СПРОБА ПОРУШЕННЯ ПРАВИЛА (Закоментовано для коректної роботи):
         * 
         * public override void Start()
         * {
         *     Console.WriteLine("3. Smartphone: Спроба змінити захищений запуск.");
         * }
         * 
         * ТЕКСТ ПОМИЛКИ КОМПІЛЯТОРА:
         * error CS0239: 'Smartphone.Start()': cannot override inherited member 'SmartDevice.Start()' because it is sealed
         * (Переклад: неможливо перевизначити успадкований член, оскільки він позначений як sealed)
         */

        // Метод Diagnostics перевизначити можна, бо він не має модифікатора sealed
        public override void Diagnostics()
        {
            Console.WriteLine("3. Smartphone: Діагностика модулів зв'язку та екрана.");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("=== ЗАГАЛЬНЕ ЗАВДАННЯ: Робота із sealed-класом Rectangle ===");
            Rectangle rect = new Rectangle(5, 10);
            Console.WriteLine("Площа прямокутника (5x10): " + rect.GetArea());
            Console.WriteLine("Спроба успадкувати клас Square від Rectangle викликає помилку CS0509.\n");

            Console.WriteLine("=== ДОМАШНЄ ЗАВДАННЯ: Ланцюжок з трьох класів та sealed override ===");
            Smartphone phone = new Smartphone();
            
            // Викличеться версія з проміжного класу SmartDevice, оскільки далі вона заблокована
            phone.Start();
            
            // Викличеться версія з класу Smartphone, оскільки метод не був закритий
            phone.Diagnostics();
        }
    }
}
