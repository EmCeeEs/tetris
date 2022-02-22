using UnityEngine;

[CreateAssetMenu(fileName = "ColorManager", menuName = "ScriptableObjects/ColorManager", order = 1)]
public class ColorManager : ScriptableObject
{
	public Color32[] colors;

	public static Color32[] CreateColors()
	{
		Color32[] colors = new Color32[10];
		colors[0] = new Color32(0, 255, 0, 1);
		colors[1] = new Color32(153, 255, 102, 1);
		colors[2] = new Color32(204, 255, 51, 1);
		colors[3] = new Color32(204, 204, 0, 1);
		colors[4] = new Color32(255, 153, 0, 1);
		colors[5] = new Color32(204, 51, 0, 1);
		colors[6] = new Color32(255, 0, 0, 1);
		colors[7] = new Color32(153, 0, 51, 1);
		colors[8] = new Color32(102, 0, 51, 1);
		colors[9] = new Color32(102, 0, 51, 1);

		return colors;
	}
}
