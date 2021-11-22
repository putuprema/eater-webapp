using System.Runtime.CompilerServices;

namespace EaterWebClient.ViewModels
{
    public abstract class BaseViewModel
    {
        private bool _loading;
        public bool Loading
        {
            get => _loading;
            protected set => SetValue(ref _loading, value);
        }

        protected void SetValue<T>(ref T backingField, T value, [CallerMemberName] string propertyName = "")
        {
            if (!EqualityComparer<T>.Default.Equals(backingField, value))
            {
                backingField = value;
            }
        }
    }
}
