using System;
using System.IO;
using NUnit.Framework;
using System.Diagnostics;
using NUnit.Framework.SyntaxHelpers;

namespace cflat.tests
{
	[TestFixture()]
	public class DeclarationsMustUseVar
	{
		[Test()]
		public void Recognizes_global_var_declaration_as_compliant()
		{
			_input = "var \n globalVariable \n = \n 7;";
			RunInputThroughTheGauntlet(null);
		}

		[Test()]
		public void Recognizes_method_var_declaration_as_compliant()
		{
			_input = "class Foo() {\n" +
			         "  public void DoSomething(string moo) {\n" +
			         "    var moreMoo = moo + \"moo\";\n" +
			         "  }\n" +
		             "  private string foo = \"goo\";\n" +
			         "}";
			RunInputThroughTheGauntlet(null);
		}

		[Test()]
		public void Recognizes_global_int_declaration_as_non_compliant()
		{
			_input = "int \n globalVariable \n = \n 7;";
			RunInputThroughTheGauntlet("Must use var for variable declarations!");
		}

		[Test()]
		public void Recognizes_method_string_declaration_as_non_compliant()
		{
			_input = "class Foo() {\n" +
			         "  public void DoSomething(string moo) {\n" +
			         "    string moreMoo = moo + \"moo\";\n" +
			         "  }\n" +
		             "  private string foo = \"goo\";\n" +
			         "}";
			RunInputThroughTheGauntlet("Must use var for variable declarations!");
		}
		
		[SetUp()]
		public void RunBeforeEachTest()
		{
			_input = null;
			_scanner = new Scanner();
			_parser = new Parser(_scanner);
		}
		
		private void RunInputThroughTheGauntlet(string errorMessage)
		{
			string input = _input + new string('\n', 301);
			_scanner.SetSource(input, 0);
			if(errorMessage == null) {
				Assert.That(_parser.Parse());
			} else {
				Assert.That(_parser.Parse(), Is.False);
				Assert.That(_parser.ErrorMessage, Is.EqualTo(errorMessage));
			}
		}
		
		private string _input;
		private Scanner _scanner;
		private Parser _parser;
	}
}

