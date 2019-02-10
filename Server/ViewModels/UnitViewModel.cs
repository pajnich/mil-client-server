using Common;
using Common.viewmodels;
using Server.Network;
using System.Windows.Input;

namespace Server.viewmodels
{
    public class UnitViewModel : ViewModel
    {
        private readonly DelegateCommand _sendUnitDataCommand;
        public ICommand SendUnitDataCommand => _sendUnitDataCommand;
        
        private Unit _unit;
        public Unit Unit {
            get => _unit;
            set => SetProperty(ref _unit, value);
        }

        public UnitViewModel()
        {
            Unit = new Unit();
            _sendUnitDataCommand = new DelegateCommand(OnSendUnitData);
        }

        private void OnSendUnitData(object commandParameter)
        {
            UnitBroadcaster.BroadcastUnit(Unit);
        }
    }
}
