using System;
using System.Diagnostics;
using System.Threading;
using OpenTK.Graphics.ES20;

namespace UIGL.Application.Dispatch {
    public class DispatcherOperation {
        public static bool IsDebugMode = true;

        private volatile bool isAborted;
        private volatile Status status;
        private volatile Exception error;

        public StackTrace CreationTrace { get; }

        public Dispatcher Dispatcher { get; }

        public Priority Priority { get; }

        public Action Target { get; }

        public Status Status => this.status;

        public bool Aborted {
            get => this.isAborted;
            set => this.isAborted = value;
        }

        public DispatcherOperation(Dispatcher dispatcher, Priority priority, Action target) {
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

        public void WaitForCompletion() {
            while (!this.Status.IsCompleted()) {
                Thread.Sleep(1);
            }
        }

        public void WaitForCompletion(bool checkErrorStates) {
            while (!this.Status.IsCompleted()) {
                Thread.Sleep(1);
            }

            if (checkErrorStates) {
                this.ValidateCompletionState();
            }
        }

        public void ValidateCompletionState() {
            switch (this.Status) {
                case Status.CompletedAborted: throw new Exception($"Operation was aborted. {(IsDebugMode ? $"Creation trace: {this.CreationTrace}" : "")}");
                case Status.CompletedFailure: throw new Exception($"Operation failed: {this.error}. {(IsDebugMode ? $"Creation trace: {this.CreationTrace}" : "")}");
            }
        }
    }
}