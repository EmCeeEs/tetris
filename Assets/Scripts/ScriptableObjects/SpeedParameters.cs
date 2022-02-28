using UnityEngine;

public interface ISpeedParameters
{
	public float TimeScale { get; }

	public int PlayerCooldown { get; }
	public int AttachDelay { get; }

	public float PositionChange { get; }
	public float PositionSpeedChange { get; }
	public float PositionMargin { get; }
};

[CreateAssetMenu(
	fileName = "SpeedParameters",
	menuName = "ScriptableObjects/SpeedParameters",
	order = 1
)]
public class SpeedParameters : GameParameters, ISpeedParameters
{
	public override string GetParametersName() => "Speed";

	[Header("GAME")]

	[SerializeField]
	private float timeScale;
	public float TimeScale { get => timeScale; }

	[Header("PLAYER")]

	[SerializeField, Tooltip("In fixedFrames units.")]
	private int playerCooldown;
	public int PlayerCooldown { get => playerCooldown; }

	[SerializeField, Tooltip("In fixedFrames units.")]
	private int attachDelay;
	public int AttachDelay { get => attachDelay; }

	[Header("BLOCK")]

	[SerializeField, Tooltip("Per fixedFrames.")]
	private float positionChange;
	public float PositionChange { get => positionChange; }

	[SerializeField]
	private float positionSpeedChange;
	public float PositionSpeedChange { get => positionSpeedChange; }

	[SerializeField,
	Tooltip("The max position overlap allowed for rotation and flip.")]
	private float positionMargin;
	public float PositionMargin { get => positionMargin; }
}
