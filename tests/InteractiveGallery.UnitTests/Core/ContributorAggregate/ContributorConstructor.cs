using InteractiveGallery.Core.ContributorAggregate;
using Xunit;

namespace InteractiveGallery.UnitTests.Core.ContributorAggregate;
public class ContributorConstructor
{
  private readonly string _testName = "test name";
  private Contributor? _testContributor;

  private Contributor CreateContributor()
  {
    return new Contributor(1,_testName);
  }

  [Fact]
  public void InitializesName()
  {
    _testContributor = CreateContributor();

    Assert.Equal(_testName, _testContributor.Name);
  }
}
