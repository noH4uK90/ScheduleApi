using System;
using System.Collections.Generic;

namespace Schedule.Persistence.Models;

public partial class Role
{
    public int RoleId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
}
