using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace TestApp.Models
{
    public class TestContext : DbContext
    {
        public TestContext(DbContextOptions<TestContext> options)
            : base(options)
        { }

        public DbSet<Student> Students { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Entry> Entries { get; set; }
    }

    public class Student
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public float Average_score { get; set; }
        public DateTime Birthday { get; set; }
        public int Exp { get; set; }
    }

    public class Group
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public int Number { get; set; }
        public int Capacity { get; set; }
        public int Maxcap { get; set; }
       
    }

    public class Entry
    {
        public long Id { get; set; }
        public long GroupId { get; set; }
        public long StudentId { get; set; }
    }

}
