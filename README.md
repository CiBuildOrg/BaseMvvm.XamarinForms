[![nuget](https://img.shields.io/badge/Nuget-BaseMvvm.XamarinForms-brightgreen.svg?maxAge=259200)](https://www.nuget.org/packages/BaseMvvm.XamarinForms)

# BaseMvvm.XamarinForms
BaseMvvm.XamarinForms


### Using CustomContentPage and CustomContentView
- just change `ContentPage` to `CustomContentPage` or `CustomContentView`

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
        public MainPage(bool navigationBar, object bindingContextData) : base(navigationBar, bindingContextData) //show/hide navigationBar AND sets ViewModel to bindingContext (default ViewModel is BaseViewModel)
        {
            InitializeComponent();
        }
    }
```

### Using CommandProperty with CommandParameter
- we dont need everytime a new ICommand instance we only sets onces then call it with parameter, it allows call different action.
 ```c#
            SetCommand("CustomCmd", CustomCmdMth); //command initializer, first parameter is commandName, second is that actionMethod

            CallCommand("CustomCmd", false); //call command with commandName, second parameter is that manage the `IsBusy` Property for ActivitiyIndicator and also it works async if sets `True`.
```
- also we can determine the custom method for any commandAction (we have one restriction, if we use `CallCommand` in constructure there is no restriction but if we use `CallCommand` in other methods `we can not access xaml properties directly`, so we have to use ViewModel for bridge with binding (you can see in sample project) )
 ```c#
    SetCommand("CustomCmd", CustomCmdMth); 
 
 
      private void CustomCmdMth()
      {
          this.Title ="Page Title" //[RESTRCTION] if we need to use like this, we must call from CTOR otherwise you will get an exception, so we must use GetViewModel<>()
          GetViewModel<BaseViewModel>().Title = "Page Title //[NO RESTRCTION]   //every Page has default ViewModel which names `BaseViewModel`
          
          //it can be works async if set TRUE,  CallCommand("CustomCmd", True);
      }
 ```
 
 - or we can use in xaml, do not forget every CustomContentPage and CustomContentView are already binded with BaseViewModel if you not use custom ViewModel
 ```c#
    //CommandParameter="CustomCmd,true"  first: commandName, second: whether use IsBusy or not
    <Button x:Name="BtnCallCmd" Text="Call Command"  Command="{Binding Commands}" CommandParameter="CustomCmd,true"></Button>
 ```
 
 ### PullToRefresh Feature
 - thanks to `jamesmontemagno` for this feature, i only changed a few codes for implement to my Library, so now it calls `
            command.Execute("OnPullToRefresh");` you do not need to determine a command for this.
  ```c#
    // implementation is same with original library but RefreshCommand only should be "{Binding Commands}", in short this is static value for every CommnandProperty
   <layouts:PullToRefreshLayout
        IsPullToRefreshEnabled="True"
        RefreshCommand="{Binding Commands}"
        IsRefreshing="{Binding IsBusy}"
        RefreshColor="Blue">
        
        </layouts:PullToRefreshLayout>
  ```
  - then we can handling it in code base, only override the `OnPullToRefresh` method, (IsBusy property is auto changing)
    ```c#
     public override void OnPullToRefresh()
     {
            GetViewModel<BaseViewModel>().Title = "pulled and refreshed the page";
            //we dont have any IsBusy changer in this scope, it is automatic
     }
    ```
    
    
 
 
 
