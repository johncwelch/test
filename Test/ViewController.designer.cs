// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Test
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		AppKit.NSTextField theCurrentComputerName { get; set; }

		[Outlet]
		AppKit.NSTextField theCurrentHostName { get; set; }

		[Outlet]
		AppKit.NSTextField theCurrentLocalHostName { get; set; }

		[Outlet]
		AppKit.NSTextField theNewComputerName { get; set; }

		[Outlet]
		AppKit.NSTextField theNewHostName { get; set; }

		[Outlet]
		AppKit.NSTextField theNewLocalHostName { get; set; }

		[Action ("cancelChanges:")]
		partial void cancelChanges (Foundation.NSObject sender);

		[Action ("saveChanges:")]
		partial void saveChanges (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (theCurrentComputerName != null) {
				theCurrentComputerName.Dispose ();
				theCurrentComputerName = null;
			}

			if (theCurrentLocalHostName != null) {
				theCurrentLocalHostName.Dispose ();
				theCurrentLocalHostName = null;
			}

			if (theCurrentHostName != null) {
				theCurrentHostName.Dispose ();
				theCurrentHostName = null;
			}

			if (theNewComputerName != null) {
				theNewComputerName.Dispose ();
				theNewComputerName = null;
			}

			if (theNewLocalHostName != null) {
				theNewLocalHostName.Dispose ();
				theNewLocalHostName = null;
			}

			if (theNewHostName != null) {
				theNewHostName.Dispose ();
				theNewHostName = null;
			}
		}
	}
}
