using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace CurrencyApp.Domain.Entities
{
    public class LogBook
    {
        [Key]
        [NotNull]
        public Guid Id { get; set; }
        [NotNull]
        public DateTime CreateAt { get; set; }
        [NotNull]
        public string Message { get; set; }
        [NotNull]
        public string? StackTrace { get; set; }
        [NotNull]
        public string? Source { get; set; }

        private LogBook(Exception exception)
        {
            if (exception == null)
                throw new ArgumentNullException(nameof(exception));

            Message = exception.Message;
            StackTrace = exception.StackTrace;
            Source = exception.Source;
            Id = Guid.NewGuid();
            CreateAt = DateTime.Now;
        }

        public static LogBook Create(Exception exception)
        {
            return new LogBook(exception);
        }

        public LogBook() { }
    }
}
