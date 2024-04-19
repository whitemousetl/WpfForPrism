using ModuleA.ViewModels;
using ModuleA.Views;
using Prism.Ioc;
using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleA
{
    public class ModuleAProfile : IModule
    {
        /// <summary>
        /// 初始化方法
        /// </summary>
        /// <param name="containerProvider"></param>
        public void OnInitialized(IContainerProvider containerProvider)
        {
           
        }

        /// <summary>
        /// 注册方法，实现依赖注入
        /// </summary>
        /// <param name="containerRegistry"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //注入导航
            containerRegistry.RegisterForNavigation<ModuleViewA, ModuleViewAViewModel>();

            //注入对话框
            containerRegistry.RegisterDialog<DialogUC,DialogUCViewModel>();
        }
    }
}
