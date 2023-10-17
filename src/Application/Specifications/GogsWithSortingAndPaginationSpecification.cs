using Domain.Entities;

namespace Application.Specifications;

public class GogsWithSortingAndPaginationSpecification : BaseSpecification<Dog>
{
    public GogsWithSortingAndPaginationSpecification(SpecParams specParams) 
    {
        if (specParams.PageSize != null)
        {
            ApplyPaging((int)(specParams.PageSize * (specParams.PageNumber - 1)), (int)specParams.PageSize);
        }
        if (!(string.IsNullOrEmpty(specParams.Attribute)))
        {
            switch (specParams.Attribute)
            {
                case "weight":
                    AddAttribute(d => d.Weight);
                    break;
                case "tail_length":
                    AddAttribute(d => d.TailLength);
                    break;
                default:
                    AddAttribute(d => d.Name);
                    break;
            }
        }
        if (!(string.IsNullOrEmpty(specParams.Attribute)))
        {
            switch (specParams.Order)
            {
                case "desc":
                    AddOrderByDescending(Attribute!);
                    break;
                default:
                    AddOrderBy(Attribute!);
                    break;
            }
        }
    }
}
