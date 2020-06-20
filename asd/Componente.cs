/*
 * Created by SharpDevelop.
 * User: EndUser
 * Date: 25/03/2019
 * Time: 07:47 a. m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace asd
{
	/// <summary>
	/// Description of Componente.
	/// </summary>
	public class Componente{
		// Direccion = -1  => Es sin siguiente
		public int Direccion;
		
		//Tipos =  V, N, M, C, [, ], =, ;, +, -, *, /, %
		public string Tipo;
		
		// Verdadero o Falso
		public Boolean Terminal;
		
		public Componente(){
			
		}
		
		public Componente(int Direccion, string Tipo, Boolean Terminal){
			this.Direccion = Direccion ;
			this.Tipo = Tipo ;
			this.Terminal = Terminal ;
		}
		
		public override string ToString()
		{
			return string.Format("[{0}, {1}, {2}]", Direccion, Tipo, Terminal);
		}

	}
	
}
