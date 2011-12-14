using System;
using System.IO;
using NUnit.Framework;
using System.Diagnostics;
using NUnit.Framework.SyntaxHelpers;

namespace cflat.tests
{
	[TestFixture()]
	public class Needs300LinesTests
	{
		[Test()]
		public void Compiler_does_not_err_with_file_containing_300_newline_chars()
		{
			CreateFileAndRunCompilerOnIt(300);
			Assert.That(exitCode, Is.EqualTo(0));
			Assert.That(stderrContent, Is.EqualTo(""));
		}

		[Test()]
		public void Compiler_errs_with_file_containing_less_than_300_newline_chars()
		{
			CreateFileAndRunCompilerOnIt(299);
			Assert.That(exitCode, Is.Not.EqualTo(0));
			Assert.That(stderrContent.Length, Is.GreaterThan(0));
		}

		[SetUp]
		public void RunBeforeEachTest()
		{
			exitCode = null;
			stderrContent = null;
		}
		
		private void CreateFileAndRunCompilerOnIt(int numberOfNewlineCharacters)
		{
			string sourcePath = Path.GetTempFileName();
			using(StreamWriter source = new StreamWriter(sourcePath)) {
				for(int i = 1; i <= numberOfNewlineCharacters; i += 1) {
					source.WriteLine();
				}
			}
			ProcessStartInfo startInfo = new ProcessStartInfo();
			startInfo.Arguments = sourcePath;
			startInfo.FileName = "cflat.exe";
			startInfo.UseShellExecute = false;
			startInfo.WorkingDirectory = Environment.CurrentDirectory;
			startInfo.RedirectStandardError = true;
			Process compiler = new Process();
			compiler.StartInfo = startInfo;
			compiler.Start();
			compiler.WaitForExit(10000);
			exitCode = compiler.ExitCode;
			stderrContent = compiler.StandardError.ReadToEnd();
		}
		
		private int? exitCode;
		private string stderrContent;
	}
}

