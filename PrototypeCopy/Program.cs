// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

 var obj = new System.Windows.Forms.DataObject(
            DataFormats.Text,
            "This is a test"
        );

await StaHelper.Run(() => Clipboard.SetDataObject( obj, true ));