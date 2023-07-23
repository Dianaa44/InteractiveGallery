
using Ardalis.Specification;

namespace InteractiveGallery.Core.CategoryAggregate.Specifications;
public class CategoryByIdSpec : Specification<Category>, ISingleResultSpecification
{
  public CategoryByIdSpec(int categoryId)
  {
    Query
        .Where(category => category.Id == categoryId);
  }
}
