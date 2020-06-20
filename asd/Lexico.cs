/*
 * Created by SharpDevelop.
 * User: Usuario
 * Date: 22/01/2019
 * Time: 09:27 a. m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace asd
{
	/// <summary>
	/// Description of Lexico.
	/// </summary>
	public class Lexico
	{
		// disable once FieldCanBeMadeReadOnly.Local
		string  word;
		int Posicion;

		public Lexico(string word){
			this.word = word.ToLower();
		}
		
		public string validate(ref int Inicio){
			string tipo;
			Posicion = Inicio;
			
			tipo = "Sin Coincidencias";
			
			if(itsLeftCorchete()){ // Corchete de apertura
				tipo = "[";
			}else if(itsRightCorchete()){ // Corchete de cerradura
				tipo = "]";
			}else if(itsLeftBracket()){ // {
				tipo = "{";
			}else if(itsRightBracket()){ // }
				tipo = "}";
			}else if(itsLeftParenthesis()){ // Parentesis abertura
				tipo = "(";
			}else if(itsRightParenthesis()){ // Parentecis cerradura
				tipo = ")";
			}else if(itsPlus()){ // Suma
				tipo = "+";
			}else if(itsSub()){ // Resta
				tipo = "-";
			}else if(itsAsterisk()){ // Multiplicacion
				tipo = "*";
			}else if(itsDivision()){ // Division
				tipo = "/";
			}else if(itsMod()){ // Mod
				tipo = "%";
			}else if(itsDot()){ // Punto y Coma
				tipo = ";";
			}else if(itsTwoPoints()){ // Dos Puntos
				tipo = ":";
			}else if(itsExclamation()){ // Exclamacion
				tipo = "!";
			}else if(itsSingleChar()){ // Comilla Simple
				tipo = "'";
			}else if(itsIn()){ // Entrada de datos
				tipo = ">>";
			}else if(itsOut()){ // Salida de datos
				tipo = "<<";
			}else if(itsGreaterThan()){ // Greater
				tipo = ">";
			}else if(itsSmallerThan()){ // Smaller
				tipo = "<";
			}else if(itsIf()){ // IF
				tipo = "IF";
			}else if(itsAnd()){ // && AND
				tipo = "&&";
			}else if(itsOr()){ // || OR
				tipo = "||";
			}else if(itsGreaterEqual()){ // Mayor o Igual
				tipo = ">=";
			}else if(itsSmallerEqual()){ // Menor o Igual
				tipo = "<=";
			}else if(itsEqualEqual()){ // Igual Comprarcion
				tipo = "==";
			}else if(itsEqual()){ // Igual Asignacion
				tipo = "=";
			}else if(itsNotEqual()){ // Diferente compracion
				tipo = "!=";
			}else if(itsInt()){ // INT - Entero
				tipo = "INT";
			}else if(itsFor()){ // FOR
				tipo = "FOR";
			}else if(itsElse()){ // ELSE
				tipo = "ELSE";
			}else if(itsCase()){ // CASE
				tipo = "CASE";
			}else if(itsChar()){ // CHAR
				tipo = "CHAR";
			}else if(itsBreak()){ // BREAK
				tipo = "BREAK";
			}else if(itsWhile()){ // WHILE
				tipo = "WHILE";
			}else if(itsSwitch()){ // SWITCH
				tipo = "SWITCH";
			}else if(itsString()){ // STRING
				tipo = "STRING";
			}else if(itsDefault()){ // DEFAULT
				tipo = "DEFAULT";
			}else if(itsChain()){ // Cadena
				tipo = "C";
			}else if(itsNumber()){ // Numero    ------> SIEMPRE AL ULTIMO DE VALIDAR
				tipo = "N";
			}else if(itsWord()){ // Variabler    ------> SIEMPRE AL ULTIMO DE VALIDAR (ESTE SE TOMA COMO CARCTER)
				tipo = "V";
			}else if (itsSpace()){ // Espacios en blaco
				tipo = "SP";
			}else{
				tipo = "Error";
			}
			
			if(tipo != "Error"){
				Inicio = Posicion;
			}
			
			return tipo;
		}
		
		// ************************************************  GRAMATICA VALIDADA *************************************************************
		
		// 1 CARACTER
		
		Boolean itsLeftCorchete(){
			Boolean Pertenece;
			int Actual = Posicion;
			
			Pertenece = false;
			
			do {
				if (word[Actual] == '[') {
					Actual++;
					Pertenece = true;
					break;
				}

				if (Actual >= word.Length || (word[Actual] != ' ' && word[Actual] != '\n' && word[Actual] != '\r')) {
					Pertenece = false;
					break;
				}
				
				Actual++;
				
			}while(Actual < word.Length);
			
			if(Pertenece){
				Posicion = Actual;
			}
			
			return Pertenece;
		}
		
		Boolean itsTwoPoints(){
			Boolean Pertenece;
			int Actual = Posicion;
			
			Pertenece = false;
			
			do {
				if (word[Actual] == ':') {
					Actual++;
					Pertenece = true;
					break;
				}

				if (Actual >= word.Length || (word[Actual] != ' ' && word[Actual] != '\n' && word[Actual] != '\r')) {
					Pertenece = false;
					break;
				}
				
				Actual++;
				
			}while(Actual < word.Length);
			
			if(Pertenece){
				Posicion = Actual;
			}
			
			return Pertenece;
		}
		
		Boolean itsRightCorchete(){
			Boolean Pertenece;
			int Actual = Posicion;
			
			Pertenece = false;
			
			do {
				if (word[Actual] == ']') {
					Actual++;
					Pertenece = true;
					break;
				}

				if (Actual >= word.Length || (word[Actual] != ' ' && word[Actual] != '\n' && word[Actual] != '\r')) {
					Pertenece = false;
					break;
				}
				
				Actual++;
				
			}while(Actual < word.Length);
			
			if(Pertenece){
				Posicion = Actual;
			}
			
			return Pertenece;
		}
		
		Boolean itsLeftParenthesis(){
			Boolean Pertenece;
			int Actual = Posicion;
			
			Pertenece = false;
			
			do {
				if (word[Actual] == '(') {
					Actual++;
					Pertenece = true;
					break;
				}

				if (Actual >= word.Length || (word[Actual] != ' ' && word[Actual] != '\n' && word[Actual] != '\r')) {
					Pertenece = false;
					break;
				}
				
				Actual++;
				
			}while(Actual < word.Length);
			
			if(Pertenece){
				Posicion = Actual;
			}
			
			return Pertenece;
		}
		
		Boolean itsRightParenthesis(){
			Boolean Pertenece;
			int Actual = Posicion;
			
			Pertenece = false;
			
			do {
				if (word[Actual] == ')') {
					Actual++;
					Pertenece = true;
					break;
				}

				if (Actual >= word.Length || (word[Actual] != ' ' && word[Actual] != '\n' && word[Actual] != '\r')) {
					Pertenece = false;
					break;
				}
				
				Actual++;
				
			}while(Actual < word.Length);
			
			if(Pertenece){
				Posicion = Actual;
			}
			
			return Pertenece;
		}
		
		Boolean itsLeftBracket(){
			Boolean Pertenece;
			int Actual = Posicion;
			
			Pertenece = false;
			
			do {
				if (word[Actual] == '{') {
					Actual++;
					Pertenece = true;
					break;
				}

				if (Actual >= word.Length || (word[Actual] != ' ' && word[Actual] != '\n' && word[Actual] != '\r')) {
					Pertenece = false;
					break;
				}
				
				Actual++;
				
			}while(Actual < word.Length);
			
			if(Pertenece){
				Posicion = Actual;
			}
			
			return Pertenece;
		} 
		
		Boolean itsRightBracket(){
			Boolean Pertenece;
			int Actual = Posicion;
			
			Pertenece = false;
			
			do {
				if (word[Actual] == '}') {
					Actual++;
					Pertenece = true;
					break;
				}

				if (Actual >= word.Length || (word[Actual] != ' ' && word[Actual] != '\n' && word[Actual] != '\r')) {
					Pertenece = false;
					break;
				}
				
				Actual++;
				
			}while(Actual < word.Length);
			
			if(Pertenece){
				Posicion = Actual;
			}
			
			return Pertenece;
		} 
		
		Boolean itsPlus(){
			Boolean Pertenece;
			int Actual = Posicion;
			
			Pertenece = false;
			
			do {
				if (word[Actual] == '+') {
					Actual++;
					Pertenece = true;
					break;
				}

				if (Actual >= word.Length || (word[Actual] != ' ' && word[Actual] != '\n' && word[Actual] != '\r')) {
					Pertenece = false;
					break;
				}
				
				Actual++;
				
			}while(Actual < word.Length);
			
			if(Pertenece){
				Posicion = Actual;
			}
			
			return Pertenece;
		}
		
		Boolean itsSub(){
			Boolean Pertenece;
			int Actual = Posicion;
			
			Pertenece = false;
			
			do {
				if (word[Actual] == '-') {
					Actual++;
					Pertenece = true;
					break;
				}

				if (Actual >= word.Length || (word[Actual] != ' ' && word[Actual] != '\n' && word[Actual] != '\r')) {
					Pertenece = false;
					break;
				}
				
				Actual++;
				
			}while(Actual < word.Length);
			
			if(Pertenece){
				Posicion = Actual;
			}
			
			return Pertenece;
		}
		
		Boolean itsDivision(){
			Boolean Pertenece;
			int Actual = Posicion;
			
			Pertenece = false;
			
			do {
				if (word[Actual] == '/') {
					Actual++;
					Pertenece = true;
					break;
				}

				if (Actual >= word.Length || (word[Actual] != ' ' && word[Actual] != '\n' && word[Actual] != '\r')) {
					Pertenece = false;
					break;
				}
				
				Actual++;
				
			}while(Actual < word.Length);
			
			if(Pertenece){
				Posicion = Actual;
			}
			
			return Pertenece;
		}
		
		Boolean itsAsterisk(){
			Boolean Pertenece;
			int Actual = Posicion;
			
			Pertenece = false;
			
			do {
				if (word[Actual] == '*') {
					Actual++;
					Pertenece = true;
					break;
				}

				if (Actual >= word.Length || (word[Actual] != ' ' && word[Actual] != '\n' && word[Actual] != '\r')) {
					Pertenece = false;
					break;
				}
				
				Actual++;
				
			}while(Actual < word.Length);
			
			if(Pertenece){
				Posicion = Actual;
			}
			
			return Pertenece;
		}
		
		Boolean itsMod(){
			Boolean Pertenece;
			int Actual = Posicion;
			
			Pertenece = false;
			
			do {
				if (word[Actual] == '%') {
					Actual++;
					Pertenece = true;
					break;
				}

				if (Actual >= word.Length || (word[Actual] != ' ' && word[Actual] != '\n' && word[Actual] != '\r')) {
					Pertenece = false;
					break;
				}
				
				Actual++;
				
			}while(Actual < word.Length);
			
			if(Pertenece){
				Posicion = Actual;
			}
			
			return Pertenece;
		}
		
		Boolean itsEqual(){
			Boolean Pertenece;
			int Actual = Posicion;
			
			Pertenece = false;
			
			do {
				if (word[Actual] == '=') {
					Actual++;
					Pertenece = true;
					break;
				}

				if (Actual >= word.Length || (word[Actual] != ' ' && word[Actual] != '\n' && word[Actual] != '\r')) {
					Pertenece = false;
					break;
				}
				
				Actual++;
				
			}while(Actual < word.Length);
			
			if(Pertenece){
				Posicion = Actual;
			}
			
			return Pertenece;
		} 
		
		Boolean itsGreaterThan(){
			Boolean Pertenece;
			int Actual = Posicion;
			
			Pertenece = false;
			
			do {
				if (word[Actual] == '>') {
					Actual++;
					Pertenece = true;
					break;
				}

				if (Actual >= word.Length || (word[Actual] != ' ' && word[Actual] != '\n' && word[Actual] != '\r')) {
					Pertenece = false;
					break;
				}
				
				Actual++;
				
			}while(Actual < word.Length);
			
			if(Pertenece){
				Posicion = Actual;
			}
			
			return Pertenece;
		} // ADD
		
		Boolean itsSmallerThan(){
			Boolean Pertenece;
			int Actual = Posicion;
			
			Pertenece = false;
			
			do {
				if (word[Actual] == '<') {
					Actual++;
					Pertenece = true;
					break;
				}

				if (Actual >= word.Length || (word[Actual] != ' ' && word[Actual] != '\n' && word[Actual] != '\r')) {
					Pertenece = false;
					break;
				}
				
				Actual++;
				
			}while(Actual < word.Length);
			
			if(Pertenece){
				Posicion = Actual;
			}
			
			return Pertenece;
		} // ADD
		
		Boolean itsDot(){
			Boolean Pertenece;
			int Actual = Posicion;
			
			Pertenece = false;
			
			do {
				if (word[Actual] == ';') {
					Actual++;
					Pertenece = true;
					break;
				}

				if (Actual >= word.Length || (word[Actual] != ' ' && word[Actual] != '\n' && word[Actual] != '\r')) {
					Pertenece = false;
					break;
				}
				
				Actual++;
				
			}while(Actual < word.Length);
			
			if(Pertenece){
				Posicion = Actual;
			}
			
			return Pertenece;
		}
		
		Boolean itsExclamation(){
			Boolean Pertenece;
			int Actual = Posicion;
			
			Pertenece = false;
			
			do {
				if (word[Actual] == '!') {
					Actual++;
					Pertenece = true;
					break;
				}

				if (Actual >= word.Length || (word[Actual] != ' ' && word[Actual] != '\n' && word[Actual] != '\r')) {
					Pertenece = false;
					break;
				}
				
				Actual++;
				
			}while(Actual < word.Length);
			
			if(Pertenece){
				Posicion = Actual;
			}
			
			return Pertenece;
		}
		
		Boolean itsSingleChar(){
			Boolean Pertenece;
			int Actual = Posicion;
			
			Pertenece = false;
			
			do {
				if (word[Actual] == '\'') {
					Actual++;
					Pertenece = true;
					break;
				}

				if (Actual >= word.Length || (word[Actual] != ' ' && word[Actual] != '\n' && word[Actual] != '\r')) {
					Pertenece = false;
					break;
				}
				
				Actual++;
				
			}while(Actual < word.Length);
			
			if(Pertenece){
				Posicion = Actual;
			}
			
			return Pertenece;
		}
		
		// 2 CARFACTERES
		
		Boolean itsOut(){
			Boolean Pertenece;
			int Actual = Posicion;
			
			Pertenece = false;
			
			do {
				if (word[Actual] == '<') {
					if(word[Actual+1] == '<'){
						Actual+=2;
						Pertenece = true;
						break;
					}
				}

				if (Actual >= word.Length || (word[Actual] != ' ' && word[Actual] != '\n' && word[Actual] != '\r')) {
					Pertenece = false;
					break;
				}
				
				Actual++;
				
			}while(Actual < word.Length);
			
			if(Pertenece){
				Posicion = Actual;
			}
			
			return Pertenece;
		}
		
		Boolean itsIn(){
			Boolean Pertenece;
			int Actual = Posicion;
			
			Pertenece = false;
			
			do {
				if (word[Actual] == '>') {
					if(word[Actual+1] == '>'){
						Actual+=2;
						Pertenece = true;
						break;
					}
				}

				if (Actual >= word.Length || (word[Actual] != ' ' && word[Actual] != '\n' && word[Actual] != '\r')) {
					Pertenece = false;
					break;
				}
				
				Actual++;
				
			}while(Actual < word.Length);
			
			if(Pertenece){
				Posicion = Actual;
			}
			
			return Pertenece;
		}
		
		Boolean itsIf(){
			Boolean Pertenece;
			int Actual = Posicion;
			
			Pertenece = false;
			
			do {
				if (word[Actual] == 'i') {
					if(word[Actual+1] == 'f'){
						Actual+=2;
						Pertenece = true;
						break;
					}
				}

				if (Actual >= word.Length || (word[Actual] != ' ' && word[Actual] != '\n' && word[Actual] != '\r')) {
					Pertenece = false;
					break;
				}
				
				Actual++;
				
			}while(Actual < word.Length);
			
			if(Pertenece){
				Posicion = Actual;
			}
			
			return Pertenece;
		}
		
		Boolean itsAnd(){
			Boolean Pertenece;
			int Actual = Posicion;
			
			Pertenece = false;
			
			do {
				if (word[Actual] == '&') {
					if(word[Actual+1] == '&'){
						Actual+=2;
						Pertenece = true;
						break;
					}
				}

				if (Actual >= word.Length || (word[Actual] != ' ' && word[Actual] != '\n' && word[Actual] != '\r')) {
					Pertenece = false;
					break;
				}
				
				Actual++;
				
			}while(Actual < word.Length);
			
			if(Pertenece){
				Posicion = Actual;
			}
			
			return Pertenece;
		}
		
		Boolean itsOr(){
			Boolean Pertenece;
			int Actual = Posicion;
			
			Pertenece = false;
			
			do {
				if (word[Actual] == '|') {
					if(word[Actual+1] == '|'){
						Actual+=2;
						Pertenece = true;
						break;
					}
				}

				if (Actual >= word.Length || (word[Actual] != ' ' && word[Actual] != '\n' && word[Actual] != '\r')) {
					Pertenece = false;
					break;
				}
				
				Actual++;
				
			}while(Actual < word.Length);
			
			if(Pertenece){
				Posicion = Actual;
			}
			
			return Pertenece;
		}
		
		Boolean itsGreaterEqual(){
			Boolean Pertenece;
			int Actual = Posicion;
			
			Pertenece = false;
			
			do {
				if (word[Actual] == '>') {
					if(word[Actual+1] == '='){
						Actual+=2;
						Pertenece = true;
						break;
					}
				}

				if (Actual >= word.Length || (word[Actual] != ' ' && word[Actual] != '\n' && word[Actual] != '\r')) {
					Pertenece = false;
					break;
				}
				
				Actual++;
				
			}while(Actual < word.Length);
			
			if(Pertenece){
				Posicion = Actual;
			}
			
			return Pertenece;
		}
		
		Boolean itsSmallerEqual(){
			Boolean Pertenece;
			int Actual = Posicion;
			
			Pertenece = false;
			
			do {
				if (word[Actual] == '<') {
					if(word[Actual+1] == '='){
						Actual+=2;
						Pertenece = true;
						break;
					}
				}

				if (Actual >= word.Length || (word[Actual] != ' ' && word[Actual] != '\n' && word[Actual] != '\r')) {
					Pertenece = false;
					break;
				}
				
				Actual++;
				
			}while(Actual < word.Length);
			
			if(Pertenece){
				Posicion = Actual;
			}
			
			return Pertenece;
		}
		
		Boolean itsEqualEqual(){
			Boolean Pertenece;
			int Actual = Posicion;
			
			Pertenece = false;
			
			do {
				if (word[Actual] == '=') {
					if(word[Actual+1] == '='){
						Actual+=2;
						Pertenece = true;
						break;
					}
				}

				if (Actual >= word.Length || (word[Actual] != ' ' && word[Actual] != '\n' && word[Actual] != '\r')) {
					Pertenece = false;
					break;
				}
				
				Actual++;
				
			}while(Actual < word.Length);
			
			if(Pertenece){
				Posicion = Actual;
			}
			
			return Pertenece;
		}
		
		Boolean itsNotEqual(){
			Boolean Pertenece;
			int Actual = Posicion;
			
			Pertenece = false;
			
			do {
				if (word[Actual] == '!') {
					if(word[Actual+1] == '='){
						Actual+=2;
						Pertenece = true;
						break;
					}
				}

				if (Actual >= word.Length || (word[Actual] != ' ' && word[Actual] != '\n' && word[Actual] != '\r')) {
					Pertenece = false;
					break;
				}
				
				Actual++;
				
			}while(Actual < word.Length);
			
			if(Pertenece){
				Posicion = Actual;
			}
			
			return Pertenece;
		}
		
		// 3 CARACTERES
		
		Boolean itsInt(){
			Boolean Pertenece;
			int Actual = Posicion;
			
			Pertenece = false;
			
			do {
				if (word[Actual] == 'i') {
					if(word[Actual+1] == 'n'){
						if(word[Actual+2] == 't'){
							Actual+=3;
							Pertenece = true;
							break;
						}
					}
				}

				if (Actual >= word.Length || (word[Actual] != ' ' && word[Actual] != '\n' && word[Actual] != '\r')) {
					Pertenece = false;
					break;
				}
				
				Actual++;
				
			}while(Actual < word.Length);
			
			if(Pertenece){
				Posicion = Actual;
			}
			
			return Pertenece;
		}
		
		Boolean itsFor(){
			Boolean Pertenece;
			int Actual = Posicion;
			
			Pertenece = false;
			
			do {
				if (word[Actual] == 'f') {
					if(word[Actual+1] == 'o'){
						if(word[Actual+2] == 'r'){
							Actual+=3;
							Pertenece = true;
							break;
						}
					}
				}

				if (Actual >= word.Length || (word[Actual] != ' ' && word[Actual] != '\n' && word[Actual] != '\r')) {
					Pertenece = false;
					break;
				}
				
				Actual++;
				
			}while(Actual < word.Length);
			
			if(Pertenece){
				Posicion = Actual;
			}
			
			return Pertenece;
		}
		
		// 4 CARACTERES
		
		Boolean itsElse(){
			Boolean Pertenece;
			int Actual = Posicion;
			
			Pertenece = false;
			
			do {
				if (word[Actual] == 'e') {
					if(word[Actual+1] == 'l'){
						if(word[Actual+2] == 's'){
							if(word[Actual+3] == 'e'){
								Actual+=4;
								Pertenece = true;
								break;
							}
						}
					}
				}

				if (Actual >= word.Length || (word[Actual] != ' ' && word[Actual] != '\n' && word[Actual] != '\r')) {
					Pertenece = false;
					break;
				}
				
				Actual++;
				
			}while(Actual < word.Length);
			
			if(Pertenece){
				Posicion = Actual;
			}
			
			return Pertenece;
		}
		
		Boolean itsCase(){
			Boolean Pertenece;
			int Actual = Posicion;
			
			Pertenece = false;
			
			do {
				if (word[Actual] == 'c') {
					if(word[Actual+1] == 'a'){
						if(word[Actual+2] == 's'){
							if(word[Actual+3] == 'e'){
								Actual+=4;
								Pertenece = true;
								break;
							}
						}
					}
				}

				if (Actual >= word.Length || (word[Actual] != ' ' && word[Actual] != '\n' && word[Actual] != '\r')) {
					Pertenece = false;
					break;
				}
				
				Actual++;
				
			}while(Actual < word.Length);
			
			if(Pertenece){
				Posicion = Actual;
			}
			
			return Pertenece;
		}
		
		Boolean itsChar(){
			Boolean Pertenece;
			int Actual = Posicion;
			
			Pertenece = false;
			
			do {
				if (word[Actual] == 'c') {
					if(word[Actual+1] == 'h'){
						if(word[Actual+2] == 'a'){
							if(word[Actual+3] == 'r'){
								Actual+=4;
								Pertenece = true;
								break;
							}
						}
					}
				}

				if (Actual >= word.Length || (word[Actual] != ' ' && word[Actual] != '\n' && word[Actual] != '\r')) {
					Pertenece = false;
					break;
				}
				
				Actual++;
				
			}while(Actual < word.Length);
			
			if(Pertenece){
				Posicion = Actual;
			}
			
			return Pertenece;
		}
		
		// 5 CARACTERES
		
		Boolean itsBreak(){
			Boolean Pertenece;
			int Actual = Posicion;
			
			Pertenece = false;
			
			do {
				if (word[Actual] == 'b') {
					if(word[Actual+1] == 'r'){
						if(word[Actual+2] == 'e'){
							if(word[Actual+3] == 'a'){
								if(word[Actual+4] == 'k'){
									Actual+=5;
									Pertenece = true;
									break;
								}
							}
						}
					}
				}

				if (Actual >= word.Length || (word[Actual] != ' ' && word[Actual] != '\n' && word[Actual] != '\r')) {
					Pertenece = false;
					break;
				}
				
				Actual++;
				
			}while(Actual < word.Length);
			
			if(Pertenece){
				Posicion = Actual;
			}
			
			return Pertenece;
		}
		
		Boolean itsWhile(){
			Boolean Pertenece;
			int Actual = Posicion;
			
			Pertenece = false;
			
			do {
				if (word[Actual] == 'w') {
					if(word[Actual+1] == 'h'){
						if(word[Actual+2] == 'i'){
							if(word[Actual+3] == 'l'){
								if(word[Actual+4] == 'e'){
									Actual+=5;
									Pertenece = true;
									break;
								}
							}
						}
					}
				}

				if (Actual >= word.Length || (word[Actual] != ' ' && word[Actual] != '\n' && word[Actual] != '\r')) {
					Pertenece = false;
					break;
				}
				
				Actual++;
				
			}while(Actual < word.Length);
			
			if(Pertenece){
				Posicion = Actual;
			}
			
			return Pertenece;
		}
		
		// 6 CARACTESRES
		
		Boolean itsSwitch(){
			Boolean Pertenece;
			int Actual = Posicion;
			
			Pertenece = false;
			
			do {
				if (word[Actual] == 's') {
					if(word[Actual+1] == 'w'){
						if(word[Actual+2] == 'i'){
							if(word[Actual+3] == 't'){
								if(word[Actual+4] == 'c'){
									if(word[Actual+5] == 'h'){
										Actual+=6;
										Pertenece = true;
										break;
									}
								}
							}
						}
					}
				}

				if (Actual >= word.Length || (word[Actual] != ' ' && word[Actual] != '\n' && word[Actual] != '\r')) {
					Pertenece = false;
					break;
				}
				
				Actual++;
				
			}while(Actual < word.Length);
			
			if(Pertenece){
				Posicion = Actual;
			}
			
			return Pertenece;
		}
		
		Boolean itsString(){
			Boolean Pertenece;
			int Actual = Posicion;
			
			Pertenece = false;
			
			do {
				if (word[Actual] == 's') {
					if(word[Actual+1] == 't'){
						if(word[Actual+2] == 'r'){
							if(word[Actual+3] == 'i'){
								if(word[Actual+4] == 'n'){
									if(word[Actual+5] == 'g'){
										Actual+=6;
										Pertenece = true;
										break;
									}
								}
							}
						}
					}
				}

				if (Actual >= word.Length || (word[Actual] != ' ' && word[Actual] != '\n' && word[Actual] != '\r')) {
					Pertenece = false;
					break;
				}
				
				Actual++;
				
			}while(Actual < word.Length);
			
			if(Pertenece){
				Posicion = Actual;
			}
			
			return Pertenece;
		}
		
		// 7 CARACTERES
		
		Boolean itsDefault(){
			Boolean Pertenece;
			int Actual = Posicion;
			
			Pertenece = false;
			
			do {
				if (word[Actual] == 'd') {
					if(word[Actual+1] == 'e'){
						if(word[Actual+2] == 'f'){
							if(word[Actual+3] == 'a'){
								if(word[Actual+4] == 'u'){
									if(word[Actual+5] == 'l'){
										if(word[Actual+6] == 't'){
											Actual+=7;
											Pertenece = true;
											break;
										}
									}
								}
							}
						}
					}
				}

				if (Actual >= word.Length || (word[Actual] != ' ' && word[Actual] != '\n' && word[Actual] != '\r')) {
					Pertenece = false;
					break;
				}
				
				Actual++;
				
			}while(Actual < word.Length);
			
			if(Pertenece){
				Posicion = Actual;
			}
			
			return Pertenece;
		}
		
		// CARACTERES INDEFINIDOS
		
		Boolean itsWord(){
			int Actual = Posicion;
			Boolean isIdentifier;
			isIdentifier = false;
			
			do{
				if(Actual > word.Length){
					break;
				}
				
				if ((word[Actual] == ' ' && isIdentifier) || (word[Actual] == '\n' && isIdentifier) || (word[Actual] == '\r' && isIdentifier)) { // Salir Valido
					break;
				}
				
				if(!(word[Actual].ToString() == "_" || validateAlphabet(word[Actual].ToString()) || validateNumber(word[Actual].ToString())) && isIdentifier){
					break;
				}
				
				if(word[Actual].ToString() == "_" || validateAlphabet(word[Actual].ToString()) || (validateNumber(word[Actual].ToString()) && isIdentifier)){ // Primer Caracter Valido
					isIdentifier = true;
					
				}else if(word[Actual] != ' ' && word[Actual] != '\n' && word[Actual] != '\r'){
					isIdentifier = false;
					break;
				}
				
				Actual++;
				
			}while(Actual < word.Length);
			
			if(isIdentifier){
				Posicion = Actual;
			}
			
			return isIdentifier;
		}
		
		Boolean itsNumber(){
			int Actual = Posicion;
			Boolean isNumber;
			isNumber = false;
			
			do{
				if(Actual > word.Length){
					break;
				}
				
				if ((word[Actual] == ' ' && isNumber) || (word[Actual] == '\n' && isNumber)) { // Salir Valido
					break;
				}
				
				if(!validateNumber(word[Actual].ToString()) && !(word[Actual] == ' ' || word[Actual] == '\n' || word[Actual] == '\r')) {
					break;
				}
				
				if(validateNumber(word[Actual].ToString())){ // Primer Caracter Valido
					isNumber = true;
					
				}else if(word[Actual] != ' ' && word[Actual] != '\n'){
					isNumber = false;
					break;
				}
				
				Actual++;
				
			}while(Actual < word.Length);
			
			if(isNumber){
				Posicion = Actual;
			}
			
			return isNumber;
		}
		
		Boolean itsChain(){
			int Actual = Posicion;
			Boolean isIdentifier;
			isIdentifier = false;
			
			do{
				if(Actual > word.Length){
					break;
				}
				
				if(word[Actual] == '\"'){ // Primer Caracter Valido
					while(Actual < word.Length){
					      	Actual++;
					      	if(word[Actual] == '\"'){
					      		isIdentifier = true;
					      		break;
					      	}
					      }
					      
					}else if(word[Actual] != ' ' && word[Actual] != '\n'){
					isIdentifier = false;
					break;
				}
				
				Actual++;
				
				if(isIdentifier) { // Salir Valido
					break;
				}
				
			}while(Actual < word.Length);
			
			if(isIdentifier){
				Posicion = Actual;
			}
			
			return isIdentifier;
		}
		
		Boolean itsSpace(){
			int Actual = Posicion;
			Boolean isIdentifier;
			isIdentifier = true;
			
			do{
				if(Actual > word.Length){
					break;
				}
				
				if(word[Actual] != ' ' && word[Actual] != '\r' && word[Actual] != '\n'){ // Primer Caracter Valido
					isIdentifier = false;
					break;
				}
				
				Actual++;
			}while(Actual < word.Length);
			
			if(isIdentifier){
				Posicion = Actual;
			}
			
			return isIdentifier;
		}
		
		// ***************************************************  Validations for Words ***************************************************
		
		Boolean validateAlphabet(string letter){
			Regex rgx = new Regex(@"[a-z]");
			return rgx.IsMatch(letter);
		}
		
		Boolean validateNumber(string Number){
			Regex rgx = new Regex(@"\d");
			return rgx.IsMatch(Number);
		}
		
	}
}