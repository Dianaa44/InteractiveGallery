using InteractiveGallery.Core.ProjectAggregate;
using Xunit;

namespace InteractiveGallery.UnitTests.Core.ProjectAggregate;
public class Project_AddItem
{
  private Project _testProject = new Project(5,"some name", PriorityStatus.Backlog);

  [Fact]
  public void AddsItemToItems()
  {
    var _testItem = new ToDoItem(1)
    {
      Title = "title",
      Description = "description"
    };

    _testProject.AddItem(_testItem);

    Assert.Contains(_testItem, _testProject.Items);
  }

  [Fact]
  public void ThrowsExceptionGivenNullItem()
  {
#nullable disable
    Action action = () => _testProject.AddItem(null);
#nullable enable

    var ex = Assert.Throws<ArgumentNullException>(action);
    Assert.Equal("newItem", ex.ParamName);
  }
}
