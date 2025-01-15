namespace Education.BL.Exceptions;

public class CategoryNotFoundException : Exception
{
    public CategoryNotFoundException(string message) : base(message) { }

    public CategoryNotFoundException() : base("Category not found!") { }
}
