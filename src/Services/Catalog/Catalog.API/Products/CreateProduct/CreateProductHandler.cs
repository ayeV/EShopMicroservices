

namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(string Name,
        List<string> Category,
        string Description,
        string ImageFile,
        decimal Price) : ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);

    public class CreateCommandValidation :AbstractValidator<CreateProductCommand>
    {
        public CreateCommandValidation()
        {
            RuleFor(x=>x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Price).GreaterThan(0);
            RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required");
            RuleFor(x => x.ImageFile).NotEmpty().WithMessage("Image is required");
        }
    }
    internal class CreateProductCommandHandler(IDocumentSession session)
        : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            //Logic to create a product
            //1.Create product entity from command object
           
            var product = new Product
            {
                Name = command.Name,
                Category = command.Category,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price,
            };
            //2. Save to db

            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);

            //3 Return CreateProductResult
            return new CreateProductResult(product.Id);
        }
    }
}
