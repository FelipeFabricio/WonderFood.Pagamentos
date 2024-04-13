using MassTransit;

namespace Wonderfood.Worker.Interfaces;

/// <summary>
/// Possibilita o uso de mais de um tipo de bus nas aplicação:
/// Referência: https://masstransit.io/documentation/configuration/multibus
/// </summary>
public interface IPublisher : IBus
{
}