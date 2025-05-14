using ApiTaskManagement.Services;
using ApiTaskManagement.Utils.Exceptions;

namespace ApiTaskManagementTests.Services
{
    public class TaskServiceTests
    {
        private readonly TaskService _service;

        public TaskServiceTests()
        {
            _service = new TaskService(null!, null!);
        }

        [Fact]
        public void TryParseDateRange_ValidDates_ReturnsRange()
        {
            var result = _service.TryParseDateRange("2025-05-01", "2025-05-10");

            Assert.NotNull(result);
            Assert.Equal(new DateTime(2025, 5, 1), result.Value.from);
            Assert.Equal(new DateTime(2025, 5, 10, 23, 59, 59), result.Value.to);
        }

        [Fact]
        public void TryParseDateRange_FromGreaterThanEnd_ThrowsHttpException()
        {
            var ex = Assert.Throws<HttpException>(() =>
                _service.TryParseDateRange("2025-06-01", "2025-05-01"));

            Assert.Equal(400, ex.StatusCode);
            Assert.Equal("La fecha fin debe ser mayor o igual a la fecha de inicio.", ex.Message);
        }

        [Fact]
        public void TryParseDateRange_InvalidDates_ReturnsNull()
        {
            var result = _service.TryParseDateRange("no-date", "2025-05-01");
            Assert.Null(result);

            result = _service.TryParseDateRange("2025-05-01", null);
            Assert.Null(result);
        }

        [Fact]
        public void TryParseDateRange_OnlyFromOrEndProvided_ReturnsNull()
        {
            var result1 = _service.TryParseDateRange("2025-05-01", null);
            var result2 = _service.TryParseDateRange(null, "2025-05-01");

            Assert.Null(result1);
            Assert.Null(result2);
        }
    }

}
