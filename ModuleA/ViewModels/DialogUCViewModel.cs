using Prism.Commands;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleA.ViewModels;
public class DialogUCViewModel : IDialogAware
{
    public string Title{ get; set; } = "对话框标题";

    string P1 { get; set; } = string.Empty;

    string P2 { get; set; } = string.Empty;

    public DelegateCommand ConfirmCom { get; set; }

    public DelegateCommand CancelCom {  get; set; }

    //关闭委托
    public event Action<IDialogResult> RequestClose;

    public DialogUCViewModel()
    {
        ConfirmCom = new (Comfirm);

        CancelCom = new(Cancel);
    }

    private void Cancel()
    {
        RequestClose?.Invoke(new DialogResult(ButtonResult.No));
    }

    private void Comfirm()
    {
        if(RequestClose != null)
        {
            DialogParameters paras = [];
            paras.Add("result1", "返回结果1");
            paras.Add("result2", "返回结果2");

            RequestClose.Invoke(new DialogResult(ButtonResult.Yes,paras));
        }
    }



    /// <summary>
    /// 是否可以关闭对话框
    /// </summary>
    /// <returns></returns>
    public bool CanCloseDialog()
    {
        return true;
    }

    /// <summary>
    /// 关闭方法,把参数传递回去
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    public void OnDialogClosed()
    {
        DialogParameters paras = [];

        paras.Add("key", "你好我是返回的参数");

        RequestClose?.Invoke(new DialogResult(ButtonResult.No,paras));
    }

    /// <summary>
    /// 打开对话框，点击的时候首先执行这个方法，接收参数
    /// </summary>
    /// <param name="parameters"></param>
    /// <exception cref="NotImplementedException"></exception>
    public void OnDialogOpened(IDialogParameters parameters)
    {
        if (parameters.ContainsKey("Title"))
            Title = parameters.GetValue<string>("Title");
        P1 = parameters.GetValue<string>("para1");
        P2 = parameters.GetValue<string>("para2");
    }
}
