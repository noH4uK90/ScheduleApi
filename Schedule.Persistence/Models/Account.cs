using System;
using System.Collections.Generic;

namespace Schedule.Persistence.Models;

public partial class Account
{
    public int AccountId { get; set; }

    public string Login { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string? MiddleName { get; set; }

    public string Email { get; set; } = null!;

    public int RoleId { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual MiddleName? MiddleNameNavigation { get; set; }

    public virtual Name NameNavigation { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();

    public virtual Surname SurnameNavigation { get; set; } = null!;

    public virtual ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
}
