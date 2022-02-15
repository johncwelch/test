using System;
using System.Diagnostics;

using AppKit;
using Foundation;
using CoreGraphics;

namespace Test
{
	public partial class ViewController : NSViewController
	{
		public ViewController (IntPtr handle) : base (handle)
		{
		}

		//function to pull hostnames on app launch. this may need to be moved some day.
		string GetHostNames(string hostNameType)
		{
			Process scutilTest = new Process();
			scutilTest.StartInfo.UseShellExecute = false;
			scutilTest.StartInfo.RedirectStandardOutput = true;
			scutilTest.StartInfo.FileName = "/usr/sbin/scutil";
			scutilTest.StartInfo.Arguments = " --get " + hostNameType;
			scutilTest.Start();

			//appProperties.localHostName = scutilTest.StandardOutput.ReadToEnd();
			string theHostName = scutilTest.StandardOutput.ReadToEnd();
			scutilTest.WaitForExit();

			return theHostName;
			//theCurrentLocalHostName.StringValue = appProperties.localHostName;
		}


		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			//blah
			//blah
			//for strictly local functions, no public/priveat or static/override needed (or allowed),
			//just generic return type and paramater types

			

			//get the current computer names from scutil and populate the current label fields
			theCurrentComputerName.StringValue = GetHostNames("ComputerName");
			theCurrentLocalHostName.StringValue = GetHostNames("LocalHostName");
			theCurrentHostName.StringValue = GetHostNames("HostName");

		}

        



        public override NSObject RepresentedObject {
			get {
				return base.RepresentedObject;
			}
			set {
				base.RepresentedObject = value;
				// Update the view, if already loaded.
			}
		}

        partial void cancelChanges(NSObject sender)
        {
			//Console.WriteLine("clicked");
			theNewComputerName.StringValue = "";
			theNewLocalHostName.StringValue = "";
			theNewHostName.StringValue = "";
		}

        partial void saveChanges(NSObject sender)
        {

			var thePassword = "";

			//use this instead of NSUserName()
			var theUserName = Environment.UserName;
			var newComputerNameText = "";
			var newLocalHostNameText = "";
			var newHostNameText = "";
			var theCommand = "";

			//Vars for NSAppleScript

			//theScriptResult - used for the NSAppleEventDescriptor
			//theAppleScriptError - used for the returned error

			//flags
			var hasPassword = false;

			//cgrect requires core graphics. this may not be the best way to implement this, but it's
			//pretty easy.

			//text field for entering password
			var thePass = new NSSecureTextField(new CGRect(0, 0, 300, 20));

			//building the alert
			var thePassAlert = new NSAlert()
			{
				AlertStyle = NSAlertStyle.Warning,
				InformativeText = "This application needs your password so it can set the new hostname value.",
				MessageText = "Scutil Authentication Request",
			};

			thePassAlert.AddButton("OK");
			thePassAlert.AddButton("Cancel");
			thePassAlert.ShowsSuppressionButton = false;
			//insert the secure text field into the alert
			thePassAlert.AccessoryView = thePass;
			thePassAlert.Layout();
			var thePassAlertResponse = thePassAlert.RunModal();

			switch(thePassAlertResponse)
			{
                case 1000:
					thePassword = thePass.StringValue;
					break;

				case 1001:
					thePass.StringValue = "";
					thePassword = "";
					break;
				default:
					Console.WriteLine("Clicked nothing");
					break;
			}


			//set the has password flag
            if (thePassword != "")
            {
				hasPassword = true;
            } else
            {
				hasPassword = false;
				Console.WriteLine("Password is blank");
            }

            if (hasPassword)
            {
                if (theNewComputerName.StringValue != "")
                {
					//grab the new computer name string value
					newComputerNameText = theNewComputerName.StringValue;
					//build the nsapplescript command
					theCommand = "do shell script \"/usr/sbin/scutil --set ComputerName " + newComputerNameText + "\"" + " user name \"" + theUserName + "\"" + " password \"" + thePassword + "\" with administrator privileges";
					//create the AS command, the "new" keyword is required
					var theScriptCommand = new NSAppleScript(theCommand);
					//run the command
					var theScriptResult = theScriptCommand.ExecuteAndReturnError(out var theAppleScriptError);
					theCommand = "";

					//grab the actual error message here. This is somewhat different than the swift way, but
					//i can't say if it's better or worse, just different. 
					var theErrorValue = theAppleScriptError.ValueForKey((NSString)"NSAppleScriptErrorMessage");

					//you can't use the anyobject trick here so we use the description since that should work
                    if ((theErrorValue.Description).Contains("user name or password was incorrect"))
                    {
						Console.WriteLine("Bad password or user name");
                    } else
                    {
						theCurrentComputerName.StringValue = GetHostNames("ComputerName");
						theCurrentLocalHostName.StringValue = GetHostNames("LocalHostName");
						theCurrentHostName.StringValue = GetHostNames("HostName");
					}
				}

				if (theNewLocalHostName.StringValue != "")
				{
					//grab the new computer name string value
					newLocalHostNameText = theNewLocalHostName.StringValue;
					//build the nsapplescript command
					theCommand = "do shell script \"/usr/sbin/scutil --set ComputerName " + newLocalHostNameText + "\"" + " user name \"" + theUserName + "\"" + " password \"" + thePassword + "\" with administrator privileges";
					//create the AS command, the "new" keyword is required
					var theScriptCommand = new NSAppleScript(theCommand);
					//run the command
					var theScriptResult = theScriptCommand.ExecuteAndReturnError(out var theAppleScriptError);
					theCommand = "";

					//grab the actual error message here. This is somewhat different than the swift way, but
					//i can't say if it's better or worse, just different. 
					var theErrorValue = theAppleScriptError.ValueForKey((NSString)"NSAppleScriptErrorMessage");

					//you can't use the anyobject trick here so we use the description since that should work
					if ((theErrorValue.Description).Contains("user name or password was incorrect"))
					{
						Console.WriteLine("Bad password or user name");
					}
					else
					{
						theCurrentComputerName.StringValue = GetHostNames("ComputerName");
						theCurrentLocalHostName.StringValue = GetHostNames("LocalHostName");
						theCurrentHostName.StringValue = GetHostNames("HostName");
					}
				}

				if (theNewHostName.StringValue != "")
				{
					//grab the new computer name string value
					newHostNameText = theNewHostName.StringValue;
					//build the nsapplescript command
					theCommand = "do shell script \"/usr/sbin/scutil --set ComputerName " + newHostNameText + "\"" + " user name \"" + theUserName + "\"" + " password \"" + thePassword + "\" with administrator privileges";
					//create the AS command, the "new" keyword is required
					var theScriptCommand = new NSAppleScript(theCommand);
					//run the command
					var theScriptResult = theScriptCommand.ExecuteAndReturnError(out var theAppleScriptError);
					theCommand = "";

					//grab the actual error message here. This is somewhat different than the swift way, but
					//i can't say if it's better or worse, just different. 
					var theErrorValue = theAppleScriptError.ValueForKey((NSString)"NSAppleScriptErrorMessage");

					//you can't use the anyobject trick here so we use the description since that should work
					if ((theErrorValue.Description).Contains("user name or password was incorrect"))
					{
						Console.WriteLine("Bad password or user name");
					}
					else
					{
						theCurrentComputerName.StringValue = GetHostNames("ComputerName");
						theCurrentLocalHostName.StringValue = GetHostNames("LocalHostName");
						theCurrentHostName.StringValue = GetHostNames("HostName");
					}
				}


			} else
            {
				//do stuff here
            }



		}


	}
}
 
