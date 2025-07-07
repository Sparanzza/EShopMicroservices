using BuildingBlocks.CQRS;
using CatalogAPI.Models;
using CatalogAPI.Products.Exceptions;
using Marten;

namespace CatalogAPI.Products.UpdateProduct;

public record UpdateProductCommand(
    Guid Id,
    string Name,
    string Description,
    List<string> Categories,
    string ImageFile,
    decimal Price
) : ICommand<UpdateProductResult>;

public record UpdateProductResult(bool IsSuccess);

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Product ID is required.");
        RuleFor(x => x.Name).NotEmpty().WithMessage("Product name is required.");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Product description is required.");
        RuleFor(x => x.Categories).NotEmpty().WithMessage("At least one category is required.");
        RuleFor(x => x.ImageFile).NotEmpty().WithMessage("Image file is required.");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than zero.");
    }
}

public class UpdateProductQueryHandler(IDocumentSession session, ILogger<UpdateProductQueryHandler> logger)
    : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetProducts handler called {@Command}", command);
        var product = await session.LoadAsync<Product>(command.Id, cancellationToken);
        if (product == null)
        {
            throw new ProductNotFoundException(command.Id);
        }

        product.Name = command.Name;
        product.Description = command.Description;
        product.Categories = command.Categories;
        product.ImageFile = command.ImageFile;
        product.Price = command.Price;

        session.Update(product);
        await session.SaveChangesAsync(cancellationToken);
        
        return new UpdateProductResult(true);
    }
}