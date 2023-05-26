using System;
using System.Collections.Generic;

namespace TestDA.Model;

public partial class Tovari
{
    public string Id { get; set; } = null!;

    public string? Name { get; set; }

    public int? Quantity { get; set; }

    public int? Price { get; set; }

    public byte[]? Photo { get; set; }

    public int? TypeId { get; set; }

    public DateTime? AddedDate { get; set; }

    public virtual ICollection<CorzinaOfTovari> CorzinaOfTovaris { get; set; } = new List<CorzinaOfTovari>();

    public virtual Type? Type { get; set; }
}
