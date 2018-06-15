using System;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace WpfApp1.ViewModel
{
    /// <summary>
    /// メインウィンドウ用のViewModel
    /// </summary>
    public class MainWindowViewModel : INotifyPropertyChanged, IDisposable
    {
        /// <summary>
        /// DataContextに割り付ける型は必ずINotifyPropertyChangedを継承していないと
        /// WPFの環境ではメモリリークが発生する!!
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        private CompositeDisposable Disposable { get; } = new CompositeDisposable();

        /// <summary>
        /// メインコンテンツ
        /// </summary>
        public IReactiveProperty<string> MainText { get; } = new ReactivePropertySlim<string>("ここに文字を入力");

        /// <summary>
        /// 小文字に変換した文字列
        /// </summary>
        public IReadOnlyReactiveProperty<string> LowerText { get; }

        /// <summary>
        /// 文字数
        /// </summary>
        public IReadOnlyReactiveProperty<int> TextCount { get; }

        /// <summary>
        /// 現在時刻
        /// </summary>
        public IReadOnlyReactiveProperty<DateTime> Now { get; }

        public MainWindowViewModel()
        {
            MainText.AddTo(Disposable);

            LowerText = MainText
                .Select(x => ConvertLower(x))
                .ToReadOnlyReactivePropertySlim()
                .AddTo(Disposable);

            TextCount = MainText
                .Select(s => s.Length)
                .ToReadOnlyReactivePropertySlim()
                .AddTo(Disposable);

            Now = Observable.Interval(TimeSpan.FromSeconds(1))
                .Select(_ => DateTime.Now)
                .ToReadOnlyReactivePropertySlim(DateTime.Now)
                .AddTo(Disposable);
        }

        public static string ConvertLower(string baseString)
        {
            if(baseString.Length <= 30)
            {
                return baseString;
            }
            else
            {
                return baseString.ToLower();
            }
        }

        public void Dispose()
        {
            Disposable.Dispose();
        }
    }
}