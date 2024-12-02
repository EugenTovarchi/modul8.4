/*
 * Написать программу-загрузчик данных из бинарного формата в текст.

На вход программа получает бинарный файл, предположительно, это база данных студентов.

Свойства сущности Student:

Имя — Name (string);
Группа — Group (string);
Дата рождения — DateOfBirth (DateTime).
Средний балл — (decimal).
Ваша программа должна:

Cчитать данные о студентах из файла;   
Создать на рабочем столе директорию Students. // Создаем при запуске метода Directory newDirectory = 
Внутри раскидать всех студентов из файла по группам (каждая группа-отдельный текстовый файл), //РЕКУРСИОННО ПРИ УСЛОВИИ ? 
в файле группы студенты перечислены построчно в формате "Имя, дата рождения, средний балл".
Подсказка
Считывание типа DateTime из двоичного файла можно разбить на две части - сначала считать значение в переменную long,
а потом преобразовать ее в DateTime.
 */
namespace Task4
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Student> studentsToWrite = new List<Student>
            {
                new Student { Name = "Жульен", Group = "G1", DateOfBirth = new DateTime(2001, 10, 22), AverageScore = 3.3M },
                new Student { Name = "Боб", Group = "G1", DateOfBirth = new DateTime(1999, 5, 25), AverageScore = 4.5M},
                new Student { Name = "Лилия", Group = "F2", DateOfBirth = new DateTime(1999, 1, 11), AverageScore = 5M},
                new Student { Name = "Роза", Group = "F2", DateOfBirth = new DateTime(1989, 9, 19), AverageScore = 3.7M}
            };

            WriteStudentsToBinFile(studentsToWrite, "students.dat");

            List<Student> studentsToRead = ReadStudentsFromBinFile("students.dat");

            foreach (Student studentProp in studentsToRead)
            {
                Console.WriteLine(studentProp.Name + " " + studentProp.Group + " " + studentProp.DateOfBirth + " " + studentProp.AverageScore);
            }

            WriteStudentsToTxtFile(studentsToRead);
        }
        static void WriteStudentsToBinFile(List<Student> students, string fileName)
        {
            using FileStream fs = new FileStream(fileName, FileMode.Create);
            using BinaryWriter bw = new BinaryWriter(fs);

            foreach (Student student in students)
            {
                bw.Write(student.Name);
                bw.Write(student.Group);
                bw.Write(student.DateOfBirth.ToBinary());
                bw.Write(student.AverageScore);
            }

        }

        static List<Student> ReadStudentsFromBinFile(string fileName)
        {
            List<Student> result = new(); // поработать с result в моем методе ? 
            using FileStream fs = new FileStream(fileName, FileMode.Open);
            using StreamReader sr = new StreamReader(fs);

            Console.WriteLine(sr.ReadToEnd());

            fs.Position = 0;

            BinaryReader br = new BinaryReader(fs);

            while (fs.Position < fs.Length)
            {
                Student student = new Student();
                student.Name = br.ReadString();
                student.Group = br.ReadString();
                long dt = br.ReadInt64();
                student.DateOfBirth = DateTime.FromBinary(dt);
                student.AverageScore = br.ReadDecimal();

                result.Add(student);
            }

            return result;
        }
        public static void WriteStudentsToTxtFile(List<Student> studentsToRead) 
        {
            string folderPath = @"C:\Users\Yudjine\OneDrive\Рабочий стол\Students";
            DirectoryInfo studentsFolder = new DirectoryInfo(folderPath);

            if (!studentsFolder.Exists)
                studentsFolder.Create();

            foreach (Student student in studentsToRead) 
            {
                string filePath = Path.Combine(folderPath,$"{student.Group}.txt" );
                FileInfo file = new FileInfo(filePath);
                if (!file.Exists)
                {
                    using (StreamWriter sw = file.CreateText())
                    {
                        sw.WriteLine($"{student.Name} || {student.DateOfBirth} || {student.AverageScore} ");
                    }
                }  
                else
                {
                    using (StreamWriter sw1 = file.AppendText())
                    {
                        sw1.WriteLine($"{student.Name} || {student.DateOfBirth} || {student.AverageScore} ");
                    }
                }
            }
                
            
        }
    }
}
