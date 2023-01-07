using System;
using System.Diagnostics;

namespace UIGL.Application.Dispatch {
    public class DispatcherOperation {
        public static bool IsDebugMode = true;

        private volatile bool isAborted;
        private volatile Status status;
        private volatile Exception error;

        public StackTrace CreationTrace { get; }

        public Dispatcher Dispatcher { get; }

        public Priority Priority { get; }

        public Runnable Target { get; }

        public Status Status => this.status;

        public bool Aborted {
            get => this.isAborted;
            set => this.isAborted = value;
        }

        public DispatcherOperation(Dispatcher dispatcher, Priority priority, Runnable target) {
            this.Dispatcher = dispatcher ?? throw new ArgumentNullException(nameof(dispatcher), "Dispatcher cannot be null");
            this.Priority = priority;
            this.Target = target ?? throw new ArgumentNullException(nameof(target), "Target cannot be null");
            this.CreationTrace = IsDebugMode ? new StackTrace() : null;
            this.status = Status.Pending;
        }

        internal void Invoke() {
            this.status = Status.Executing;
            try {
                if (this.isAborted) {
                    this.status = Status.Aborted;
                }
                else {
                    this.Target();
                    this.status = this.isAborted ? Status.CompletedAborted : Status.CompletedSuccess;
                }
            }
            catch (Exception e) {
                this.status = Status.CompletedFailure;
                this.error = e;
            }
        }

        public void Reset() {
            this.isAborted = false;
            this.status = Status.Pending;
            this.error = null;
        }
    }
}