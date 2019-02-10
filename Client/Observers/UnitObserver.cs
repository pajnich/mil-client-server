using Client.viewmodels;
using Common;
using System;

namespace Client.Observers
{
    public class UnitObserver : IObserver<Unit>
    {
        private IDisposable unsubscriber;
        private readonly UnitViewModel unitViewModel;

        public UnitObserver(UnitViewModel unitViewModel)
        {
            this.unitViewModel = unitViewModel;
        }

        public virtual void Subscribe(IObservable<Unit> provider)
        {
            unsubscriber = provider.Subscribe(this);
        }

        public virtual void Unsubscribe()
        {
            unsubscriber.Dispose();
        }

        public virtual void OnCompleted()
        {
            Console.WriteLine("Additional units will not be transmitted.");
        }

        public virtual void OnError(Exception error)
        {
            // Do nothing.
        }

        public virtual void OnNext(Unit unit)
        {
            unitViewModel.Unit = unit;
        }
    }
}
