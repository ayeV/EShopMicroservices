namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductCommand(
       Guid Id,
       string Name,
       List<string> Category,
       string Description,
       string ImageFile,
       decimal Price) :ICommand<UpdateProductResult>;

    public record UpdateProductResult(bool IsSuccess);

    public class UpdateCommandValidation : AbstractValidator<UpdateProductCommand>
    {
        public UpdateCommandValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required").Length(2,150).WithMessage("Name must be between 2 and 150 characters");
            RuleFor(x => x.Price).GreaterThan(0);
            RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required");
            RuleFor(x => x.ImageFile).NotEmpty().WithMessage("Image is required");
            RuleFor(x => x.Id).NotEmpty().WithMessage("ID is required");
        }
    }
    public class UpdateProductCommandHandler(IDocumentSession session) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var existingProduct = await session.LoadAsync<Product>(command.Id,cancellationToken);

            if (existingProduct is null)
            {
                throw new ProductNotFoundExeption(command.Id);
            }

            existingProduct.Name = command.Name;
            existingProduct.Category = command.Category;
            existingProduct.Description = command.Description;
            existingProduct.ImageFile = command.ImageFile;
            existingProduct.Price = command.Price;

             session.Update(existingProduct);
            await session.SaveChangesAsync(cancellationToken);

            return new UpdateProductResult(true);
        }
    }
}
