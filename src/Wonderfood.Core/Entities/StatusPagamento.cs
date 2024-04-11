using Wonderfood.Core.Entities.Enums;

namespace Wonderfood.Core.Entities;

public class StatusPagamento
{
    public SituacaoPagamento Situacao { get; set; }
    public DateTime Data { get; set; }
}