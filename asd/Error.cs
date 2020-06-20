/*
 * Created by SharpDevelop.
 * User: EndUser
 * Date: 25/03/2019
 * Time: 07:42 a. m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace asd
{
	/// <summary>
	/// Description of Error.
	/// </summary>
	public class Error
	{
		
		public int indice;
		public string tipo;
		
		public Error(int indice, string tipo)
		{
			this.indice = indice;
			this.tipo = tipo;
		}
	}
}
