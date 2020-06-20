/*
 * Created by SharpDevelop.
 * User: EndUser
 * Date: 25/03/2019
 * Time: 09:18 a. m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace asd
{
	/// <summary>
	/// Description of Semantico.
	/// </summary>
	/// 
	
	public class Variable{
		
		public string nombre;
		public string tipo;
		
		public Variable(string nombre, string tipo){
			nombre = nombre.Replace(" ","");
			nombre = nombre.Replace("\n","");
			nombre = nombre.Replace("\r","");
			this.nombre = nombre;
			this.tipo = tipo;
		}
	}
	
	public static class Semantico
	{
		public static List<Variable> variables = new List<Variable>();
		
		static Semantico()
		{
		}
		
		public static void Reiniciar(){
			variables.Clear();
		}
		
		public static Boolean GuardarVariable(Variable a){
			if(DuplicacionVariables(a)){
				variables.Add(a);
				return true;
			}
			return false;
		}
		
		// Implementacion por ver
		public static Boolean DuplicacionVariables(Variable a){
			Boolean libre;
			libre = true;
			foreach(Variable b in variables){
				if(b.nombre == a.nombre){
					libre = false;
					break;
				}
			}
			
			return libre;
		}
		
		public static Boolean CorrespondenciaDeclaracion(Variable a){
			Boolean retorno;
			retorno = false;
			foreach(Variable aAux in variables){
				if(aAux.nombre == a.nombre){
					retorno = true;
					break;
				}
			}
			return retorno;
			
		}
		
		public static Boolean CorrepondenciaOperacion(Variable a, Variable b){
			Boolean retorno;
			retorno = false;
			foreach(Variable aAux in variables){
				if(aAux.nombre == a.nombre){
					a = aAux;
					foreach(Variable bAux in variables){
						if(bAux.nombre == b.nombre){
							b = bAux;
							if(a.tipo == b.tipo){
								retorno = true;
								break;
							}
						}
					}
				}
			}
			return retorno;
		}
	}
}
