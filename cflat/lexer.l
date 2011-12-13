%namespace cflat

%{
private int totalLines;
%}

%%

\n			{ totalLines += 1; }
<<EOF>>		{
				yylval.integer = totalLines;
				if(totalLines < 300) return (int) Tokens.NOT_LONG_ENOUGH;
				else return (int) Tokens.EOF;
			}