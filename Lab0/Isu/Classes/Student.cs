using Isu.Services;

namespace Isu.Classes
{
    public class Student
    {
        private static int _idCount;
        public Student(string name)
        {
            Name = name;
            Id = ++_idCount;
        }

        public string Name { get; }
        public int Id { get; }
    }
}