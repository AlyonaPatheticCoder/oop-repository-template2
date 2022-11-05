using System;
using System.Collections.Generic;
using System.Linq;
using Isu.Classes;
using Isu.Models;
using Isu.Tools;

namespace Isu.Services
{
    public class IsuService : IIsuService
    {
        private const int MaxNumberOfStudentsInGroup = 30;
        private List<Group?> allGroups = new List<Group?>();

        public Group? AddGroup(string name)
        {
            if ((name[0] != 'M') || (name[1] != '3'))
                throw new IsuException("Invalid name");
            var gname = new GroupName(name);
            var group = new Group(gname);
            allGroups.Add(group);
            return group;
        }

        public Student AddStudent(Group? group, string name)
        {
            if (group != null && group.Students.Count >= MaxNumberOfStudentsInGroup)
                throw new IsuException("The group is full");

            var student = new Student(name);
            group?.Students.Add(student);
            return student;
        }

        public Student? GetStudent(int id)
        {
            IEnumerable<Student?> studentsOfGroup = allGroups.SelectMany(s => s?.Students.ToArray() ?? throw new IsuException("Groups not found"));
            Student? student = studentsOfGroup.FirstOrDefault(s => s != null && s.Id == id);
            if (student == null)
            {
                throw new IsuException("Student was not found");
            }

            return student;
        }

        public Student? FindStudent(string name)
        {
            IEnumerable<Student?> studentsOfGroup = allGroups.SelectMany(s => s?.Students.ToArray() ?? throw new IsuException("Groups not found"));
            Student? student = studentsOfGroup.FirstOrDefault(s => s != null && s.Name == name);
            return student;
        }

        public List<Student> FindStudents(string groupName)
        {
            Group? group = allGroups.FirstOrDefault(g => g != null && g.Name.Name == groupName);
            if (group != null) return group.Students;
            throw new IsuException("students not found");
        }

        public List<Student> FindStudents(CourseNumber courseNumber)
        {
            Group? group = allGroups.FirstOrDefault(g => g != null && g.Name.CourseNumber == courseNumber);
            if (group != null) return group.Students;
            throw new IsuException("students not found");
        }

        public Group FindGroup(string groupName)
        {
            Group? group = allGroups.FirstOrDefault(g => g != null && g.Name.Name.ToLower() == groupName.ToLower());
            if (group != null) return group;
            throw new IsuException("group not found");
        }

        public List<Group?> FindGroups(CourseNumber courseNumber)
        {
            List<Group?> res = new List<Group?>();
            foreach (Group? g in allGroups)
            {
                if (g != null && g.Name.CourseNumber == courseNumber)
                {
                    res.Add(g);
                }
            }

            return res;
        }

        public void ChangeStudentGroup(Student student, Group? newGroup)
        {
            Group group = allGroups.FirstOrDefault(g => g != null && g.Students.Contains(student)) ?? throw new IsuException("Groups not found");
            group?.Students.Remove(student);
            newGroup?.Students.Add(student);
        }
    }
}