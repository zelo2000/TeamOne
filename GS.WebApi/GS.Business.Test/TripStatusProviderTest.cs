using FluentAssertions;
using GS.Business.Infrastructure;
using GS.Data.Entities;
using GS.Domain.Enums;
using Moq;
using NUnit.Framework;
using System;

namespace GS.Business.Test
{
    public class TripStatusProviderTest
    {
        private Mock<IDateTimeProvider> _dateTimeProvider;
        private TripStatusProvider _tripStatusProvider;

        [SetUp]
        public void SetUp()
        {
            _dateTimeProvider = new Mock<IDateTimeProvider>();
            _tripStatusProvider = new TripStatusProvider(_dateTimeProvider.Object);
        }

        [Test]
        public void GetStatus_TripWithoutDates_Planed()
        {
            var trip = new Trip
            {
                StartDate = null,
                EndDate = null
            };

            var result = _tripStatusProvider.GetStatus(trip);

            result.Should().Be(TripStatus.Planned);
        }

        [Test]
        public void GetStatus_TripWithStartDateLessThanNow_Planed()
        {
            var now = DateTime.UtcNow;
            var trip = new Trip
            {
                StartDate = now.AddDays(-1)
            };

            _dateTimeProvider.Setup(x => x.GetUtcNow())
                .Returns(now);

            var result = _tripStatusProvider.GetStatus(trip);

            result.Should().Be(TripStatus.Planned);
        }

        [Test]
        public void GetStatus_TripWithEndDateLessThanNow_Closed()
        {
            var now = DateTime.UtcNow;
            var trip = new Trip
            {
                StartDate= now.AddDays(-2),
                EndDate = now.AddDays(-1)
            };

            _dateTimeProvider.Setup(x => x.GetUtcNow())
                .Returns(now);

            var result = _tripStatusProvider.GetStatus(trip);

            result.Should().Be(TripStatus.Closed);
        }

        [Test]
        public void GetStatus_TripWithStartDateGreaterAndEndDateLessThanNow_InProgress()
        {
            var now = DateTime.UtcNow;
            var trip = new Trip
            {
                StartDate = now.AddDays(-1),
                EndDate = now.AddDays(1)
            };

            _dateTimeProvider.Setup(x => x.GetUtcNow())
                .Returns(now);

            var result = _tripStatusProvider.GetStatus(trip);

            result.Should().Be(TripStatus.InProgress);
        }
    }
}
