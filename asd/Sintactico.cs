/*
 * Created by SharpDevelop.
 * User: EndUser
 * Date: 13/02/2019
 * Time: 09:14 a. m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;

namespace asd
{
	/// <summary>
	/// Description of Sintactico.
	/// </summary>
	/// 
	
	
	public class Sintactico
	{
		int Posicion;
		string Cadena;
		string tipoVariable; // Uso exclisivo de Declaracion
		
		Lexico verificacion;
		List<Error> errores;

		Componente[,] Matriz0 = new Componente[6,12];  	// ASIGNACION
		Componente[,] Matriz0A = new Componente[3,10];  // CORCHETER ARIT
		Componente[,] Matriz0B = new Componente[3,10];  // PARENTESIS ARIT
		Componente[,] Matriz2 = new Componente[3,3];  	// DECLARACION
		Componente[,] Matriz3 = new Componente[12,25];  // IF
		Componente[,] Matriz4 = new Componente[3,5];  	// SALIDA
		Componente[,] Matriz5 = new Componente[3,3];  	// ENTRADA
		Componente[,] Matriz6 = new Componente[19,24]; 	// FOR
		Componente[,] Matriz7 = new Componente[9,24]; 	// WHILE
		Componente[,] Matriz8 = new Componente[16,22]; 	// CASE
		
		public Sintactico()
		{
			errores = new List<Error>();
			Posicion = 0;
			constructM0();
			constructM0A();
			constructM0B();
			constructM2();
			constructM3();
			constructM4();
			constructM5();
			constructM6();
			constructM7();
			constructM8();
		}
		
		public void setCadena(string Cadena){
			this.Cadena = Cadena;
		}
		
		public string Estado(){
			if(Cadena.Length > 0){
				if(Valida()){
					return "Codigo valido";
				}
				return "Codigo invalido";
			}
			return "No hay codigo";
		}
		
		public List<Error> erroresDeCodigo(){
			return errores;
		}
		
		Boolean Valida(){
			Boolean estatus;
			string tipo;
			int posicionAnterior;
			
			estatus = false;
			posicionAnterior = Posicion;
			verificacion = new Lexico(Cadena);
			
			do{
				tipo = verificacion.validate(ref Posicion);
				
				//MessageBox.Show(tipo + "    R:" + Cadena.Substring(Posicion));
				
				if(tipo == "SP"){
					break;
					
				}else if(tipo == "V"){
					Variable aux = new Variable (Cadena.Substring(posicionAnterior,Posicion-posicionAnterior),"");
					if(!Semantico.CorrespondenciaDeclaracion(aux)){
						Error E = new Error(indiceFila(),"Variable no declarada");
						errores.Add(E);
						estatus = false;
					}else{
						estatus = valM0();
					}
					
				}else if(tipo == "INT" || tipo == "STRING" || tipo == "CHAR"){
					tipoVariable = tipo;
					estatus = valM2();
					
				}else if(tipo == "IF"){
					estatus = valM3();
					
				}else if(tipo == "<<"){
					estatus = valM4();
					
				}else if(tipo == ">>"){
					estatus = valM5();
					
				}else if(tipo == "FOR"){
					estatus = valM6();
					
				}else if(tipo == "WHILE"){
					estatus = valM7();
					
				}else if(tipo == "SWITCH"){
					estatus = valM8();
					
				}else{
					estatus = false;
				}
				
				if(tipo == "Error"){
					Error E = new Error(indiceFila(),"Caracter No Apectado por el compilador");
					errores.Add(E);
					estatus = false;
					break;
				}
				
				if(!estatus){
					Error E;
					if(tipo == "V"){
						E = new Error(indiceFila(),"Error cerca de: Variable");
					}else if(tipo == "N"){
						E = new Error(indiceFila(),"Error cerca de: Numero");
					}else if(tipo == "C"){
						E = new Error(indiceFila(),"Error cerca de: Cadena");
					}else{
						E = new Error(indiceFila(),"Error cerca de: " + tipo);
					}
					errores.Add(E);
				}
				
				posicionAnterior = Posicion;
				
			}while((Posicion)<Cadena.Length);
			
			if(errores.Count > 0 ){
				estatus = false;
			}
			
			return estatus;
		}
		
		Boolean valM0(){
			int fila,posicionAnterior;
			string tipo;
			Boolean salida;
			Componente actual = new Componente();
			Lexico temp = new Lexico(Cadena);
			
			fila = 0;
			salida = false;
			posicionAnterior = Posicion;
			
			do{
				tipo = temp.validate(ref Posicion);  // Tipo de dato entrante
				
				if(tipo == "=" && fila == 0){ // Columna 0
					actual = Matriz0[fila,0];
					fila = actual.Direccion;
					
				}else if(tipo == "N" && (fila == 1 || fila == 5)){ // Columna 8
					actual = Matriz0[fila,1];
					fila = actual.Direccion;
					
				}else if(tipo == "V" && (fila == 1)){ // Columna 9
					Variable aux = new Variable (Cadena.Substring(posicionAnterior,Posicion-posicionAnterior),"");
					if(!Semantico.CorrespondenciaDeclaracion(aux)){
						Error E = new Error(indiceFila(),"Variable no declarada");
						errores.Add(E);
						break;
					}
					actual = Matriz0[fila,2];
					fila = actual.Direccion;
					
				}else if(tipo == "C" && (fila == 1 || fila == 5)){ // Columna 9
					actual = Matriz0[fila,3];
					fila = actual.Direccion;
					
				}else if(tipo == "[" && (fila == 3)){ // Columna 9
					if(!valM0A()){
						return false;
					}
					actual = Matriz0[fila,4];
					fila = actual.Direccion;
					
				}else if(tipo == "+" && (fila == 2 || fila == 3 || fila == 4)){ // Columna 9
					actual = Matriz0[fila,5];
					fila = actual.Direccion;
					
				}else if(tipo == "-" && (fila == 2 || fila == 3)){ // Columna 9
					actual = Matriz0[fila,6];
					fila = actual.Direccion;
					
				}else if(tipo == "*" && (fila == 2 || fila == 3)){ // Columna 9
					actual = Matriz0[fila,7];
					fila = actual.Direccion;
					
				}else if(tipo == "/" && (fila == 2 || fila == 3)){ // Columna 9
					actual = Matriz0[fila,8];
					fila = actual.Direccion;
					
				}else if(tipo == "%" && (fila == 2 || fila == 3)){ // Columna 9
					actual = Matriz0[fila,9];
					fila = actual.Direccion;
					
				}else if(tipo == "(" && (fila == 1)){ // Columna 9
					if(!valM0B()){
						return false;
					}
					actual = Matriz0[fila,10];
					fila = actual.Direccion;
					
				}else if(tipo == ";" && (fila == 2 || fila == 3 || fila == 4)){ // Columna 9
					actual = Matriz0[fila,11];
					fila = actual.Direccion;
					
				}else{
					salida = false;
					break;
				}
				
				if(actual.Terminal){
					salida = true;
					break;
				}
				
				posicionAnterior = Posicion;
				
			}while((Posicion) < Cadena.Length);
			
			return salida;
		} 	// Asignacion
		
		Boolean valM0A(){
			int fila,posicionAnterior;
			string tipo;
			Boolean salida;
			Componente actual = new Componente();
			Lexico temp = new Lexico(Cadena);
			
			fila = 0;
			salida = false;
			posicionAnterior = Posicion;
			
			do{
				tipo = temp.validate(ref Posicion);  // Tipo de dato entrante
				
				if(tipo == "N" && fila == 0){ // Columna 0
					actual = Matriz0A[fila,0];
					fila = actual.Direccion;
					
				}else if(tipo == "V" && fila == 0){ // Columna 1
					Variable aux = new Variable (Cadena.Substring(posicionAnterior,Posicion-posicionAnterior),"");
					if(!Semantico.CorrespondenciaDeclaracion(aux)){
						Error E = new Error(indiceFila(),"Variable no declarada");
						errores.Add(E);
						break;
					}
					actual = Matriz0A[fila,1];
					fila = actual.Direccion;
					
				}else if(tipo == "[" && fila == 2){  // Columna 2
					if(!valM0A()){
						return false;
					}
					actual = Matriz0A[fila,2];
					fila = actual.Direccion;
					
				}else if(tipo == "]" && (fila == 1 || fila == 2)){ // Columna 3
					actual = Matriz0A[fila,3];
					fila = actual.Direccion;
					
				}else if(tipo == "+" && (fila == 1 || fila == 2)){ // Columna 4
					actual = Matriz0A[fila,4];
					fila = actual.Direccion;
					
				}else if(tipo == "-" && (fila == 1 || fila == 2)){ // Columna 5
					actual = Matriz0A[fila,5];
					fila = actual.Direccion;
					
				}else if(tipo == "*" && (fila == 1 || fila == 2)){ // Columna 6
					actual = Matriz0A[fila,6];
					fila = actual.Direccion;
					
				}else if(tipo == "/" && (fila == 1 || fila == 2)){ // Columna 7
					actual = Matriz0A[fila,7];
					fila = actual.Direccion;
					
				}else if(tipo == "%" && (fila == 1 || fila == 2)){ // Columna 8
					actual = Matriz0A[fila,8];
					fila = actual.Direccion;
					
				}else if(tipo == "(" && fila == 0){ // Columna 9
					if(!valM0B()){
						return false;
					}
					actual = Matriz0A[fila,9];
					fila = actual.Direccion;
				}else{
					salida = false;
					break;
				}
				
				if(actual.Terminal){
					salida = true;
					break;
				}
				
				posicionAnterior = Posicion;
				
			}while((Posicion) < Cadena.Length);
			
			return salida;
		} // Corchetes
		
		Boolean valM0B(){
			int fila,posicionAnterior;
			string tipo;
			Boolean salida;
			Componente actual = new Componente();
			Lexico temp = new Lexico(Cadena);
			
			fila = 0;
			salida = false;
			posicionAnterior = Posicion;
			
			do{
				tipo = temp.validate(ref Posicion);  // Tipo de dato entrante
				
				if(tipo == "N" && fila == 0){ // Columna 0
					actual = Matriz0B[fila,0];
					fila = actual.Direccion;
					
				}else if(tipo == "V" && fila == 0){  // Columna 1
					Variable aux = new Variable (Cadena.Substring(posicionAnterior,Posicion-posicionAnterior),"");
					if(!Semantico.CorrespondenciaDeclaracion(aux)){
						Error E = new Error(indiceFila(),"Variable no declarada");
						errores.Add(E);
						break;
					}
					actual = Matriz0B[fila,1];
					fila = actual.Direccion;
					
				}else if(tipo == "[" && fila == 2){  // Columna 2
					if(!valM0A()){
						return false;
					}
					actual = Matriz0B[fila,2];
					fila = actual.Direccion;
					
				}else if(tipo == "+" && (fila == 1 || fila == 2)){ // Columna 3
					actual = Matriz0B[fila,3];
					fila = actual.Direccion;
					
				}else if(tipo == "-" && (fila == 1 || fila == 2)){ // Columna 4
					actual = Matriz0B[fila,4];
					fila = actual.Direccion;
					
				}else if(tipo == "*" && (fila == 1 || fila == 2)){ // Columna 5
					actual = Matriz0B[fila,5];
					fila = actual.Direccion;
					
				}else if(tipo == "/" && (fila == 1 || fila == 2)){ // Columna 6
					actual = Matriz0B[fila,6];
					fila = actual.Direccion;
					
				}else if(tipo == "%" && (fila == 1 || fila == 2)){ // Columna 7
					actual = Matriz0B[fila,7];
					fila = actual.Direccion;
					
				}else if(tipo == "(" && fila == 0){ // Columna 8
					if(!valM0B()){
						return false;
					}
					actual = Matriz0B[fila,8];
					fila = actual.Direccion;
					
				}else if(tipo == ")" && (fila == 1 || fila == 2)){ // Columna 9
					actual = Matriz0B[fila,9];
					fila = actual.Direccion;
					
				}else{
					salida = false;
					break;
				}
				
				if(actual.Terminal){
					salida = true;
					break;
				}
				
				posicionAnterior = Posicion;
				
			}while((Posicion) < Cadena.Length);
			
			return salida;
		} // Perentesis
		
		Boolean valM2(){
			int fila,posicionAnterior;
			string tipo;
			Boolean salida;
			Componente actual = new Componente();
			Lexico temp = new Lexico(Cadena);
			
			fila = 0;
			salida = false;
			posicionAnterior = Posicion;
			
			do{
				tipo = temp.validate(ref Posicion);
				if(tipo == "V" && fila == 0){
					
					Variable aux = new Variable (Cadena.Substring(posicionAnterior,Posicion-posicionAnterior),tipoVariable);
					if(!Semantico.GuardarVariable(aux)){
						Error E = new Error(indiceFila(),"Duplicacion de variable");
						errores.Add(E);
						return false;
					}
					
					actual = Matriz2[fila,0];
					fila = actual.Direccion;
					
				}else if(tipo == "[" && fila ==1){
					if(!valM0A()){
						return false;
					}
					actual = Matriz2[fila,1];
					fila = actual.Direccion;
					
				}else if(tipo == ";" && (fila == 1 || fila == 2)){
					actual = Matriz2[fila,2];
					fila = actual.Direccion;
				}else{
					salida = false;
					break;
				}
				
				if(actual.Terminal){
					salida = true;
					break;
				}
				
			}while((Posicion) < Cadena.Length);
			
			return salida;
		}  // Declaracion
		
		Boolean valM3(){
			int fila,posicionAnterior,nVariables;
			string tipo;
			Boolean salida;
			Componente actual = new Componente();
			Lexico temp = new Lexico(Cadena);
			Variable aux1,aux2;
			
			fila = 0;
			nVariables = 0;
			salida = false;
			posicionAnterior = Posicion;
			aux1 = new Variable("","");
			
			do{
				tipo = temp.validate(ref Posicion);
				
				if(tipo == "N" && (fila == 1 || fila == 3)){
					actual = Matriz3[fila,0];
					fila = actual.Direccion;
					
				}else if(tipo == "V" && (fila == 1)){
					aux1 = new Variable (Cadena.Substring(posicionAnterior,Posicion-posicionAnterior),"");
					if(!Semantico.CorrespondenciaDeclaracion(aux1)){
						Error E = new Error(indiceFila(),"Variable no declarada");
						errores.Add(E);
						break;
					}
					actual = Matriz3[fila,1];
					fila = actual.Direccion;
					nVariables++;
					
				}else if(tipo == "V" && (fila == 3)){
					aux2 = new Variable (Cadena.Substring(posicionAnterior,Posicion-posicionAnterior),"");
					if(!Semantico.CorrespondenciaDeclaracion(aux2)){
						Error E = new Error(indiceFila(),"Variable no declarada");
						errores.Add(E);
						break;
					}
					if(nVariables > 0){
						if(!Semantico.CorrepondenciaOperacion(aux1,aux2)){
							Error E = new Error(indiceFila(),"Sin correspondencia de variables");
							errores.Add(E);
							break;
						}
					}
					actual = Matriz3[fila,1];
					fila = actual.Direccion;
					nVariables = 0;
					
				}else if(tipo == "V" && (fila == 8)){
					if(!valM0()){
						return false;
					}
					actual = Matriz3[fila,1];
					fila = actual.Direccion;
					
				}else if(tipo == "[" && (fila == 5 || fila == 6)){
					if(!valM0A()){
						return false;
					}
					actual = Matriz3[fila,3];
					fila = actual.Direccion;
					
				}else if(tipo == "&&" && (fila == 4 || fila == 6)){
					actual = Matriz3[fila,3];
					fila = actual.Direccion;
					
				}else if(tipo == "||" && (fila == 4 || fila == 6)){
					actual = Matriz3[fila,4];
					fila = actual.Direccion;
					
				}else if(tipo == "(" && (fila == 0)){
					actual = Matriz3[fila,5];
					fila = actual.Direccion;
					
				}else if(tipo == ")" && (fila == 4 || fila == 6)){
					actual = Matriz3[fila,6];
					fila = actual.Direccion;
					
				}else if(tipo == "<" && (fila == 2 || fila == 5)){
					actual = Matriz3[fila,7];
					fila = actual.Direccion;
					
				}else if(tipo == ">" && (fila == 2 || fila == 5)){
					actual = Matriz3[fila,8];
					fila = actual.Direccion;
					
				}else if(tipo == "<=" && (fila == 2 || fila == 5)){
					actual = Matriz3[fila,9];
					fila = actual.Direccion;
					
				}else if(tipo == ">=" && (fila == 2 || fila == 5)){
					actual = Matriz3[fila,10];
					fila = actual.Direccion;
					
				}else if(tipo == "==" && (fila == 2 || fila == 5)){
					actual = Matriz3[fila,11];
					fila = actual.Direccion;
					
				}else if(tipo == "!=" && (fila == 2 || fila == 5)){
					actual = Matriz3[fila,12];
					fila = actual.Direccion;
					
				}else if(tipo == "{" && (fila == 7 || fila == 10)){
					actual = Matriz3[fila,13];
					fila = actual.Direccion;
					
				}else if(tipo == "}" && (fila == 8 || fila == 11)){
					actual = Matriz3[fila,14];
					fila = actual.Direccion;
					
				}else if(tipo == "IF" && (fila == 8 || fila == 10 || fila == 11)){
					if(!valM3()){
						return false;
					}
					actual = Matriz3[fila,15];
					fila = actual.Direccion;
					
				}else if(tipo == "INT" && (fila == 8 || fila == 11)){
					tipoVariable = tipo;
					if(!valM2()){
						return false;
					}
					actual = Matriz3[fila,16];
					fila = actual.Direccion;
					
				}else if(tipo == "STRING" && (fila == 8 || fila == 11)){
					tipoVariable = tipo;
					if(!valM2()){
						return false;
					}
					actual = Matriz3[fila,17];
					fila = actual.Direccion;
					
				}else if(tipo == "CHAR" && (fila == 8 || fila == 11)){
					tipoVariable = tipo;
					if(!valM2()){
						return false;
					}
					actual = Matriz3[fila,18];
					fila = actual.Direccion;
					
				}else if(tipo == "<<" && (fila == 8 || fila == 11)){
					if(!valM4()){
						return false;
					}
					actual = Matriz3[fila,19];
					fila = actual.Direccion;
					
				}else if(tipo == ">>" && (fila == 8 || fila == 11)){
					if(!valM5()){
						return false;
					}
					actual = Matriz3[fila,20];
					fila = actual.Direccion;
					
				}else if(tipo == "FOR" && (fila == 8 || fila == 11)){
					if(!valM6()){
						return false;
					}
					actual = Matriz3[fila,21];
					fila = actual.Direccion;
					
				}else if(tipo == "WHILE" && (fila == 8 || fila == 11)){
					if(!valM7()){
						return false;
					}
					actual = Matriz3[fila,22];
					fila = actual.Direccion;
					
				}else if(tipo == "SWITCH" && (fila == 8 || fila == 11)){
					if(!valM8()){
						return false;
					}
					actual = Matriz3[fila,23];
					fila = actual.Direccion;
					
				}else if(tipo == "ELSE" && (fila == 9)){
					actual = Matriz3[fila,24];
					fila = actual.Direccion;
					
				}else{
					salida = false;
					break;
				}
				
				if(actual.Terminal){
					salida = true;
					break;
				}
				
				posicionAnterior = Posicion;
				
			}while((Posicion) < Cadena.Length);
			
			return salida;
		}  // If
		
		Boolean valM4(){
			int fila,posicionAnterior;
			string tipo;
			Boolean salida;
			Componente actual = new Componente();
			Lexico temp = new Lexico(Cadena);
			
			fila = 0;
			salida = false;
			posicionAnterior = Posicion;
			
			do{
				tipo = temp.validate(ref Posicion);
				
				if(tipo == "C" && fila ==0){
					actual = Matriz4[fila,0];
					fila = actual.Direccion;
					
				}else if(tipo == "V" && fila == 0){
					Variable aux = new Variable (Cadena.Substring(posicionAnterior,Posicion-posicionAnterior),"");
					if(!Semantico.CorrespondenciaDeclaracion(aux)){
						Error E = new Error(indiceFila(),"Variable no declarada");
						errores.Add(E);
						break;
					}
					actual = Matriz4[fila,1];
					fila = actual.Direccion;
					
				}else if(tipo == "[" && fila == 2){
					if(!valM0A()){
						return false;
					}
					actual = Matriz4[fila,2];
					fila = actual.Direccion;
					
				}else if(tipo == "+" && (fila == 1 || fila == 2)){
					actual = Matriz4[fila,3];
					fila = actual.Direccion;
					
				}else if(tipo == ";" && (fila == 1 || fila == 2)){
					actual = Matriz4[fila,4];
					fila = actual.Direccion;
					
				}else{
					salida = false;
					break;
				}
				
				if(actual.Terminal){
					salida = true;
					break;
				}
				
				posicionAnterior = Posicion;
				
			}while((Posicion) < Cadena.Length);
			
			return salida;
		}  // Salida
		
		Boolean valM5(){  // Entrada
			int fila,posicionAnterior;
			string tipo;
			Boolean salida;
			Componente actual = new Componente();
			Lexico temp = new Lexico(Cadena);
			
			fila = 0;
			salida = false;
			posicionAnterior = Posicion;
			
			do{
				tipo = temp.validate(ref Posicion);
				if(tipo == "V" && fila == 0){
					Variable aux = new Variable (Cadena.Substring(posicionAnterior,Posicion-posicionAnterior),"");
					if(!Semantico.CorrespondenciaDeclaracion(aux)){
						Error E = new Error(indiceFila(),"Variable no declarada");
						errores.Add(E);
						break;
					}
					actual = Matriz5[fila,0];
					fila = actual.Direccion;
				}else if(tipo == "[" && fila ==1){
					if(!valM0A()){
						return false;
					}
					actual = Matriz5[fila,1];
					fila = actual.Direccion;
				}else if(tipo == ";" && (fila == 1 || fila == 2)){
					actual = Matriz5[fila,2];
					fila = actual.Direccion;
				}else{
					salida = false;
					break;
				}
				
				if(actual.Terminal){
					salida = true;
					break;
				}
				
				posicionAnterior = Posicion;
				
			}while((Posicion) < Cadena.Length);
			
			return salida;
		}  // Entrada
		
		Boolean valM6(){
			int fila,posicionAnterior,nVariables;
			string tipo;
			Boolean salida,cambio;
			Componente actual = new Componente();
			Lexico temp = new Lexico(Cadena);
			Variable aux1,aux2;
			
			fila = 0;
			nVariables = 0;
			cambio = false;
			salida = false;
			posicionAnterior = Posicion;
			aux1 = new Variable("","");
			
			do{
				tipo = temp.validate(ref Posicion);
				
				if(tipo == "N" && (fila == 4 || fila == 6 || fila == 9 || fila == 15)){
					actual = Matriz6[fila,0];
					fila = actual.Direccion;
					
				}else if(tipo == "V" && (fila == 1 || fila == 2)){
					if(cambio){
						Variable aux = new Variable (Cadena.Substring(posicionAnterior,Posicion-posicionAnterior),tipoVariable);
						if(!Semantico.GuardarVariable(aux)){
							Error E = new Error(indiceFila(),"Duplicacion de variable");
							errores.Add(E);
							return false;
						}
					}else{
						Variable aux = new Variable (Cadena.Substring(posicionAnterior,Posicion-posicionAnterior),"");
						if(!Semantico.CorrespondenciaDeclaracion(aux)){
							Error E = new Error(indiceFila(),"Variable no declarada");
							errores.Add(E);
							break;
						}
					}
					actual = Matriz6[fila,1];
					fila = actual.Direccion;
					
				}else if(tipo == "V" && (fila == 6)){
					aux1 = new Variable (Cadena.Substring(posicionAnterior,Posicion-posicionAnterior),"");
					if(!Semantico.CorrespondenciaDeclaracion(aux1)){
						Error E = new Error(indiceFila(),"Variable no declarada");
						errores.Add(E);
						break;
					}
					actual = Matriz6[fila,1];
					fila = actual.Direccion;
					nVariables++;
					
				}else if(tipo == "V" && (fila == 9)){
					aux2 = new Variable (Cadena.Substring(posicionAnterior,Posicion-posicionAnterior),"");
					if(!Semantico.CorrespondenciaDeclaracion(aux2)){
						Error E = new Error(indiceFila(),"Variable no declarada");
						errores.Add(E);
						break;
					}
					if(nVariables > 0){
						if(!Semantico.CorrepondenciaOperacion(aux1,aux2)){
							Error E = new Error(indiceFila(),"Sin correspondencia de variables");
							errores.Add(E);
							break;
						}
					}
					actual = Matriz6[fila,1];
					fila = actual.Direccion;
					nVariables = 0;
					
				}else if(tipo == "V" && (fila == 12)){
					Variable aux = new Variable (Cadena.Substring(posicionAnterior,Posicion-posicionAnterior),"");
					if(!Semantico.CorrespondenciaDeclaracion(aux)){
						Error E = new Error(indiceFila(),"Variable no declarada");
						errores.Add(E);
						break;
					}
					actual = Matriz6[fila,1];
					fila = actual.Direccion;
					
				}else if(tipo == "[" && (fila == 7 || fila == 10 || fila == 13)){
					if(!valM0A()){
						return false;
					}
					actual = Matriz6[fila,2];
					fila = actual.Direccion;
					
				}else if(tipo == "(" && (fila == 0)){
					actual = Matriz6[fila,3];
					fila = actual.Direccion;
					
				}else if(tipo == ")" && (fila == 16)){
					actual = Matriz6[fila,4];
					fila = actual.Direccion;
					
				}else if(tipo == "=" && (fila == 3)){
					actual = Matriz6[fila,5];
					fila = actual.Direccion;
					
				}else if(tipo == "<" && (fila == 7 || fila == 8)){
					actual = Matriz6[fila,6];
					fila = actual.Direccion;
					
				}else if(tipo == ">" && (fila == 7 || fila == 8)){
					actual = Matriz6[fila,7];
					fila = actual.Direccion;
					
				}else if(tipo == "<=" && (fila == 7 || fila == 8)){
					actual = Matriz6[fila,8];
					fila = actual.Direccion;
					
				}else if(tipo == ">=" && (fila == 7 || fila == 8)){
					actual = Matriz6[fila,9];
					fila = actual.Direccion;
					
				}else if(tipo == "{" && (fila == 17)){
					actual = Matriz6[fila,10];
					fila = actual.Direccion;
					
				}else if(tipo == "}" && (fila == 18)){
					actual = Matriz6[fila,11];
					fila = actual.Direccion;
					
				}else if(tipo == "IF" && (fila == 18)){
					if(!valM3()){
						return false;
					}
					actual = Matriz6[fila,12];
					fila = actual.Direccion;
					
				}else if(tipo == "INT" && (fila == 18)){
					tipoVariable = tipo;
					if(!valM2()){
						return false;
					}
					actual = Matriz6[fila,13];
					fila = actual.Direccion;
					
				}else if(tipo == "INT" && (fila == 1)){
					tipoVariable = tipo;
					actual = Matriz6[fila,13];
					fila = actual.Direccion;
					cambio = true;
					
				}else if(tipo == "STRING" && (fila == 18)){
					tipoVariable = tipo;
					if(!valM2()){
						return false;
					}
					actual = Matriz6[fila,14];
					fila = actual.Direccion;
					
				}else if(tipo == "CHAR" && (fila == 18)){
					tipoVariable = tipo;
					if(!valM2()){
						return false;
					}
					actual = Matriz6[fila,15];
					fila = actual.Direccion;
					
				}else if(tipo == "<<" && (fila == 18)){
					if(!valM4()){
						return false;
					}
					actual = Matriz6[fila,16];
					fila = actual.Direccion;
					
				}else if(tipo == ">>" && (fila == 18)){
					if(!valM5()){
						return false;
					}
					actual = Matriz6[fila,17];
					fila = actual.Direccion;
					
				}else if(tipo == "FOR" && (fila == 18)){
					if(!valM6()){
						return false;
					}
					actual = Matriz6[fila,18];
					fila = actual.Direccion;
					
				}else if(tipo == "WHILE" && (fila == 18)){
					if(!valM7()){
						return false;
					}
					actual = Matriz6[fila,19];
					fila = actual.Direccion;
					
				}else if(tipo == "SWITCH" && (fila == 18)){
					if(!valM8()){
						return false;
					}
					actual = Matriz6[fila,20];
					fila = actual.Direccion;
					
				}else if(tipo == ";" && (fila == 1 || fila == 5 || fila == 10 || fila == 11)){
					actual = Matriz6[fila,21];
					fila = actual.Direccion;
					
				}else if(tipo == "+" && (fila == 13 || fila == 14)){
					actual = Matriz6[fila,22];
					fila = actual.Direccion;
					
				}else{
					salida = false;
					break;
				}
				
				if(actual.Terminal){
					salida = true;
					break;
				}
				
				posicionAnterior = Posicion;
				
			}while((Posicion) < Cadena.Length);
			
			return salida;
		} 	// For
		
		Boolean valM7(){
			int fila, posicionAnterior, nVariables;
			string tipo;
			Boolean salida;
			Componente actual = new Componente();
			Lexico temp = new Lexico(Cadena);
			Variable aux1, aux2;

			fila = 0;
			nVariables = 0;
			salida = false;
			posicionAnterior = Posicion;
			aux1 = new Variable("", "");
			
			do{
				tipo = temp.validate(ref Posicion);
				
				if(tipo == "N" && (fila == 1 || fila == 3)){
					actual = Matriz7[fila,0];
					fila = actual.Direccion;
					
				}else if(tipo == "V" && (fila == 1)){
					aux1 = new Variable(Cadena.Substring(posicionAnterior, Posicion - posicionAnterior), "");
					if (!Semantico.CorrespondenciaDeclaracion(aux1)) {
						Error E = new Error(indiceFila(), "Variable no declarada");
						errores.Add(E);
						break;
					}
					
					nVariables++;
					actual = Matriz7[fila,1];
					fila = actual.Direccion;
					
				}else if(tipo == "V" && (fila == 3)){
					aux2 = new Variable(Cadena.Substring(posicionAnterior, Posicion - posicionAnterior), "");
					if (!Semantico.CorrespondenciaDeclaracion(aux2)) {
						Error E = new Error(indiceFila(), "Variable no declarada");
						errores.Add(E);
						break;
					}
					if (nVariables > 0) {
						if (!Semantico.CorrepondenciaOperacion(aux1, aux2)) {
							Error E = new Error(indiceFila(), "Sin correspondencia de variables");
							errores.Add(E);
							break;
						}
					}
					
					nVariables = 0;
					actual = Matriz7[fila,1];
					fila = actual.Direccion;
					
				}else if(tipo == "V" && (fila == 8)){
					if(!valM0()){
						return false;
					}
					actual = Matriz7[fila,1];
					fila = actual.Direccion;
					
				}else if(tipo == "[" && (fila == 5 || fila == 6)){
					if(!valM0A()){
						return false;
					}
					actual = Matriz7[fila,2];
					fila = actual.Direccion;
					
				}else if(tipo == "&&" && (fila == 4 || fila == 6)){
					actual = Matriz7[fila,3];
					fila = actual.Direccion;
					
				}else if(tipo == "||" && (fila == 4 || fila == 6)){
					actual = Matriz7[fila,4];
					fila = actual.Direccion;
					
				}else if(tipo == "(" && (fila == 0)){
					actual = Matriz7[fila,5];
					fila = actual.Direccion;
					
				}else if(tipo == ")" && (fila == 4 || fila == 6)){
					actual = Matriz7[fila,6];
					fila = actual.Direccion;
					
				}else if(tipo == "<" && (fila == 2 || fila == 5)){
					actual = Matriz7[fila,7];
					fila = actual.Direccion;
					
				}else if(tipo == ">" && (fila == 2 || fila == 5)){
					actual = Matriz7[fila,8];
					fila = actual.Direccion;
					
				}else if(tipo == "<=" && (fila == 2 || fila == 5)){
					actual = Matriz7[fila,9];
					fila = actual.Direccion;
					
				}else if(tipo == ">=" && (fila == 2 || fila == 5)){
					actual = Matriz7[fila,10];
					fila = actual.Direccion;
					
				}else if(tipo == "==" && (fila == 2 || fila == 5)){
					actual = Matriz7[fila,11];
					fila = actual.Direccion;
					
				}else if(tipo == "!=" && (fila == 2 || fila == 5)){
					actual = Matriz7[fila,12];
					fila = actual.Direccion;
					
				}else if(tipo == "{" && (fila == 7)){
					actual = Matriz7[fila,13];
					fila = actual.Direccion;
					
				}else if(tipo == "}" && (fila == 8)){
					actual = Matriz7[fila,14];
					fila = actual.Direccion;
					
				}else if(tipo == "IF" && (fila == 8)){
					if(!valM3()){
						return false;
					}
					actual = Matriz7[fila,15];
					fila = actual.Direccion;
					
				}else if(tipo == "INT" && (fila == 8)){
					tipoVariable = tipo;
					if(!valM2()){
						return false;
					}
					actual = Matriz7[fila,16];
					fila = actual.Direccion;
					
				}else if(tipo == "STRING" && (fila == 8)){
					tipoVariable = tipo;
					if(!valM2()){
						return false;
					}
					actual = Matriz7[fila,17];
					fila = actual.Direccion;
					
				}else if(tipo == "CHAR" && (fila == 8)){
					tipoVariable = tipo;
					if(!valM2()){
						return false;
					}
					actual = Matriz7[fila,18];
					fila = actual.Direccion;
					
				}else if(tipo == "<<" && (fila == 8)){
					if(!valM4()){
						return false;
					}
					actual = Matriz7[fila,19];
					fila = actual.Direccion;
					
				}else if(tipo == ">>" && (fila == 8)){
					if(!valM5()){
						return false;
					}
					actual = Matriz7[fila,20];
					fila = actual.Direccion;
					
				}else if(tipo == "FOR" && (fila == 8)){
					if(!valM6()){
						return false;
					}
					actual = Matriz7[fila,21];
					fila = actual.Direccion;
					
				}else if(tipo == "WHILE" && (fila == 8)){
					if(!valM7()){
						return false;
					}
					actual = Matriz7[fila,22];
					fila = actual.Direccion;
					
				}else if(tipo == "SWITCH" && (fila == 8)){
					if(!valM8()){
						return false;
					}
					actual = Matriz7[fila,23];
					fila = actual.Direccion;
					
				}else{
					salida = false;
					break;
				}
				
				if(actual.Terminal){
					salida = true;
					break;
				}
				
				posicionAnterior = Posicion;
				
			}while((Posicion) < Cadena.Length);
			
			return salida;
		} 	// While
		
		Boolean valM8(){
			int fila,posicionAnterior;
			string tipo;
			Boolean salida;
			Componente actual = new Componente();
			Lexico temp = new Lexico(Cadena);
			
			fila = 0;
			salida = false;
			posicionAnterior = Posicion;
			
			do{
				tipo = temp.validate(ref Posicion);
				
				if(tipo == "N" && (fila == 5)){
					actual = Matriz8[fila,0];
					fila = actual.Direccion;
					
				}else if(tipo == "V" && (fila == 1 || fila == 5)){
					Variable aux = new Variable(Cadena.Substring(posicionAnterior, Posicion - posicionAnterior), "");
					if (!Semantico.CorrespondenciaDeclaracion(aux)) {
						Error E = new Error(indiceFila(), "Variable no declarada");
						errores.Add(E);
						break;
					}
					actual = Matriz8[fila,1];
					fila = actual.Direccion;
					
				}else if(tipo == "(" && (fila == 0)){
					actual = Matriz8[fila,2];
					fila = actual.Direccion;
					
				}else if(tipo == ")" && (fila == 2)){
					actual = Matriz8[fila,3];
					fila = actual.Direccion;
					
				}else if(tipo == "{" && (fila == 3)){
					actual = Matriz8[fila,4];
					fila = actual.Direccion;
					
				}else if(tipo == "}" && (fila == 15)){
					actual = Matriz8[fila,5];
					fila = actual.Direccion;
					
				}else if(tipo == "'" && (fila == 5 || fila == 7)){
					actual = Matriz8[fila,6];
					fila = actual.Direccion;
					
				}else if(tipo == "V" && (fila == 6)){
					Variable aux = new Variable(Cadena.Substring(posicionAnterior, Posicion - posicionAnterior), "");
					if (!Semantico.CorrespondenciaDeclaracion(aux)) {
						Error E = new Error(indiceFila(), "Variable no declarada");
						errores.Add(E);
						break;
					}
					actual = Matriz8[fila,7];
					fila = actual.Direccion;
					
				}else if(tipo == "CASE" && (fila == 4 || fila == 11)){
					actual = Matriz8[fila,8];
					fila = actual.Direccion;
					
				}else if(tipo == ":" && (fila == 8 || fila == 12)){
					actual = Matriz8[fila,9];
					fila = actual.Direccion;
					
				}else if(tipo == "DEFAULT" && (fila == 11)){
					actual = Matriz8[fila,10];
					fila = actual.Direccion;
					
				}else if(tipo == "BREAK" && (fila == 9 || fila == 13)){
					actual = Matriz8[fila,11];
					fila = actual.Direccion;
					
				}else if(tipo == ";" && (fila == 10 || fila == 14)){
					actual = Matriz8[fila,12];
					fila = actual.Direccion;
					
				}else if(tipo == "IF" && (fila == 9 || fila == 13)){
					if(!valM3()){
						return false;
					}
					actual = Matriz8[fila,13];
					fila = actual.Direccion;
					
				}else if(tipo == "INT" && (fila == 9 || fila == 13)){
					tipoVariable = tipo;
					if(!valM2()){
						return false;
					}
					actual = Matriz8[fila,14];
					fila = actual.Direccion;
					
				}else if(tipo == "STRING" && (fila == 9 || fila == 13)){
					tipoVariable = tipo;
					if(!valM2()){
						return false;
					}
					actual = Matriz8[fila,15];
					fila = actual.Direccion;
					
				}else if(tipo == "CHAR" && (fila == 9 || fila == 13)){
					tipoVariable = tipo;
					if(!valM2()){
						return false;
					}
					actual = Matriz8[fila,16];
					fila = actual.Direccion;
					
				}else if(tipo == "<<" && (fila == 9 || fila == 13)){
					if(!valM4()){
						return false;
					}
					actual = Matriz8[fila,17];
					fila = actual.Direccion;
					
				}else if(tipo == ">>" && (fila == 9 || fila == 13)){
					if(!valM5()){
						return false;
					}
					actual = Matriz8[fila,18];
					fila = actual.Direccion;
					
				}else if(tipo == "FOR" && (fila == 9 || fila == 13)){
					if(!valM6()){
						return false;
					}
					actual = Matriz8[fila,19];
					fila = actual.Direccion;
					
				}else if(tipo == "WHILE" && (fila == 9 || fila == 13)){
					if(!valM7()){
						return false;
					}
					actual = Matriz8[fila,20];
					fila = actual.Direccion;
					
				}else if(tipo == "SWITCH" && (fila == 9 || fila == 13)){
					if(!valM8()){
						return false;
					}
					actual = Matriz8[fila,21];
					fila = actual.Direccion;
					
				}else{
					salida = false;
					break;
				}
				
				if(actual.Terminal){
					salida = true;
					break;
				}
				
				posicionAnterior = Posicion;
				
			}while((Posicion) < Cadena.Length);
			
			return salida;
		}	// Switch
		
		public int indiceFila()
		{
			int cont = 0;
			string data = Cadena.Substring(0,Posicion);
			string[] splitdata = data.Split('\n');

			foreach(string a in splitdata){
				cont++;
			}

			return cont--;
		}
		
		// ****************************************************** CONTRUCCION DE  MATRICES ******************************************************
		
		void constructM0(){
			int a,b,c,d;
			
			string[] lineas = File.ReadAllLines("m0.txt");
			foreach(string linea in lineas){
				string[] partes = linea.Split(',');
				
				int.TryParse(partes[0],out a);
				int.TryParse(partes[1],out b);
				int.TryParse(partes[2],out c);
				int.TryParse(partes[4],out d);
				
				Componente auxD = new Componente(c,partes[3],(d==1));
				
				Matriz0[a,b] = auxD;
			}
		}
		
		void constructM0A(){
			int a,b,c,d;
			
			string[] lineas = File.ReadAllLines("m0A.txt");
			foreach(string linea in lineas){
				string[] partes = linea.Split(',');
				
				int.TryParse(partes[0],out a);
				int.TryParse(partes[1],out b);
				int.TryParse(partes[2],out c);
				int.TryParse(partes[4],out d);
				
				Componente auxD = new Componente(c,partes[3],(d==1));
				
				Matriz0A[a,b] = auxD;
			}
		}
		
		void constructM0B(){
			int a,b,c,d;
			
			string[] lineas = File.ReadAllLines("m0B.txt");
			foreach(string linea in lineas){
				string[] partes = linea.Split(',');
				
				int.TryParse(partes[0],out a);
				int.TryParse(partes[1],out b);
				int.TryParse(partes[2],out c);
				int.TryParse(partes[4],out d);
				
				Componente auxD = new Componente(c,partes[3],(d==1));
				
				Matriz0B[a,b] = auxD;
			}
		}
		
		void constructM2(){
			int a,b,c,d;
			
			string[] lineas = File.ReadAllLines("m2.txt");
			foreach(string linea in lineas){
				string[] partes = linea.Split(',');
				
				int.TryParse(partes[0],out a);
				int.TryParse(partes[1],out b);
				int.TryParse(partes[2],out c);
				int.TryParse(partes[4],out d);
				
				Componente auxD = new Componente(c,partes[3],(d==1));
				
				Matriz2[a,b] = auxD;
			}
		}
		
		void constructM3(){
			int a,b,c,d;
			
			string[] lineas = File.ReadAllLines("m3.txt");
			foreach(string linea in lineas){
				string[] partes = linea.Split(',');
				
				int.TryParse(partes[0],out a);
				int.TryParse(partes[1],out b);
				int.TryParse(partes[2],out c);
				int.TryParse(partes[4],out d);
				
				Componente auxD = new Componente(c,partes[3],(d==1));
				
				Matriz3[a,b] = auxD;
			}
		}
		
		void constructM4(){
			int a,b,c,d;
			
			string[] lineas = File.ReadAllLines("m4.txt");
			foreach(string linea in lineas){
				string[] partes = linea.Split(',');
				
				int.TryParse(partes[0],out a);
				int.TryParse(partes[1],out b);
				int.TryParse(partes[2],out c);
				int.TryParse(partes[4],out d);
				
				Componente auxD = new Componente(c,partes[3],(d==1));
				
				Matriz4[a,b] = auxD;
			}
		}
		
		void constructM5(){
			int a,b,c,d;
			
			string[] lineas = File.ReadAllLines("m5.txt");
			foreach(string linea in lineas){
				string[] partes = linea.Split(',');
				
				int.TryParse(partes[0],out a);
				int.TryParse(partes[1],out b);
				int.TryParse(partes[2],out c);
				int.TryParse(partes[4],out d);
				
				Componente auxD = new Componente(c,partes[3],(d==1));
				
				Matriz5[a,b] = auxD;
			}
		}
		
		void constructM6(){
			int a,b,c,d;
			
			string[] lineas = File.ReadAllLines("m6.txt");
			foreach(string linea in lineas){
				string[] partes = linea.Split(',');
				
				int.TryParse(partes[0],out a);
				int.TryParse(partes[1],out b);
				int.TryParse(partes[2],out c);
				int.TryParse(partes[4],out d);
				
				Componente auxD = new Componente(c,partes[3],(d==1));
				
				Matriz6[a,b] = auxD;
			}
		}
		
		void constructM7(){
			int a,b,c,d;
			
			string[] lineas = File.ReadAllLines("m7.txt");
			foreach(string linea in lineas){
				string[] partes = linea.Split(',');
				
				int.TryParse(partes[0],out a);
				int.TryParse(partes[1],out b);
				int.TryParse(partes[2],out c);
				int.TryParse(partes[4],out d);
				
				Componente auxD = new Componente(c,partes[3],(d==1));
				
				Matriz7[a,b] = auxD;
			}
		}
		
		void constructM8(){
			int a,b,c,d;
			
			string[] lineas = File.ReadAllLines("m8.txt");
			foreach(string linea in lineas){
				string[] partes = linea.Split(',');
				
				int.TryParse(partes[0],out a);
				int.TryParse(partes[1],out b);
				int.TryParse(partes[2],out c);
				int.TryParse(partes[4],out d);
				
				Componente auxD = new Componente(c,partes[3],(d==1));
				
				Matriz8[a,b] = auxD;
			}
		}
		
	}
}