using System.Collections;
using System.Collections.Generic;
using Isu.Models;

namespace Isu.Classes
{
    public class Group
    {
        public Group(GroupName name)
        {
            Name = name;
            Students = new List<Student>();
        }

        public List<Student> Students { get; }
        public GroupName Name { get; }
    }
}