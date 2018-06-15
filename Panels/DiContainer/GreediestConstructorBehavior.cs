using System;
using System.Linq;
using System.Reflection;
using SimpleInjector.Advanced;

namespace Panels.DiContainer
{
    /// <summary>
    /// 最大長のコンストラクタを検索するビヘイビア
    /// </summary>
    public class GreediestConstructorBehavior : IConstructorResolutionBehavior
    {
        public ConstructorInfo GetConstructor(Type implementationType)
            => implementationType
            .GetConstructors()
            .OrderByDescending(x => x.GetParameters().Length)
            .First();
    }
}