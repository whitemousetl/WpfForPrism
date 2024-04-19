using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WpfForPrism.Event;
using WpfForPrism.Views;


namespace WpfForPrism.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        //区域管理
        private readonly IRegionManager _regionManager;

        //导航记录
        private IRegionNavigationJournal _regionNavigationJournal = default!;

        //对话框服务
        private readonly IDialogService _dialogService;

        private readonly IEventAggregator _eventAggregator;

        //显示区域命令
        public DelegateCommand<string> ShowContentCom { get; set; }

        //后退命令
        public DelegateCommand GoBackCom { get; set; }

        //打开对话框命令
        public DelegateCommand<string> ShowDialogCom { get; set; }

        //发布命令
        public DelegateCommand PubCom { get; set; }

        //订阅命令
        public DelegateCommand SubCom { get; set; }

        //取消订阅命令
        public DelegateCommand UnSubCom { get; set; }

        //构造函数
        public MainWindowViewModel(IRegionManager regionManager, IDialogService dialogService,
            IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            _dialogService = dialogService;

            _regionManager = regionManager;

            _regionManager.RegisterViewWithRegion("ContentRegion", "Initial");

            ShowContentCom = new(ShowContentFunc);

            GoBackCom = new(GoBack);

            ShowDialogCom = new(ShowDialogFuc);

            PubCom = new(Pub);

            SubCom = new(Sub);

            UnSubCom = new(UnSub);
        }

        /// <summary>
        /// 对话框关闭函数的回调函数
        /// </summary>
        /// <param name="dialogResult"></param>
        private void DialogService_RequestClose(IDialogResult dialogResult)
        {
            // 获取传递的参数
            if (dialogResult.Parameters.ContainsKey("result1"))
            {
                string result1 = dialogResult.Parameters.GetValue<string>("result1");
                // 在这里处理result1参数
            }

            if (dialogResult.Parameters.ContainsKey("result2"))
            {
                string result2 = dialogResult.Parameters.GetValue<string>("result2");
                // 在这里处理result2参数
            }
        }

        private void UnSub()
        {
            _eventAggregator.GetEvent<MsgEvent>().Unsubscribe(SubEvent);
        }

        /// <summary>
        /// 发布
        /// </summary>
        private void Sub()
        {
            _eventAggregator.GetEvent<MsgEvent>().Subscribe(SubEvent);
        }

        /// <summary>
        /// 订阅
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void Pub()
        {
            _eventAggregator.GetEvent<MsgEvent>().Publish("我想去旅游了");
        }

        /// <summary>
        /// 订阅
        /// </summary>
        /// <param name="obj"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void SubEvent(string obj)
        {
            MessageBox.Show($"我收到订阅的信息：{obj}");
        }



        /// <summary>
        /// 打开对话框并传递参数
        /// </summary>
        /// <param name="ucName"></param>
        private void ShowDialogFuc(string ucName)
        {
            DialogParameters paras = [];
            paras.Add("Title", "动态传递的标题");
            paras.Add("para1", "业务参数1");
            paras.Add("para2", "业务参数2");
            _dialogService.ShowDialog(ucName, paras, callback =>
            {
                if (callback.Result == ButtonResult.Yes)
                {
                    string r1 = callback.Parameters.GetValue<string>("result1");
                    string r2 = callback.Parameters.GetValue<string>("result2");
                }

                if (callback.Result == ButtonResult.No)
                {

                }

                if (callback.Result == ButtonResult.None)
                {

                }
            });
        }
        
        /// <summary>
        /// 区域返回
        /// </summary>
        private void GoBack()
        {
            if (_regionNavigationJournal != null && _regionNavigationJournal.CanGoBack == true)
                _regionNavigationJournal.GoBack();
        }

        #region 改变显示用户控件方法
        /// <summary>
        /// 改变显示用户控件,带有参数的,使用区域管理依赖注入
        /// </summary>
        /// <param name="viewName"></param>
        private void ShowContentFunc(string viewName)
        {
            NavigationParameters paras = [];

            paras.Add("MsgA", "大家好，我是A");

            _regionManager.Regions["ContentRegion"].RequestNavigate(viewName, callBack =>
            {
                _regionNavigationJournal = callBack.Context.NavigationService.Journal;
            }, paras);
        }
        #endregion

        #region 用户控件属性 已注释(用区域代替)
        //private UserControl _showContext = new();

        //public UserControl ShowContext
        //{
        //	get { return _showContext; }
        //	set
        //	{
        //		_showContext = value;
        //		RaisePropertyChanged();

        //	}
        //} 
        #endregion
    }
}
