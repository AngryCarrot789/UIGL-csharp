using System;
using System.Collections.Generic;
using System.Threading;

namespace UIGL.Application.Dispatch {
    public class Dispatcher {
        private static readonly Dictionary<Thread, Dispatcher> MAP = new Dictionary<Thread, Dispatcher>();
        private static readonly string[] PriorityNames = Enum.GetNames(typeof(Priority));

        public static Dispatcher Current {
            get {
                lock (MAP) {
                    Thread current = Thread.CurrentThread;
                    if (MAP.TryGetValue(current, out Dispatcher dispatcher)) {
                        return dispatcher;
                    }

                    MAP[current] = dispatcher = new Dispatcher(current);
                    return dispatcher;
                }
            }
        }

        private readonly DispatchQueue[] queues;

        public Thread Thread { get; }

        public bool IsOwningThread => Thread.CurrentThread == this.Thread;

        private Dispatcher(Thread thread) {
            this.Thread = thread;
            this.queues = new DispatchQueue[PriorityNames.Length];
            for (int i = 0; i < this.queues.Length; i++) {
                this.queues[i] = new DispatchQueue();
            }
        }

        public static void RequestProcessing() {
            App.Wake();
        }

        public DispatcherOperation Invoke(Runnable runnable) {
            return this.Invoke(runnable, Priority.AppPre);
        }

        public DispatcherOperation Invoke(Runnable runnable, Priority priority) {
            DispatcherOperation operation = new DispatcherOperation(this, priority, runnable);
            this.Invoke(operation);
            return operation;
        }

        public void Invoke(DispatcherOperation operation) {
            if (operation == null) {
                throw new ArgumentNullException(nameof(operation), "Operation cannot be null");
            }

            DispatchQueue queue = this.queues[(int) operation.Priority];
            if (queue != null) {
                lock (queue) {
                    queue.Add(operation);
                }
            }
        }

        public void Process(Priority priority) {
            DispatchQueue queue = this.queues[(int) priority];
            if (queue != null) {
                lock (queue) {
                    if (queue.Count > 0) {
                        foreach (DispatcherOperation operation in queue) {
                            operation.Invoke();
                        }
                    }
                }
            }
        }

        private class DispatchQueue : List<DispatcherOperation> { }
    }
}