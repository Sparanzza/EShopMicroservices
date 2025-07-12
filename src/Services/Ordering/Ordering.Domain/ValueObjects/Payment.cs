namespace Ordering.Domain.ValueObjects;

public record Payment
{
    public string? CardNumber { get; } = null;
    public string? CardName { get; } = null;
    public string? Expiration { get; } = null;
    public string? CardExpiration { get; } = null;
    public string? PaymentMehod { get; } = null;
    public int CVV { get; } = 0;
}