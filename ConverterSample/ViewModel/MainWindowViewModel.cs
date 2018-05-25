using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Reactive.Bindings;

namespace ConverterSample.ViewModel
{
    /// <summary>
    /// ViewModel
    /// </summary>
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Stringのリスト
        /// </summary>
        public IReactiveProperty<List<string>> StringList { get; } = new ReactivePropertySlim<List<string>>();

        /// <summary>
        /// Doubleのリストのリスト
        /// </summary>
        public IReactiveProperty<List<List<double>>> DoubleListList { get; } = new ReactivePropertySlim<List<List<double>>>();

        /// <summary>
        /// Doubleのリストのリスト(Propertychangedの基本的な実装)
        /// </summary>
        public List<List<double>> RawDoubleListList
        {
            get => _rawDoubleListList;
            private set
            {
                if(value == _rawDoubleListList) return;
                _rawDoubleListList = value;
                RaisePropertyChanged();
            }
        }

        private List<List<double>> _rawDoubleListList = new List<List<double>>();

        /// <summary>
        /// テスト用コマンド
        /// </summary>
        public ReactiveCommand TestCommand { get; } = new ReactiveCommand();

        public ReactiveCommand DeleteCommand { get; } = new ReactiveCommand();

        /// <summary>
        /// ちょっと調整用のコマンド
        /// </summary>
        public ReactiveCommand TyottokaeruCommand { get; }

        /// <summary>
        /// ランダム生成機
        /// </summary>
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

                RawDoubleListList = Enumerable.Range(0, 10)
                    .Select(x => rList.Select(y => y + x).ToList())
                    .ToList();
            });

            DeleteCommand.Subscribe(_ =>
            {
                StringList.Value = new List<string>();
                DoubleListList.Value = new List<List<double>>();
                RawDoubleListList = new List<List<double>>();
            });

            TyottokaeruCommand = Observable
                // イベントからObserbableを作成する
                .FromEvent<PropertyChangedEventHandler, PropertyChangedEventArgs>(
                    conversion: h => (s, e) => h(e),  // EventArgsを引数に実行する
                    addHandler: h => PropertyChanged += h, // ハンドルへの追加方法
                    removeHandler: h => PropertyChanged -= h) // ハンドルから削除する方法
                .Where(x => x.PropertyName == nameof(RawDoubleListList)) // RawDoubleListListの変更時
                .Select(x => RawDoubleListList.Count >= 1) // 要素数が1以上の時有効になるコマンドを作成
                .ToReactiveCommand(initialValue: false);

            TyottokaeruCommand
                .ObserveOn(UIDispatcherScheduler.Default)
                .Subscribe(_ =>
                {
                    //要素の先導を作り直す
                    RawDoubleListList[0] = Enumerable.Range(0, 3)
                        .Select(s => (double)Rand.Next(100))
                        .ToList();
                    // Listを作り直していないので、明示的にPropertyChangedを呼び出す
                    RaisePropertyChanged(nameof(RawDoubleListList));
                });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged([CallerMemberName] string name = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}