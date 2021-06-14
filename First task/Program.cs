using System;

namespace First_task
{
    class Program
    {
        private static Catalog main = new Catalog();
        private static Catalog current = main;
        private static string path = "";
        private static string command = "";
        private static string part = "";
        private static bool isError = false;
        static void NextCatalog(string name)
        {
            current = current.FindCatalog(name); //Найти каталог по имени
            if (current == null) PreviosCatalog();
        }

        static void PreviosCatalog()
        {
            current = main;
            for (int i = 0, j = 1; j < path.Length; i++)
                if ((i + 1 >= path.Length) || (path[i + 1] == '\x1'))
                {
                    NextCatalog(path.Substring(j, i - j + 1));
                    j = i + 2;
                }
        }
        /// <summary>
        /// Отделяет параметр от строки
        /// </summary>
        /// <param name="parametr">куда сохранить параметр</param>
        /// <param name="str">Исходная строка</param>
        static void GetParametrFromString(ref string parametr, ref string str)
        {
            while ((str != "") && (str[0] != ' '))
            {
                parametr += str[0];
                str = str.Remove(0, 1);
            }
        }
        /// <summary>
        /// Считывание параметров
        /// </summary>
        /// <param name="data">массив для записи</param>
        /// <param name="number">число параметров</param>
        static bool ReadParameters(string[] data, int number)
        {
            if (number == 1)
            {
                if ((command == "") || (command[0] == ' ')) //Проверка, есть ли параметр
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Missing parameter name");
                    Console.ResetColor();
                    return false;
                }
                GetParametrFromString(ref data[0], ref command); //Считывание параметра до первого пробела
            }
            else if (number == 2)
            {
                if ((command == "") || (command[0] == ' ')) //Проверка, есть ли параметр
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Missing parameters name, count");
                    Console.ResetColor();
                    return false;
                }
                GetParametrFromString(ref data[0], ref command); //Считывание параметра до первого пробела
                if (command != "") command = command.Remove(0, 1); //удаление пробела

                if (command == "") //Проверка, есть ли параметр
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Missing parameters count");
                    Console.ResetColor();
                    return false;
                }
                while ((command != "") && (command[0] != ' ')) //Проверка параметра на корректность
                {
                    if ((command[0] < '0') || (command[0] > '9'))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Only numeric values can be assigned to count");
                        Console.ResetColor();
                        return false;
                    }
                    data[1] += command[0];
                    command = command.Remove(0, 1);
                }

                if ((command != "") && (command[0] != ' ')) return false;
            }
            else if (number == 3)
            {
                if ((command == "") || (command[0] == ' ')) //Проверка всех параметров
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Missing parameters name, count, cost");
                    Console.ResetColor();
                    return false;
                }
                GetParametrFromString(ref data[0], ref command); //Считывание параметра до первого пробела
                if (command != "") command = command.Remove(0, 1);

                if (command == "")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Missing parameters count, cost");
                    Console.ResetColor();
                    return false;
                }
                while ((command != "") && (command[0] != ' '))
                {
                    if ((command[0] < '0') || (command[0] > '9'))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Only numeric values can be assigned to count");
                        Console.ResetColor();
                        return false;
                    }
                    data[1] += command[0];
                    command = command.Remove(0, 1);
                }
                if (command == "")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Missing parameter cost");
                    Console.ResetColor();
                    return false;
                }
                else if (command[0] == ' ') command = command.Remove(0, 1);
                else return false;

                while ((command != "") && (command[0] != ' '))
                {
                    if ((command[0] < '0') || (command[0] > '9'))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Only numeric values can be assigned to cost");
                        Console.ResetColor();
                        return false;
                    }
                    data[2] += command[0];
                    command = command.Remove(0, 1);
                }
                if ((command != "") && (command[0] != ' ')) return false;
            }
            else return false;
            return true;
        }
        /// <summary>
        /// Справка по данному модулю
        /// </summary>
        static void ShowHelp()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\nacat - добавить каталог name\n" +
                "ag - добавить товар name count price\n" +
                "chct - изменить количество товаров name new_count\n" +
                "dcat - удалить каталог name\n" +
                "dg - удалить товар name\n" +
                "open - перейти в каталог name\n" +
                "back - вернуться вверх по иерархии\n" +
                "sall - вывести на экран все каталоги с информацией о количестве товаров и их общей стоимости\n" + 
                "exit - выход\n" +
                "При вводе названий, избегайте пробелов, например, используйте нижнее подчеркивание '_'");
            Console.ResetColor();
        }
        static void Main(string[] args)
        {
            string[] data = new string[3]; //массив данных: название - кол-во - стоимость
            while (part != "exit")
            {
                if (part != "sall")
                {
                    if (part != "help")
                        Console.Clear();
                    current.PrintCatalogContent(); //вывести содержимое каталога
                }
                Console.Write(">"); //Приглашение
                command = Console.ReadLine();
                part = ""; //Часть от введенной строки, отвечающая за команду
                GetParametrFromString(ref part, ref command); //Считывание параметра до первого пробела
                if (command != "") command = command.Remove(0, 1); //удалить пробел

                data[0] = data[1] = data[2] = ""; //заполнение данных пробелами
                if (part == "acat") //Добавить каталог
                    if (ReadParameters(data, 1)) current.AddCatalog((new Catalog(data[0])));
                    else isError = true;
                else if (part == "ag") //Добавить товар
                    if (ReadParameters(data, 3)) current.AddGoods(new Goods(data));
                    else isError = true;
                else if (part == "chct") //Изменить кол-во
                    if (ReadParameters(data, 2)) current.ChangeGoods(data[0], int.Parse(data[1]));
                    else isError = true;
                else if (part == "dcat") //Удалить каталог
                    if (ReadParameters(data, 1)) current.DeleteCatalog(data[0]);
                    else isError = true;
                else if (part == "dg") //Удалить товар
                    if (ReadParameters(data, 1)) current.DeleteGoods(data[0]);
                    else isError = true;
                else if (part == "open") //Открыть каталог
                    if (ReadParameters(data, 1))
                    {
                        NextCatalog(data[0]);
                        path += '\x1' + data[0];
                    }
                    else isError = true;
                else if (part == "back")
                    if (path.Length == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("This is root");
                        Console.ResetColor();
                        isError = true;
                    }
                    else
                    {
                        path = path.Remove(path.LastIndexOf('\x1'));
                        PreviosCatalog();
                    }
                else if (part == "sall")
                {
                    main.PrintAllCatalogsContent();
                }
                else if (part == "help")
                {
                    ShowHelp();
                }
                else if (part != "exit")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Unknown command. You can see help."); //Не распознана команда
                    isError = true;
                    Console.ResetColor();
                }

                if (isError)
                {
                    Console.WriteLine("\nPress any key to continue");
                    Console.ReadKey();
                    isError = false;
                }
            }
        }
    }
}