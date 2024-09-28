using DocPlanner.Validators;
using DocPlanner.Models.Request;

namespace DocPlannerTests
{
    public class ValidatorTests
    {

        [Fact]
        public void Date_Greater_Equal_Now()
        {
            var date = DateTime.Now.Date;
            var dateValidator = new DateValidator();
            var result = dateValidator.Validate(date);
            Assert.True(result.IsValid);
        }

        [Fact]
        public void Date_Not_Greater_Equal_Now()
        {
            var date = DateTime.Now.Date.AddDays(-2);
            var dateValidator = new DateValidator();
            var result = dateValidator.Validate(date);
            Assert.False(result.IsValid);
        }

        [Fact]
        public void Appointment_End_Greater_Than_Start()
        {
            var app = new Appointment(
                new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D"), DateTime.Now.AddMinutes(30), DateTime.Now.AddMinutes(40), string.Empty,
                new Patient("Charles", "Darwin", "charles@evolution.com,", "888 888 888"));
            var appointmentValidator = new AppointmentValidator();
            var result = appointmentValidator.Validate(app);
            Assert.True(result.IsValid);
        }

        [Fact]
        public void Appointment_End_Not_Greater_Than_Start()
        {
            var app = new Appointment(
                new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D"), DateTime.Now.AddMinutes(30), DateTime.Now.AddMinutes(-10), string.Empty,
                new Patient("Charles", "Darwin", "charles@evolution.com,", "888 888 888"));
            var appointmentValidator = new AppointmentValidator();
            var result = appointmentValidator.Validate(app);
            Assert.False(result.IsValid);
        }
    }
}
