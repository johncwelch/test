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

		

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			//blah
			//blah
			//for strictly local functions, no public/priveat or static/override needed (or allowed),
			//just generic return type and paramater types

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
					var alert = new NSAlert()
                    {
                        AlertStyle = NSAlertStyle.Warning,
                        InformativeText = thePass.StringValue,
                        MessageText = "Look at me!",
                    };
                    alert.RunModal();
			}

			//grabs the password from the alert's text field
			string thePassWord = thePass.StringValue;

            //var alert = new NSAlert()
            //{
            //    AlertStyle = NSAlertStyle.Warning,
            //    InformativeText = thePass.StringValue,
            //    MessageText = "Look at me!",
            //};
            //alert.RunModal();

		}


	}
}
 