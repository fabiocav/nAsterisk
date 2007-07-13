using System;
using System.Collections.Generic;
using System.Text;

namespace nAsterisk.AGICommand
{
	[Flags]
	public enum EscapeDigits
	{
		None = 0,
		Zero = 1,
		One = 2,
		Two = 4,
		Three = 8,
		Four = 16,
		Five = 32,
		Six = 64,
		Seven = 128,
		Eight = 256,
		Nine = 512,
		Pound = 1024,
		Asterisk = 2048,
		AllNumbers = Zero | One | Two | Three | Four | Five | Six | Seven | Eight | Nine,
		AllSymbols = Pound | Asterisk
	}
}
