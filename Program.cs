using System;

namespace TriangleApp
{
    // Допоміжний клас (або структура) для точки. 
    // Інкапсуляція: властивості доступні тільки для читання (immutable).
    public class Point
    {
        public double X { get; }
        public double Y { get; }

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        public override string ToString() => $"({X}; {Y})";
    }

    public class Triangle
    {
        // 5. Інкапсуляція: приватні поля, недоступні ззовні.
        private readonly Point _vertexA;
        private readonly Point _vertexB;
        private readonly Point _vertexC;

        // Конструктор для ініціалізації об'єкта
        public Triangle(Point a, Point b, Point c)
        {
            _vertexA = a;
            _vertexB = b;
            _vertexC = c;
        }

        // Властивість для отримання площі.
        // Логіка розрахунку прихована всередині класу.
        public double Area
        {
            get
            {
                // Використовуємо формулу площі за координатами вершин (метод шнурків / Shoelace formula)
                // S = 0.5 * |x1(y2 - y3) + x2(y3 - y1) + x3(y1 - y2)|
                double area = 0.5 * Math.Abs(
                    _vertexA.X * (_vertexB.Y - _vertexC.Y) +
                    _vertexB.X * (_vertexC.Y - _vertexA.Y) +
                    _vertexC.X * (_vertexA.Y - _vertexB.Y)
                );
                return area;
            }
        }

        // Перевизначення методу для зручного виводу інформації про об'єкт
        public override string ToString()
        {
            return $"Трикутник [A{_vertexA}, B{_vertexB}, C{_vertexC}]";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Налаштування кодування для коректного відображення кирилиці
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.Write("Введіть кількість трикутників (n): ");
            if (!int.TryParse(Console.ReadLine(), out int n) || n <= 0)
            {
                Console.WriteLine("Помилка: введіть коректне ціле число більше 0.");
                return;
            }

            // 2. Створити масив з n об'єктів
            Triangle[] triangles = new Triangle[n];

            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"\n--- Введення даних для трикутника #{i + 1} ---");
                Point p1 = GetPointFromUser("A");
                Point p2 = GetPointFromUser("B");
                Point p3 = GetPointFromUser("C");

                triangles[i] = new Triangle(p1, p2, p3);
            }

            Console.WriteLine("\n--- Результати ---");

            Triangle largestTriangle = triangles[0];

            // 2. Визначити площу кожного трикутника
            foreach (var triangle in triangles)
            {
                double area = triangle.Area;
                Console.WriteLine($"{triangle} -> Площа: {area:F2}");

                // Пошук найбільшого
                if (area > largestTriangle.Area)
                {
                    largestTriangle = triangle;
                }
            }

            Console.WriteLine($"\nНайбільший за площею: {largestTriangle}");
            Console.WriteLine($"Його площа: {largestTriangle.Area:F2}");

            Console.ReadKey();
        }

        // Допоміжний метод для зчитування координат (Code Reuse)
        static Point GetPointFromUser(string vertexName)
        {
            Console.Write($"Введіть X для вершини {vertexName}: ");
            double x = Convert.ToDouble(Console.ReadLine());

            Console.Write($"Введіть Y для вершини {vertexName}: ");
            double y = Convert.ToDouble(Console.ReadLine());

            return new Point(x, y);
        }
    }
}
