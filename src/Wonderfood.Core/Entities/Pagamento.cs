using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Wonderfood.Core.Entities.Enums;

namespace Wonderfood.Core.Entities;

public class Pagamento
 {
     [BsonId]
     [BsonRepresentation(BsonType.ObjectId)]
     public string Id { get; set; }
     public Guid IdPedido { get; set; }
     public Guid IdCliente { get; set; }
     public decimal ValorTotal { get; set; }
     public DateTime DataConfirmacaoPedido { get; set; }
     public FormaPagamento FormaPagamento { get; set; }
     public List<StatusPagamento> HistoricoStatus { get; set; }
 }

