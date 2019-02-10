using Client.Network;
using Client.Observers;
using Client.Providers;
using Common;
using Common.viewmodels;

namespace Client.viewmodels
{
    public class UnitViewModel : ViewModel
    {
        private Unit _unit;
        public Unit Unit {
            get => _unit;
            set => SetProperty(ref _unit, value);
        }

        public UnitViewModel()
        {
            UnitProvider unitProvider = new UnitProvider();
            UnitObserver unitObserver = new UnitObserver(this);
            unitObserver.Subscribe(unitProvider);
            UnitListeningTask unitListeningTask = new UnitListeningTask(unitProvider);
        }
    }
}
