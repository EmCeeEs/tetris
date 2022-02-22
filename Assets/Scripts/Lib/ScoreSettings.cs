using UnityEngine;

[CreateAssetMenu(fileName = "ScoreSettings", menuName = "ScriptableObjects/ScoreSettings", order = 1)]
public class ScoreSettings : ScriptableObject
{
	public int baseBlockScore;
	public int singleRowScore;
	public int doubleRowScore;
	public int tripleRowScore;
	public int quatrupleRowScore;
}
