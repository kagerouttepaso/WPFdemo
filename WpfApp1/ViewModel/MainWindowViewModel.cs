using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Reactive.Bindings;

namespace WpfApp1.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public IReactiveProperty<string> MainText { get; } = new ReactivePropertySlim<string>("ここに文字を入力");
        public IReadOnlyReactiveProperty<int> TextCount { get; }
        public IReadOnlyReactiveProperty<DateTime> Now { get; }

        public MainWindowViewModel()
        {
            TextCount = MainText
                .Select(s => s.Length)
                .ToReadOnlyReactivePropertySlim();

            Now = Observable.Interval(TimeSpan.FromSeconds(1))
                .Select(_ => DateTime.Now)
                .ToReadOnlyReactivePropertySlim(DateTime.Now);
        }
    }
}