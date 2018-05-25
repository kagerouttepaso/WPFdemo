using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Reactive.Bindings;

namespace ConverterSample
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public ReactivePropertySlim<List<string>> StringList { get; } = new ReactivePropertySlim<List<string>>();
        public ReactivePropertySlim<List<List<double>>> DoubleListList { get; } = new ReactivePropertySlim<List<List<double>>>();

        public List<List<double>> RawDoubleListList { get; private set; } = new List<List<double>>();

        public ReactiveCommand TestCommand { get; } = new ReactiveCommand();

        public ReactiveCommand TyottokaeruCommand { get; } = new ReactiveCommand();

        private Random Rand { get; } = new Random();

        public MainWindowViewModel()
        {
            TestCommand.Subscribe(_ =>
           {
               var rList = Enumerable.Range(0, 10)
                   .Select(x => (double)Rand.Next(100))
                   .ToArray();

               StringList.Value = rList
                   .Select(x => x.ToString("0.0"))
                   .ToList();

               DoubleListList.Value = Enumerable.Range(0, 10)
                   .Select(x => rList.Select(y => y + x).ToList())
                   .ToList();
           });
            TestCommand
                .ObserveOn(UIDispatcherScheduler.Default)
                .Subscribe(_ =>
                {
                    var rList = Enumerable.Range(0, 10)
                        .Select(x => (double)Rand.Next(100))
                        .ToArray();
                    RawDoubleListList = Enumerable.Range(0, 10)
                        .Select(x => rList.Select(y => y + x).ToList())
                        .ToList();

                    RaisePropertyChanged(nameof(RawDoubleListList));
                });

            TyottokaeruCommand
                .ObserveOn(UIDispatcherScheduler.Default)
                .Subscribe(_ =>
                {
                    RawDoubleListList[0] = Enumerable.Range(0, 3)
                        .Select(s => (double)Rand.Next(100))
                        .ToList();
                    RaisePropertyChanged(nameof(RawDoubleListList));
                });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}