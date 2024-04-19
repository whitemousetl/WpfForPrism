//using ModuleA;
//using ModuleB;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using WpfForPrism.ViewModels;
using WpfForPrism.Views;

namespace WpfForPrism;
/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : PrismApplication
{
    /// <summary>
    /// 设置启动页
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    protected override Window CreateShell()
    {
        var mainWindow = Container.Resolve<MainWindow>();
        mainWindow.DataContext = Container.Resolve<MainWindowViewModel>();
        return mainWindow;
    }

    /// <summary>
    /// 注入服务,添加用户控件
    /// </summary>
    /// <param name="containerRegistry"></param>
    /// <exception cref="NotImplementedException"></exception>
    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterForNavigation<UCA>();
        containerRegistry.RegisterForNavigation<UCB>();
        containerRegistry.RegisterForNavigation<UCC>();
        containerRegistry.RegisterForNavigation<Initial>();
    }


    // 添加模块的方法,需要在依赖项那里添加相关项目
    //protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
    //{
    //    moduleCatalog.AddModule<ModuleAProfile>();
    //    moduleCatalog.AddModule<ModuleBProfile>();
    //    base.ConfigureModuleCatalog(moduleCatalog);
    //}

    /// <summary>
    /// 添加dll文件到模块
    /// </summary>
    /// <returns></returns>
    protected override IModuleCatalog CreateModuleCatalog()
    {
        return new DirectoryModuleCatalog() { ModulePath = @".\Modules" };
    }

}

