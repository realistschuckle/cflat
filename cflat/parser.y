%namespace cflat

%union {
    public int integer;
    public string str;
}

%token<integer> NOT_LONG_ENOUGH
%token<str> IDENTIFIER
%token VISIBILITY_MODIFIER

%%

program : statements EOF               { /* Nothing, yet, but all is good. */ }
        | statements NOT_LONG_ENOUGH
          { 
              ErrorMessage = "File only " + $1 + " lines long.";
              ErrorCode = 1;
              YYABORT;
          }
        ;

statements :
           | identifiers
           ;

identifiers : identifier
            | identifiers identifier
            ;

identifier : VISIBILITY_MODIFIER IDENTIFIER IDENTIFIER '='  { /* This is ok. */ }
           | IDENTIFIER IDENTIFIER '='
             {
                 if($1 != "var") {
                     ErrorMessage = "Must use var for variable declarations!";
                     ErrorCode = 2;
                     YYABORT;
                 }
             }
           ;

%%

public Parser(Scanner lex) : base(lex) {}
public string ErrorMessage { get; private set; }
public int ErrorCode { get; private set; }
