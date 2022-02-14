using System;
using System.Diagnostics;

using AppKit;
using Foundation;
using CoreFoundation;

namespace Test
{

	//Application Variables

	public class appProperties
	{
		public static string localHostName = "";
	}

	[Register ("AppDelegate")]
	public class AppDelegate : NSApplicationDelegate
	{

		public AppDelegate ()
		{
		}

		public override void DidFinishLaunching (NSNotification notification)
		{
			//var alert = new NSAlert()
            //{
            //    AlertStyle = NSAlertStyle.Warning,
            //    InformativeText = appProperties.localHostName,
            //    MessageText = "Look at me!",
            //};
            //alert.RunModal();
        }

		public override void WillTerminate (NSNotification notification)
		{
			// Insert code here to tear down your application
		}

        public override bool ApplicationShouldTerminateAfterLastWindowClosed(NSApplication sender)
        {
			Console.WriteLine("quite!");
			return true;
        }
    }
}

