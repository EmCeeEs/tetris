using UnityEngine;

public interface ISpeedParameters
{
	public float BaseScaleChange { get; }
	public float SpeedScaleChange { get; }
};

[CreateAssetMenu(
	fileName = "SpeedParameters",
	menuName = "ScriptableObjects/SpeedParameters",
	order = 1
)]
public class SpeedParameters : GameParameters, ISpeedParameters
{
	public override string GetParametersName() => "Speed";

	[Header("BLOCK SPEED")]

	[SerializeField]
	private float baseScaleChange;
	public float BaseScaleChange { get => baseScaleChange; }

	[SerializeField]
	private float speedScaleChange;
	public float SpeedScaleChange { get => speedScaleChange; }
}
