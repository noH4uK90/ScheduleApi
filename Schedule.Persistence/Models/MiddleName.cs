using System;
using System.Collections.Generic;

namespace Schedule.Persistence.Models;

public partial class MiddleName
{
    public string Value { get; set; } = null!;

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
}
