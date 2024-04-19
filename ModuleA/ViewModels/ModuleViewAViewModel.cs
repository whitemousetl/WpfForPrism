using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ModuleA.ViewModels
{
    //IConfirmNavigationRequest继承了INavigationAware
    public class ModuleViewAViewModel : BindableBase, IConfirmNavigationRequest// INavigationAware
    {
		/// <summary>
		/// 绑定内容
		/// </summary>
		private string _msg = string.Empty;

		public string Msg
		{
			get { return _msg; }
			set 
			{
				_msg = value;
				RaisePropertyChanged();
			}
		}

        /// <summary>
        /// 确认
        /// </summary>
        /// <param name="navigationContext"></param>
        /// <param name="continuationCallback"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
        {
            bool confirmBack = true;
            MessageBoxResult result = MessageBox.Show("确定切换吗？","温馨提示",MessageBoxButton.YesNo);
            if (result != MessageBoxResult.Yes)
                confirmBack = false;

            continuationCallback?.Invoke(confirmBack);
        }

        /// <summary>
        /// 是否重用实例
        /// </summary>
        /// <param name="navigationContext"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool IsNavigationTarget(NavigationContext navigationContext  )
        {
            return true;
        }

        /// <summary>
        /// 拦截
        /// </summary>
        /// <param name="navigationContext"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }

        /// <summary>
        /// 接收参数
        /// </summary>
        /// <param name="navigationContext"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters.ContainsKey("MsgA"))
                Msg = navigationContext.Parameters.GetValue<string>("MsgA");
        }
    }
}
