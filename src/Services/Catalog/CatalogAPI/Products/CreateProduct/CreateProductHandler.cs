using BuildingBlocks.CQRS;
using CatalogAPI.Models;
using Marten;

namespace CatalogAPI.Products.CreateProduct;

public record CreateProductCommand(
    string Name,
    string Description,
    List<string> Categories,
    string ImageFile,
    decimal Price
) : ICommand<CreateProductResult>;

public record CreateProductResult(Guid Id);

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Product name is required.");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Product description is required.");
        RuleFor(x => x.Categories).NotEmpty().WithMessage("At least one category is required.");
        RuleFor(x => x.ImageFile).NotEmpty().WithMessage("Image file is required.");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than zero.");
    }
}

internal class CreateProductCommandHandler(IDocumentSession session, ILogger<CreateProductCommandHandler> logger)
    : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling CreateProductCommand for product: {command}", command.Name);
        var product = new Product
        {
            Name = command.Name,
            Description = command.Description,
            Categories = command.Categories,
            ImageFile = command.ImageFile,
            Price = command.Price
        };

        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);
        return new CreateProductResult(product.Id);
    }
}