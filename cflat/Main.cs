using System;
using System.IO;

namespace cflat
{
	class MainClass
	{
		public static int Main (string[] args)
		{
			if (args.Length == 0) {
				Console.Error.WriteLine ("Specify a file, dude.");
				return 1;
			}
			using (Stream input = File.Open(args[0], FileMode.Open)) {
				Scanner scanner = new Scanner (input);
				Parser parser = new Parser (scanner);
				if (!parser.Parse ()) {
					Console.Error.WriteLine (parser.ErrorMessage);
					return parser.ErrorCode;
				}
			}
			return 0;
		}
	}
}
