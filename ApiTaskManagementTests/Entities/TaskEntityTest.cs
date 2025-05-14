using ApiTaskManagement.Constants;
using ApiTaskManagement.Entities;

namespace ApiTaskManagementTests.Entities
{
    public class TaskEntityTests
    {
        [Fact]
        public void Update_ShouldUpdateBasicFields()
        {
            // Arrange
            var original = new TaskEntity
            {
                Id = 1,
                Title = "Old Title",
                Description = "Old Desc",
                PriorityId = 1,
                StateId = TaskStateConstants.Done+1,
                UserId = "user1"
            };
            var updated = new TaskEntity
            {
                Title = "New Title",
                Description = "New Desc",
                PriorityId = 2,
                StateId = TaskStateConstants.Done+1,
            };

            // Act
            original.update(updated);

            // Assert
            Assert.Equal("New Title", original.Title);
            Assert.Equal("New Desc", original.Description);
            Assert.Equal(2, original.PriorityId);
            Assert.Null(original.DateClose);
        }

        [Fact]
        public void Update_ShouldSetDateClose_WhenStateChangedToDone()
        {
            // Arrange
            var original = new TaskEntity
            {
                StateId = TaskStateConstants.Done+1,
                DateClose = null
            };
            var updated = new TaskEntity
            {
                StateId = TaskStateConstants.Done
            };

            // Act
            original.update(updated);

            // Assert
            Assert.Equal(TaskStateConstants.Done, original.StateId);
            Assert.NotNull(original.DateClose);
            Assert.True(original.DateClose.Value.Kind == DateTimeKind.Utc);
        }

        [Fact]
        public void Update_ShouldClearDateClose_WhenStateChangedFromDone()
        {
            // Arrange
            var original = new TaskEntity
            {
                StateId = TaskStateConstants.Done,
                DateClose = DateTime.UtcNow
            };
            var updated = new TaskEntity
            {
                StateId = TaskStateConstants.Done + 1 // Not Done
            };

            // Act
            original.update(updated);

            // Assert
            Assert.Equal(TaskStateConstants.Done + 1, original.StateId);
            Assert.Null(original.DateClose);
        }

        [Fact]
        public void Update_ShouldConvertDateCloseToUtc()
        {
            // Arrange
            var localDate = DateTime.Now;
            var original = new TaskEntity
            {
                StateId = 1,
                DateClose = localDate
            };
            var updated = new TaskEntity
            {
                StateId = 1
            };

            // Act
            original.update(updated);

            // Assert
            Assert.Equal(localDate.ToUniversalTime(), original.DateClose);
        }

        [Fact]
        public void Update_ShouldNotUpdateDateClose()
        {
            // Arrange
            var original = new TaskEntity
            {
                StateId = 1,
                DateClose = null,
            };
            var updated = new TaskEntity
            {
                StateId = 1,
                DateClose = DateTime.Now
            };

            // Act
            original.update(updated);

            // Assert
            Assert.Null(original.DateClose);
        }
    }
}
