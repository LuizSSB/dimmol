using System;
using UnityEngine;

public interface IBondUpdate
{
	GameObject atompointer1 { get; set; }
	int atomnumber1 { get; set; }
	GameObject atompointer2 { get; set; }
	int atomnumber2 { get; set; }
}
