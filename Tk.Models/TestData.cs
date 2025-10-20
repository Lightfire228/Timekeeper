namespace Tk.Models;

using Tk.Models.Database;

public static class TestData {

    static long TaskId  { get; set; } = 0;
    static long EventId { get; set; } = 0;

    public static List<TaskModel> Tasks { get; private set; } = [
        new() {
            Id          = ++TaskId,
            Name        = "FUCKEN HIGH Priority Task",
            Description = "Description",
            Priority    = TaskPriority.FUCKEN_HIGH,
            Due         = new DateTime(2026, 01, 01),
        },
        new() {
            Id          = ++TaskId,
            Name        = "High Priority Task",
            Description = "Description",
            Priority    = TaskPriority.High,
            Due         = new DateTime(2026, 01, 01),
        },
        new() {
            Id          = ++TaskId,
            Name        = "Medium Priority Task",
            Description = "Description",
            Priority    = TaskPriority.Medium,
            Due         = new DateTime(2025, 01, 01),
        },
        new() {
            Id          = ++TaskId,
            Name        = "Low Priority Task",
            Description = "Description",
            Priority    = TaskPriority.Low,
            Due         = new DateTime(2025, 01, 01),
        },
        new() {
            Id          = ++TaskId,
            Name        = "None Priority Task",
            Description = "Description",
            Priority    = TaskPriority.None,
            Due         = new DateTime(2025, 01, 01),
        },
        new() {
            Id          = ++TaskId,
            Name        = "Long Description Task",
            Description = "Long Description Lorem Ipsum Dolor Salut",
            Priority    = TaskPriority.Low,
            Due         = new DateTime(2024, 01, 01),
        },
        new() {
            Id          = ++TaskId,
            Name        = "Overdue Task",
            Description = "Description",
            Priority    = TaskPriority.Low,
            Due         = new DateTime(2024, 01, 01),
        },
        new() {
            Id          = ++TaskId,
            Name        = "No Due Date Task",
            Description = "Description",
            Priority    = TaskPriority.Low,
            Due         = null,
        },
        new() {
            Id          = ++TaskId,
            Name        = "Completed Task",
            Description = "Description",
            Priority    = TaskPriority.Low,
            Due         = new DateTime(2026, 01, 01),

            CompletionEvents = [
                new() {
                    Id          = ++EventId,
                    TaskId      = TaskId,
                    CompletedAt = new DateTime(2025, 01, 01),
                }
            ]
        },
        new() {
            Id          = ++TaskId,
            Name        = "Double Completed Task",
            Description = "Description",
            Priority    = TaskPriority.Low,
            Due         = new DateTime(2026, 01, 01),

            CompletionEvents = [
                new() {
                    Id          = ++EventId,
                    TaskId      = TaskId,
                    CompletedAt = new DateTime(2025, 01, 01),
                },
                new() {
                    Id          = ++EventId,
                    TaskId      = TaskId,
                    CompletedAt = new DateTime(2024, 01, 01),
                }
            ]
        },
    ];

}