using System.Reactive.Linq;
using System.Windows;
using Reactive.Bindings;

namespace Panels.ViewModel
{
    public class MainWindowViewModel
    {
        /// <summary>
        /// チェックボックス用のプロパティ
        /// </summary>
        public ReactivePropertySlim<bool> IsChecked { get; } = new ReactivePropertySlim<bool>(false);

        /// <summary>
        /// チェックボックスに連動する表示プロパティ
        /// </summary>
        public IReadOnlyReactiveProperty<Visibility> Visibility { get; }

        public MainWindowViewModel()
        {
            Visibility = IsChecked
                .Select(b => b ? System.Windows.Visibility.Visible : System.Windows.Visibility.Hidden)
                .ToReadOnlyReactivePropertySlim();
        }
    }
}