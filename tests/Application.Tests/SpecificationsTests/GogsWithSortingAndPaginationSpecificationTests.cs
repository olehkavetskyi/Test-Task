namespace Application.Tests.SpecificationsTests;

using Application.Specifications;
using Xunit;

public class GogsWithSortingAndPaginationSpecificationTests
{
    [Fact]
    public void Specification_AppliesPaging_WhenPageSizeIsProvided()
    {
        var specParams = new SpecParams { PageSize = 10, PageNumber = 2 };
        var specification = new GogsWithSortingAndPaginationSpecification(specParams);

        var isPagingEnabled = specification.IsPagingEnabled;
        var skip = specification.Skip;
        var take = specification.Take;

        Assert.True(isPagingEnabled);
        Assert.Equal(10, take);
        Assert.Equal(10, skip);
    }

    [Fact]
    public void Specification_DoesNotApplyPaging_WhenPageSizeIsNotProvided()
    {
        var specParams = new SpecParams { PageNumber = 2 };
        var specification = new GogsWithSortingAndPaginationSpecification(specParams);

        var isPagingEnabled = specification.IsPagingEnabled;

        Assert.False(isPagingEnabled);
    }

    [Fact]
    public void Specification_SortsByWeight_WhenAttributeIsWeight()
    {
        var specParams = new SpecParams { Attribute = "weight", Order = "asc" };
        var specification = new GogsWithSortingAndPaginationSpecification(specParams);

        var orderBy = specification.OrderBy;
        var orderByDescending = specification.OrderByDescending;

        Assert.NotNull(orderBy);
        Assert.Null(orderByDescending);
    }

    [Fact]
    public void Specification_SortsByWeightDesc_WhenAttributeIsWeight()
    {
        var specParams = new SpecParams { Attribute = "weight", Order = "desc" };
        var specification = new GogsWithSortingAndPaginationSpecification(specParams);

        var orderBy = specification.OrderBy;
        var orderByDescending = specification.OrderByDescending;

        Assert.Null(orderBy);
        Assert.NotNull(orderByDescending);
    }

    [Fact]
    public void Specification_WithPaginationAndSort_WhenAttributeIsTailLength()
    {
        var specParams = new SpecParams { PageNumber = 1, PageSize = 2, Attribute = "tail_length", Order = "asc" };
        var specification = new GogsWithSortingAndPaginationSpecification(specParams);

        var orderBy = specification.OrderBy;
        var orderByDescending = specification.OrderByDescending;
        var isPagingEnabled = specification.IsPagingEnabled;

        Assert.True(isPagingEnabled);
        Assert.NotNull(orderBy);
        Assert.Null(orderByDescending);
    }

    [Fact]
    public void Specification_SortsByName_WhenAttributeIsInvalid()
    {
        var specParams = new SpecParams { Attribute = "invalid", Order = "asc" };
        var specification = new GogsWithSortingAndPaginationSpecification(specParams);

        var orderBy = specification.OrderBy;
        var orderByDescending = specification.OrderByDescending;

        Assert.NotNull(orderBy);
        Assert.Null(orderByDescending);
    }
}
