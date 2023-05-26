using System;
using System.Collections.Generic;

namespace TestDA.Model;

public partial class Type
{
    public int IdType { get; set; }

    public string? NameOfType { get; set; }

    public virtual ICollection<Tovari> Tovaris { get; set; } = new List<Tovari>();
}
