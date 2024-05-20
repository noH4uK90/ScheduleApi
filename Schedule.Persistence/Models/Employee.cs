using System;
using System.Collections.Generic;

namespace Schedule.Persistence.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public int AccountId { get; set; }

    public virtual Account Account { get; set; } = null!;
}
