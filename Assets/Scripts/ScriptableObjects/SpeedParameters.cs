using UnityEngine;
using System;

public interface ISpeedParameters
{
	public float BaseScaleChange { get; }
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
	private float blockSpeed;
	public float BaseScaleChange { get => blockSpeed; }
}
