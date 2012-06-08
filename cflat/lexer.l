%namespace cflat

VISIBILITY    (public|protected|internal|protected\s+internal|private)
IDENTIFIER    [a-zA-Z_][a-zA-Z0-9_]*

%{
private int totalLines = 0;
%}

%%

{VISIBILITY}    { return T(Tokens.VISIBILITY_MODIFIER); }
{IDENTIFIER}    { yylval.str = yytext; return T(Tokens.IDENTIFIER); }
=               { return C(); }

\n              { totalLines += 1; }
<<EOF>>         {
                  yylval.integer = totalLines;
                  if(totalLines < 300) return (int) Tokens.NOT_LONG_ENOUGH;
                  else return (int) Tokens.EOF;
                }

.               {}

%%

private int T(Tokens token) {
	Console.WriteLine(yytext + ": " + token);
	return (int) token;
}

private int C() {
	Console.WriteLine(yytext + ": char");
	return (int) yytext[0];
}