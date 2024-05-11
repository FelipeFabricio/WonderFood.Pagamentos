using Wonderfood.Core.Entities.Enums;

namespace Wonderfood.Core.Entities;

public class StatusPagamento
{
    public Enums.StatusPagamento Status { get; set; }
    public DateTime Data { get; set; }
}