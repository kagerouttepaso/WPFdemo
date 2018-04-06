using Microsoft.VisualStudio.TestTools.UnitTesting;
using WpfApp1.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace WpfApp1.ViewModel.Tests
{
    [TestClass()]
    public class MainWindowViewModelTests
    {
        [TestMethod()]
        public void MainWindowViewModelTest_正常系_文字数連動()
        {
            var target = new MainWindowViewModel();
            for (int i = 0; i < 100; i++)
            {
                var s = new string('a', i);
                target.MainText.Value = s;
                Assert.AreEqual(s.Length, target.TextCount.Value);
            }
        }

        [TestMethod()]
        public void MainWindowViewModelTest_正常系_現在時刻()
        {
            var target = new MainWindowViewModel();
            var d = target.Now.Value - DateTime.Now;
            Assert.IsTrue(d < TimeSpan.FromSeconds(1));
            Thread.Sleep(TimeSpan.FromSeconds(3));
            d = target.Now.Value - DateTime.Now;
            Assert.IsTrue(d < TimeSpan.FromSeconds(1));
        }
    }
}