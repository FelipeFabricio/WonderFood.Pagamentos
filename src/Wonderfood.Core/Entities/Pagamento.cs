using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Wonderfood.Core.Entities.Enums;

namespace Wonderfood.Core.Entities;

public class Pagamento
 {
     [BsonId]
     [BsonRepresentation(BsonType.ObjectId)]
     public Guid Id { get; set; }
     public int NumeroPedido { get; set; }
     public string CpfCliente { get; set; }
     public decimal ValorTotal { get; set; }
     public DateTime DataPagamento { get; set; }
     public FormaPagamento FormaPagamento { get; set; }
     public List<StatusPagamento> HistoricoStatus { get; set; }
 }

