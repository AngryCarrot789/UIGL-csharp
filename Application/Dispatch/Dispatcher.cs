using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading;

namespace UIGL.Application.Dispatch {
    public class Dispatcher {
        private static readonly Dictionary<Thread, Dispatcher> MAP = new Dictionary<Thread, Dispatcher>();
        private static readonly string[] PriorityNames = Enum.GetNames(typeof(Priority));

        public static Dispatcher Current {
            get {
                lock (MAP) {
                    Thread thread = Thread.CurrentThread;
                    if (MAP.TryGetValue(thread, out Dispatcher dispatcher)) {
                        return dispatcher;
                    }

                    return MAP[thread] = new Dispatcher(thread);
                }
            }
        }

        private readonly Dictionary<Priority, DispatchQueue> queues;

        public Thread Thread { get; }

        public Object Parameter1 { get; set; }
        public Object Parameter2 { get; set; }
        public Object Parameter3 { get; set; }
        public Object Parameter4 { get; set; }

        public bool IsOwningThread => Thread.CurrentThread == this.Thread;

        private Dispatcher(Thread thread) {
            this.Thread = thread;
            this.queues = new Dictionary<Priority, DispatchQueue>();
        }

        private DispatchQueue GetQueue(Priority priority) {
            lock (this.queues) {
                return this.queues.TryGetValue(priority, out DispatchQueue queue) ? queue : this.queues[priority] = new DispatchQueue();
            }
        }

        public static void RequestProcessing() {
            App.Wake();
        }

        public TResult InvokeFunction<TResult>(Func<TResult> getter) {
            Wrapper<TResult> wrapper = new Wrapper<TResult>();
            DispatcherOperation operation = this.Invoke(() => {
                wrapper.Value = getter();
            });

            operation.WaitForCompletion();
            operation.ValidateCompletionState();
            return wrapper.Value;
        }

        public DispatcherOperation Invoke(Action runnable) {
            return this.Invoke(runnable, Priority.ASAP);
        }

        public DispatcherOperation Invoke(Action runnable, Priority priority) {
            DispatcherOperation operation = new DispatcherOperation(this, priority, runnable);
            this.Invoke(operation);
            return operation;
        }

        public void Invoke(DispatcherOperation operation) {
            if (operation == null) {
                throw new ArgumentNullException(nameof(operation), "Operation cannot be null");
            }

            if (operation.Priority == Priority.ASAP && this.IsOwningThread) {
                operation.Invoke();
            }
            else {
                DispatchQueue queue = this.GetQueue(operation.Priority);
                lock (queue) {
                    queue.Add(operation);
                }

                RequestProcessing();
            }
        }

        public void Process(Priority priority) {
            DispatchQueue queue = this.GetQueue(priority);
            lock (queue) {
                if (queue.Count > 0) {
                    foreach (DispatcherOperation operation in queue) {
                        operation.Invoke();
                    }

                    queue.Clear();
                }
            }
        }

        private class DispatchQueue : List<DispatcherOperation> { }
    }
}