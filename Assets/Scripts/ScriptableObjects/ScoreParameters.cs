using UnityEngine;

[CreateAssetMenu(
	fileName = "ScoreParameters",
	menuName = "ScriptableObjects/ScoreParameters",
	order = 1
)]
public class ScoreParameters : GameParameters
{
	public override string GetParametersName() => "Score";

	[Header("BLOCK")]
	[SerializeField]
	private int pointsFor1Block;

	[Header("ROW")]
	[SerializeField]
	private int pointsFor1Row;

	[SerializeField]
	private int pointsFor2Rows;

	[SerializeField]
	private int pointsFor3Rows;

	[SerializeField]
	private int pointsFor4Rows;

	public int GetBlockPoints() => pointsFor1Block;

	public int GetRowPoints(int count) => count switch
	{
		1 => pointsFor1Row,
		2 => pointsFor2Rows,
		3 => pointsFor3Rows,
		4 => pointsFor4Rows,
		_ => 0,
	};

}
