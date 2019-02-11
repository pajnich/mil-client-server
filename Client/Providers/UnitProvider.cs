using Common;
using System;
using System.Collections.Generic;

namespace Client.Providers
{
    public class UnitProvider : IObservable<Unit>
    {
        List<IObserver<Unit>> observers;

        public UnitProvider()
        {
            observers = new List<IObserver<Unit>>();
        }

        private class Unsubscriber : IDisposable
        {
            private List<IObserver<Unit>> _observers;
            private IObserver<Unit> _observer;

            public Unsubscriber(List<IObserver<Unit>> observers, IObserver<Unit> observer)
            {
                this._observers = observers;
                this._observer = observer;
            }

            public void Dispose()
            {
                if (!(_observer == null)) _observers.Remove(_observer);
            }
        }

        public IDisposable Subscribe(IObserver<Unit> observer)
        {
            if (!observers.Contains(observer))
                observers.Add(observer);

            return new Unsubscriber(observers, observer);
        }

        public void ProvideUnit(Unit unit)
        {
            foreach (var observer in observers)
                observer.OnNext(unit);
        }
    }
}
