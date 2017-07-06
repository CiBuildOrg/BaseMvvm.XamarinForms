[![nuget](https://img.shields.io/badge/Nuget-BaseMvvm.XamarinForms-brightgreen.svg?maxAge=259200)](https://www.nuget.org/packages/BaseMvvm.XamarinForms)

# BaseMvvm.XamarinForms
BaseMvvm.XamarinForms all-in-one easy mvvm implementation


### Using CustomContentPage and CustomContentView
- just change **ContentPage** to **CustomContentPage** or **CustomContentView**

```c#

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class MainPage : CustomContentPage // or CustomContentView
{
    public MainPage() : base() 
    {
        InitializeComponent();
    }
    //or
    public MainPage(bool navigationBar) : base(navigationBar) // show/hide navigationBar
    {
        InitializeComponent();
    }
    //or
    //show/hide navigationBar AND sets ViewModel to bindingContext (default ViewModel is BaseViewModel)
    public MainPage(bool navigationBar, object bindingContextData) : base(navigationBar, bindingContextData)
    {
        InitializeComponent();
    }
}
```
- and xaml side
 ```c#
 <views:CustomContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:local="clr-namespace:SampleApp"
             xmlns:views="clr-namespace:BaseMvvm.XamarinForms.Views;assembly=BaseMvvm.XamarinForms"
             x:Class="SampleApp.Views.MainPage">
  <views:CustomContentPage.Content>
    //stacklayout etc..
  </views:CustomContentPage.Content>
</views:CustomContentPage>
 ```


### Using CommandProperty with CommandParameter
- we dont need everytime a new ICommand instance we only sets onces then call it with parameter, it allows call different action.
 ```c#
//command initializer, first parameter is commandName, second is that actionMethod
SetCommand("CustomCmd", CustomCmdMth); 

//call command with commandName, second parameter is that manage the `IsBusy` 
//Property for ActivitiyIndicator and also it works async if sets `True`.
CallCommand("CustomCmd", false);
```
- also we can determine the custom method for any commandAction (we have one restriction, if we use **CallCommand** in constructure there is no restriction but if we use **CallCommand** in other methods **we can not access xaml properties directly**, so we have to use ViewModel for bridge with binding (you can see in sample project) )
 ```c#
    SetCommand("CustomCmd", CustomCmdMth); 
 
 
private void CustomCmdMth()
{
this.Title ="Page Title" //[RESTRCTION] if we need to use like this, 
                         //we must call from CTOR otherwise you will 
                         //get an exception, so we must use GetViewModel<>()

GetViewModel<BaseViewModel>().Title = "Page Title"; //[NO RESTRCTION]
                                                    //every Page has default ViewModel 
                                                    //which names `BaseViewModel`

//it can be works async if set TRUE,  CallCommand("CustomCmd", True);
}
 ```
 
 - or we can use in xaml, do not forget every CustomContentPage and CustomContentView are already binded with BaseViewModel if you not use custom ViewModel
 ```c#
 //CommandParameter="CustomCmd,true"  first: commandName, second: whether use IsBusy or not
 <Button x:Name="BtnCallCmd" 
        Text="Call Command" 
        Command="{Binding Commands}" 
        CommandParameter="CustomCmd,true">
</Button>
 ```
 
 
 ### PullToRefresh Feature
 - thanks to **jamesmontemagno** for this feature, i only changed a few codes for implement to my Library,so now it calls `command.Execute("OnPullToRefresh");` you do not need to determine a command for this.
  ```c#
// implementation is same with original library but RefreshCommand only should be "{Binding Commands}", 
// in short this is static value for every CommnandProperty
   <layouts:PullToRefreshLayout
        IsPullToRefreshEnabled="True"
        RefreshCommand="{Binding Commands}"
        IsRefreshing="{Binding IsBusy}"
        RefreshColor="Blue">
        //stacklayout etc..
</layouts:PullToRefreshLayout>
  ```
  - then we can handling it in code base, only override **the OnPullToRefresh** method,(IsBusy property is auto changing)
```c#
 public override void OnPullToRefresh()
 {
        GetViewModel<BaseViewModel>().Title = "pulled and refreshed the page";
        //we dont have any IsBusy changer in this scope, it is automatic
 }
```
    
  ### MvvmMessagingCenter
  - extend of MessagingCenter for **the BaseMvvm.XamarinForms**, it must be use for using below methods.
  
   #### Init()
   - this method initializer for **the ICustomLayout** `(CustomContentPage and CustomContentView)` and also you don not need to use this method (it works automatically).
   
   #### role of OnAppearing()
   - this method controls the CurrentPage which derived from CustomContentPage or CustomContentView, so it works when created a new instance of ICustomLayout or changed the display page (same as xamarin.forms.dll) therefore it changes the current page for MessagingCenter senders. (it works automatically no need to override)
   
   #### SubcribeIncomingEvent()
   - same as `MessagingCenter.Subcribe()` but little bit changed version, it can be use for every event **except ThrowingException** we can use it for different handler. (in short subcriber of **OnIncomingEvents()** method)
```c#
//most common determining place is CTOR
//this:> subcriber page (it will be change every page action, so it is dynamic)
//string:> name of Message (same as MessagingCenter)
MvvmMessagingCenter.SubcribeIncomingEvent(this, "testMessage");
```

   #### SendIncomingEvent()
   - send event data to Page which is derived from ICustomLayout
```c#
MvvmMessagingCenter.SendIncomingEvent(this, "testMessage", new { userName = "mustafa" });
```

   ##### About MvvmMessagingCenterEventArgs
   - **MessageId Property** is same as Message of Sender
   - **Event Property** object type variable
   - **Cast<>() Method** caster of **Event Property**
   
   #### OnIncomingEvents()
   - handler of **SendIncomingEvent()**
```c#
public override void OnIncomingEvents(ICustomLayout sender, MvvmMessagingCenterEventArgs args)
{
object obj = args.Cast<object>(); //custom caster, whatevery you want, you can cast
DisplayAlert("exception", args.MessageId + " " + obj.ToString(), "OK");
```
   
   
   #### SendException()
   - send exception to currentPage or differentPage which is derived from ICustomLayout
   ```c#            
try
{
    throw new Exception("redirected custom exception to another page!!!");
}
catch (Exception exception)
{
    //subcriber is auto change to currentPage
    MvvmMessagingCenter.SendException(this, exception); 
    
    //also you can send this error to different page
    await Navigation.PushAsync(new ExceptionPage());
}
   ```
 
   #### OnException()
   - handler of **SendException()**
```c#     
public override void OnException(object sender, Exception exception)
{
    DisplayAlert("exception", exception.Message, "OK");
}
```
