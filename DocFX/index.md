<center>![Logo](images/esapixLogo.jpg "ESAPIX Logo")</center>

Welcome to the ESAPIX library homepage. Kick start your Eclipse Scripting with a powerful, open source library. It provides:

## Extension Methods
There are several extension methods available for VMS equivalent classes. Check out [ESAPIX.Extensions](https://github.com/rexcardan/ESAPIX/tree/master/ESAPIX/Extensions) namespace to do stuff like this:
```cs
            var plan = patient.Courses
                .SelectMany(c => c.PlanSetups) //Get all plans from all courses
                .FirstOrDefault(p => p.Id.Contains("Prostate")); //Find first that has the work prostate in it
            var prostate = plan.StructureSet.Structures.FirstOrDefault(s => s.Id == "Prostate");
            //Cool. That was easy
            var v50Gy = plan.ExecuteQuery(prostate, "V50Gy[cc]");
```

## Facades
Facades are wrappers around the Varian classes which smooth out rough edges for unit testing and multithreaded access. Best of all, you won't even realize you are using them.
Checkout @facades.md to learn more.

## IScriptContext Interface
IScriptContext is a technique used by ESAPIX to abstract the difference between standalone applications and plugin applications. You can build one app that runs whichever way you want, which is great for developement. One of the challenges with developing plugin applications is the need to debug it. IScriptContext is ESAPIX's solution to the problem. Watch a tutorial on how to use IScriptContext [here](https://www.youtube.com/watch?v=6LXhqgt0jT4).

## Robust DVH Constraint Help
ESAPIX has a lot of code dedicated to helping evaluate DVH metrics. You have the @mayoFormat.md at your disposal to do some complex queries quickly and evaluate the things which are most important.

# Getting Started
## WPF Application
1. Create a new WPF project in Visual Studio. 
2. Right click your project and select **Manage NuGet Packages...**
3. Search for *"ESAPI"* and select Install the most recent ESAPIX library for your install (eg. ESAPIX_13.6 for Aria 13.6)
4. Create two folders in your project : 
	* Views
	* ViewModels
5. **Right click** the *Views* folder and select **Add >> Window**. Call it *MainView*.
6. Copy and paste this into the XAML file, but replace YOURPROJECTNAME with your project name
```cs
<Window x:Class="YOURPROJECTNAME.Views.MainView"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:ESAPIWPF.Views"
        mc:Ignorable="d"
        xmlns:prism="http://prismlibrary.com/"
        prism:ViewModelLocator.AutoWireViewModel="True"
        Title="MainView" Height="300" Width="300">
    <Grid>
        <TextBlock Text="{Binding ESAPITest}" FontSize="25"/>
    </Grid>
</Window>
```
7. **Right click** the *ViewModels* folder and select **Add >> Class**. Call it *MainViewModel*.
8. Copy and paste this into the MainViewModel file, but replace YOURPROJECTNAME with your project name
```cs
using ESAPIX.Facade.API;
using ESAPIX.Interfaces;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using W = System.Windows;

namespace YOURPROJECTNAME.ViewModels
{
    public class MainViewModel : BindableBase
    {
        private string esapiTest;
        private IScriptContext _ctx;

        public string ESAPITest
        {
            get { return esapiTest; }
            set { SetProperty(ref esapiTest, value); }
        }

        public MainViewModel(IScriptContext ctx)
        {
            _ctx = ctx;
            ctx.PatientChanged += Ctx_PatientChanged;
        }

        private void Ctx_PatientChanged(Patient newPatient)
        {
            if (newPatient != null)
            {
                var plans = newPatient.Courses.SelectMany(c => c.PlanSetups).Count();
                ESAPITest = $"{newPatient?.LastName}, {newPatient?.FirstName} | {plans} Plans!";
            }
        }
    }
}

```
9. Open the App.xaml file, and remove the * StartupUri="MainWindow"* at the top.
10. Open up the App.cs file and replace the App class with this:

```cs
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        string USERNAME = "";
        string PASSWORD = "";

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            VMSLink.VMSLinker.Run(); //For facade to link up
            var bs = new ESAPIX.AppKit.AppBootstrapper<MainView>(USERNAME, PASSWORD);
            bs.Run();
        }
    }

```

## Linking Up Facades
So far we haven't actually wired up the Varian classes. You could build and play with your app at this stage, but it won't connect to real Varian data. To link it up, we will create one extra tiny project in the same solution. We are doing this to have a separation of VMS data with application development. This makes it easier to test your application, use in unit tests, and do cool multithreaded stuff.
1. **Right click** your solution file and select **Add >> New Project **
2. Select *Class Library* as the type, and name it **VMS Link** (this doesn't matter except that's what we named it in the code above)
3. Add the NuGet Package ESAPIX_Bootstrapper_{version} by searching again for *"ESAPI"* in **Manage NuGet Packages**
4. **Add a class** called *VMSLinker* to this project and paste this code
```cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMSLink
{
    public class VMSLinker
    {
        public static void Run()
        {
            ESAPIX.Bootstrapper.FacadeInitializer.Initialize();
        }
    }
}

```
5. Right click your original project *References*, select **Add Reference** and click *VMSLink* in the project tab
6. Run your project!

### See Also
*	@facades.md
*	@mayoFormat.md
