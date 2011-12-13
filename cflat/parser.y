%namespace cflat

%union {
	public int integer;
}

%token<integer> NOT_LONG_ENOUGH

%%

program : EOF               { /* Nothing, yet, but all is good. */ }
        | NOT_LONG_ENOUGH   { 
        						ErrorMessage = "File only " + $1 + " lines long.";
        						ErrorCode = 1;
        						YYABORT;
        					}
        ;

%%

internal Parser(Scanner lex) : base(lex) {}

public string ErrorMessage { get; private set; }
public int ErrorCode { get; private set; }
