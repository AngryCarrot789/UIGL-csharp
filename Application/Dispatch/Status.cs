using System;

namespace UIGL.Application.Dispatch {
    [Flags]
    public enum Status {
        Pending          = 0b000001,
        Aborted          = 0b000010,
        Completed        = 0b000100,
        CompletedAborted = 0b000110,
        CompletedSuccess = 0b001100,
        CompletedFailure = 0b010100,
        Executing        = 0b100000
    }

    public static class StatusExtensions {
        public static bool IsCompleted(this Status status) {
            return (status & Status.Completed) != 0;
        }
    }
}